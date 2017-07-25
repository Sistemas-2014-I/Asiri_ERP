using System;
using System.Collections.Generic;
using System.Linq;
using Common.View.ViewModel;
using Common.Helper;
using Common.Model;
using NLog;
using System.Data.SqlClient;
using Persistence.Conexion;
using System.Transactions;
using Common.View.View_Report;

namespace Persistence.Transaccion
{
    public class ComprobanteDA
    {
        public long Add(TNSt01_comprobanteEmitido obj)
        {
            using (var ctx = new AsiriContext())
            {
                using (var tns = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.Database.ExecuteSqlCommand("UPDATE CLlt05_cita SET esCerrado = @esCerrado Where idCita = @idCita",
                                                        new SqlParameter("@esCerrado", true),
                                                        new SqlParameter("@idCita", obj.idCita));
                        ctx.TNSt01_comprobanteEmitido.Add(obj);
                        ctx.SaveChanges();
                        tns.Commit();
                    }
                    catch (Exception e)
                    {
                        var log = LogManager.GetLogger("fileLogger");
                        log.Error(e, "Excepción en: Agregar un comprobante.");
                        tns.Rollback();
                    }
                }
            }
            return obj.idComprobanteEmitido;
        }

        public bool Anular(long idComp, string razonAnulacion, string idUsuarioAnular)
        {
            var success = false;
            using (var cnn = new SqlConnection(ConnectionManager.GetConnectionString()))
            {
                using (var tns = new TransactionScope())
                {
                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandText = @"UPDATE TNSt01_comprobanteEmitido SET 
                                                esAnulado = @esAnulado,
                                                fecAnulacion = @fecAnulacion,
                                                idUsuarioAnular = @idUsuarioAnular,
                                                razonAnulacion = @razonAnulacion 
                                                Where idComprobanteEmitido=@idComprobanteEmitido";
                            cmd.Parameters.AddWithValue("@esAnulado", true);
                            cmd.Parameters.AddWithValue("@idUsuarioAnular", idUsuarioAnular);
                            cmd.Parameters.AddWithValue("@fecAnulacion", DateTime.Now);
                            cmd.Parameters.AddWithValue("@razonAnulacion", razonAnulacion != null ? razonAnulacion.Trim() : "");
                            cmd.Parameters.AddWithValue("@idComprobanteEmitido", idComp);
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            tns.Complete();
                            success = true;
                        }
                    }
                    catch (Exception e)
                    {
                        var log = LogManager.GetLogger("fileLogger");
                        log.Error(e, "Excepción en: Anular un comprobante.");
                    }
                }
                return success;
            }
        }

        public IEnumerable<ComprobanteVM> List()
        {
            var list = new List<ComprobanteVM>();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.TNSt01_comprobanteEmitido.Select(x => new ComprobanteVM
                    {
                        idComprobanteEmitido = x.idComprobanteEmitido,
                        serie = x.serie,
                        numComprobanteEmitido = x.numComprobanteEmitido,

                        tipoPersoneria = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.tipoPersoneria,
                        nombrePaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.nombrePersona,
                        apellidoPaternoPaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.apellidoPaterno,
                        apellidoMaternoPaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.apellidoMaterno,
                        nombreRepresentante = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.nombreRepresentante,
                        numDoc = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.numDocIdentidad,

                        mtoImpto = x.mtoImpto,
                        mtoTotal = x.mtoTotal,

                        nombreTipoComp = x.SNTt04_tipoComprobante.descTipoComprobante,
                        esAnulado = x.esAnulado,

                        simboloMoneda = x.SNTt03_moneda.simbolo,

                        fecEmision = x.fecEmision
                    }).OrderByDescending(x => x.fecEmision).ToList();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Listar los comprobantes.");
                }
                return list;
            }
        }

        public ComprobanteVM GetPreCompByCita(long idCita)
        {
            var comp = new ComprobanteVM();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    var imptoParam = ctx.SISt01_parametro.SingleOrDefault(pm => pm.codParametro == CodParam.ImpuestoComprobante);
                    var impto = imptoParam != null ? ctx.SNTt02_impuesto.SingleOrDefault(i => i.idImpto == imptoParam.valorNumerico) : null;
                    decimal porcentajeImpto = 0; short idImpto = 0; var abrvImpto = "";
                    if (impto != null)
                    {
                        idImpto = impto.idImpto;
                        abrvImpto = impto.abrvImpto;
                        porcentajeImpto = impto.porcentajeImpto / 100;
                    }
                    //obtener moneda - Considerar la tabla tipo de cambio
                    var monedaParam = ctx.SISt01_parametro.SingleOrDefault(pm => pm.codParametro == CodParam.Moneda);
                    var moneda = monedaParam != null ? ctx.SNTt03_moneda.SingleOrDefault(m => m.idMoneda == monedaParam.valorNumerico) : null;
                    var nombreMoneda = ""; var simboloMoneda = ""; var idMoneda = 0;
                    if (moneda != null)
                    {
                        nombreMoneda = moneda.descMoneda;
                        idMoneda = moneda.idMoneda;
                        simboloMoneda = moneda.simbolo;
                    }

                    comp = ctx.CLlt05_cita.Select(x => new ComprobanteVM()
                    {
                        tipoPersoneria = x.RHUt07_paciente.RHUt09_persona.tipoPersoneria,

                        nombrePaciente = x.RHUt07_paciente.RHUt09_persona.nombrePersona,
                        apellidoPaternoPaciente = x.RHUt07_paciente.RHUt09_persona.apellidoPaterno,
                        apellidoMaternoPaciente = x.RHUt07_paciente.RHUt09_persona.apellidoMaterno,

                        nombreRepresentante = x.RHUt07_paciente.RHUt09_persona.nombreRepresentante,
                        razonSocial = x.RHUt07_paciente.RHUt09_persona.razonSocial,

                        tipoDoc = x.RHUt07_paciente.RHUt09_persona.RHUt12_tipoDocIdentidad.abrvTipoDocIdentidad,
                        numDoc = x.RHUt07_paciente.RHUt09_persona.numDocIdentidad,

                        ComprobanteDtl = x.CLlt06_citaDtl.Select(dtl => new ComprobanteDtlVM()
                        {
                            idCitaDtl = dtl.idCitaDtl,
                            cantidad = dtl.cantidad,
                            precio = dtl.precio,
                            nombreServicio = dtl.PROt04_servicio.nombreServicio
                        }).ToList(),

                        idCita = x.idCita,
                        codCita = x.codCita,
                        fecHoraInicioCita = x.fecCita + " " + x.horaInicio,
                        consultorio = x.RHUt01_empleado.CLlt07_consultorio.codConsultorio,
                        piso = x.RHUt01_empleado.CLlt07_consultorio.MSTt02_piso.numPiso.ToString(),

                        nombreDoctor = x.RHUt01_empleado.RHUt09_persona.nombrePersona,
                        apellidoPaternoDoctor = x.RHUt01_empleado.RHUt09_persona.apellidoPaterno,
                        apellidoMaternoDoctor = x.RHUt01_empleado.RHUt09_persona.apellidoMaterno,

                        //mtoSubTotal = x.mtoTotal - x.mtoTotal / (1 + porcentajeImpto),
                        mtoSubTotal = x.mtoTotal / (1 + porcentajeImpto),
                        mtoImpto = (x.mtoTotal * porcentajeImpto) / (1 + porcentajeImpto),
                        mtoTotal = x.mtoTotal,

                        idImpto = idImpto,
                        abrvImpto = abrvImpto,
                        porcentajeImpto = porcentajeImpto * 100,

                        idMoneda = idMoneda,
                        nombreMoneda = nombreMoneda,
                        simboloMoneda = simboloMoneda
                       

                    }).SingleOrDefault(x => x.idCita == idCita);
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar un pre-comprobante por cita.");
                }
                return comp;
            }
        }

        public IEnumerable<CitasPorCobrarVM> GetCitasPorCobrar()
        {
            var citas = new List<CitasPorCobrarVM>();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    citas = ctx.CLlt05_cita.Where(x => x.esCerrado == false && x.esReprogramado == false).Select(x => new CitasPorCobrarVM()
                    {
                        idCita = x.idCita,
                        codCita = x.codCita,

                        tipoPersoneria = x.RHUt07_paciente.RHUt09_persona.tipoPersoneria,

                        nombrePaciente = x.RHUt07_paciente.RHUt09_persona.nombrePersona,
                        apellidoPaternoPaciente = x.RHUt07_paciente.RHUt09_persona.apellidoPaterno,
                        apellidoMaternoPaciente = x.RHUt07_paciente.RHUt09_persona.apellidoMaterno,

                        nombreRepresentante = x.RHUt07_paciente.RHUt09_persona.nombreRepresentante,
                        razonSocial = x.RHUt07_paciente.RHUt09_persona.razonSocial,

                        tipoDoc = x.RHUt07_paciente.RHUt09_persona.RHUt12_tipoDocIdentidad.abrvTipoDocIdentidad,
                        numDoc = x.RHUt07_paciente.RHUt09_persona.numDocIdentidad,

                        fechaCita = x.fecCita,
                        horaInicio = x.horaInicio,
                        estadoCita = x.CLlt09_estadoCita.descEstadoCita,

                        servicios = x.CLlt06_citaDtl.Select(dtl => new CitaServicioVM()
                        {
                            cantidad = dtl.cantidad,
                            nombreServicio = dtl.PROt04_servicio.nombreServicio
                        }).ToList()

                    }).OrderByDescending(x => x.fechaCita).ToList();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar citas por cobrar.");
                }
                return citas;
            }
        }

        public ComprobanteVM GetCompById(long idComp)
        {
            var comp = new ComprobanteVM();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    comp = ctx.TNSt01_comprobanteEmitido.Select(x => new ComprobanteVM()
                    {
                        idComprobanteEmitido = x.idComprobanteEmitido,

                        serie = x.serie,
                        numComprobanteEmitido = x.numComprobanteEmitido,
                        nombreTipoComp = x.SNTt04_tipoComprobante.descTipoComprobante,
                        fecEmision = x.fecEmision,
                        fecVencimiento = x.fecVencimiento,
                        fecRegistro = x.fecRegistro,
                        fecCancelacion = x.fecCancelacion,
                        fecAnulacion = x.fecAnulacion,
                        esAnulado = x.esAnulado,
                        //Colocar nombres
                        idUsuarioAnular = x.idUsuarioAnular,
                        idUsuario = x.idUsuario,

                        tipoPersoneria = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.tipoPersoneria,

                        nombrePaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.nombrePersona,
                        apellidoPaternoPaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.apellidoPaterno,
                        apellidoMaternoPaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.apellidoMaterno,

                        nombreRepresentante = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.nombreRepresentante,
                        razonSocial = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.razonSocial,

                        tipoDoc = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.RHUt12_tipoDocIdentidad.abrvTipoDocIdentidad,
                        numDoc = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.numDocIdentidad,

                        ComprobanteDtl = x.TNSt02_comprobanteEmitidoDtl.Select(dtl => new ComprobanteDtlVM()
                        {
                            cantidad = dtl.cantidad,
                            precio = dtl.precio,
                            nombreServicio = dtl.CLlt06_citaDtl.PROt04_servicio.nombreServicio
                        }).ToList(),

                        MedioDePagoDtl = x.TNSt06_medioDePagoDtl.Select(dtl => new MedioDePagoDtlVM()
                        {
                            nombreMedioDePago = dtl.MSTt01_medioDePago.descMedioDePago,
                            mtoMedioDePago = dtl.mtoMedioDePago,
                            nombreMoneda = dtl.SNTt03_moneda.descMoneda,
                            tipoDeCambio = (decimal)dtl.tipoDeCambio
                        }).ToList(),

                        mtoSubTotal = x.mtoSubTotal,
                        mtoImpto = x.mtoImpto,
                        porcentajeImpto = x.porcentajeImpto,
                        abrvImpto = x.SNTt02_impuesto.abrvImpto,
                        mtoDescuento = x.mtoDescuento,
                        porcentajeDescuento = x.porcentajeDescuento,
                        mtoTotal = x.mtoTotal,

                        nombreMoneda = x.SNTt03_moneda.descMoneda,
                        simboloMoneda = x.SNTt03_moneda.simbolo,

                        obsvComprobanteEmitido = x.obsvComprobanteEmitido,
                        razonAnulacion = x.razonAnulacion,
                        info01 = x.info01,
                        info02 = x.info01,
                        info03 = x.info01,
                        fecha01 = x.fecha01,
                        fecha02 = x.fecha02,
                        fecha03 = x.fecha03,

                    }).SingleOrDefault(x => x.idComprobanteEmitido == idComp);
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar un comprobante.");
                }
                return comp;
            }
        }

        public ComprobanteVM GetCompBasicById(long idComp)
        {
            var comp = new ComprobanteVM();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    comp = ctx.TNSt01_comprobanteEmitido.Select(x => new ComprobanteVM
                    {
                        idComprobanteEmitido = x.idComprobanteEmitido,

                        serie = x.serie,
                        numComprobanteEmitido = x.numComprobanteEmitido,
                        nombreTipoComp = x.SNTt04_tipoComprobante.descTipoComprobante,
                        fecEmision = x.fecEmision,

                        tipoPersoneria = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.tipoPersoneria,

                        nombrePaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.nombrePersona,
                        apellidoPaternoPaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.apellidoPaterno,
                        apellidoMaternoPaciente = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.apellidoMaterno,

                        nombreRepresentante = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.nombreRepresentante,
                        razonSocial = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.razonSocial,

                        tipoDoc = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.RHUt12_tipoDocIdentidad.abrvTipoDocIdentidad,
                        numDoc = x.CLlt05_cita.RHUt07_paciente.RHUt09_persona.numDocIdentidad,

                        ComprobanteDtl = x.TNSt02_comprobanteEmitidoDtl.Select(dtl => new ComprobanteDtlVM()
                        {
                            cantidad = dtl.cantidad,
                            precio = dtl.precio,
                            nombreServicio = dtl.CLlt06_citaDtl.PROt04_servicio.nombreServicio
                        }).ToList(),

                        mtoSubTotal = x.mtoSubTotal,
                        mtoImpto = x.mtoImpto,
                        porcentajeImpto = x.porcentajeImpto,
                        abrvImpto = x.SNTt02_impuesto.abrvImpto,
                        mtoDescuento = x.mtoDescuento,
                        porcentajeDescuento = x.porcentajeDescuento,
                        mtoTotal = x.mtoTotal,

                        nombreMoneda = x.SNTt03_moneda.descMoneda,
                        simboloMoneda = x.SNTt03_moneda.simbolo,

                    }).SingleOrDefault(x => x.idComprobanteEmitido == idComp);
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar un comprobante básico por id.");
                }
                return comp;
            }
        }

        public int? GetMaxNumCorr(long id)
        {
            int? numCorr = -1;
            using (var ctx = new AsiriContext())
            {
                try
                {
                    numCorr = ctx.Database
                        .SqlQuery<int?>("SP_GET_NUMCORR @idTipoComprobante", new SqlParameter("@idTipoComprobante", id)).First();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Traer el número correlativo por id del tipo de comprobante.");
                }
            }
            return numCorr;
        }

        public int? GetMaxSerie(long id)
        {
            int? serie = -1;
            using (var ctx = new AsiriContext())
            {
                try
                {
                    serie = ctx.Database
                        .SqlQuery<int?>("SP_GET_SERIE @idTipoComprobante", new SqlParameter("@idTipoComprobante", id)).First();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Traer el la serie por id del tipo de comprobante.");
                }
            }
            return serie;
        }

        public int? GetMaxNumCorrByCompSerie(long id, string serie)
        {
            int? numCorr = -1;
            using (var ctx = new AsiriContext())
            {
                try
                {
                    numCorr = ctx.Database
                        .SqlQuery<int?>("SP_GET_NUMCORR_BY_COMP_SERIE @idTipoComprobante,@serie",
                        new SqlParameter("@idTipoComprobante", id),
                         new SqlParameter("@serie", serie)).First();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Traer el número correlativo por id del tipo de comprobante y serie");
                }
            }
            return numCorr;
        }

        //---- Reportes
        //SP_GET_NUMCORR

        public IEnumerable<ComprobanteVR> GetComprobanteVR(long id)
        {
            var list = new List<ComprobanteVR>();
            using (var ctx = new AsiriContext())
            {
                try
                {
                    list = ctx.Database
                        .SqlQuery<ComprobanteVR>("SP_CNS_COMPROBANTE @idComprobanteEmitido", new SqlParameter("@idComprobanteEmitido", id))
                        .ToList();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Consultar un comprobanteVR por id.");
                }
            }
            return list;
        }

        public string GetAmountInText(long id)
        {
            string text = "";
            using (var ctx = new AsiriContext())
            {
                try
                {
                    text = ctx.Database
                        .SqlQuery<string>("SP_GET_MTO_COMPROBANTE_EN_TEXTO @idComprobanteEmitido",
                        new SqlParameter("@idComprobanteEmitido", id)).First();
                }
                catch (Exception e)
                {
                    var log = LogManager.GetLogger("fileLogger");
                    log.Error(e, "Excepción en: Convertir en un número en texto.");
                }
            }
            return text;
        }


    }

}


