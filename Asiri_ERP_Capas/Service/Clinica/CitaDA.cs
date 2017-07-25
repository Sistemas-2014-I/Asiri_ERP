using System;
using Mvc_Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Clinica
{
    public class CitaDA
    {
        public List<CLlt05_cita> verificarCita(CLlt05_cita oCita)
        {
            List<CLlt05_cita> oListCita = new List<CLlt05_cita>();
            using (SqlConnection conexion = ConnectionManager.GetConnection())
            {
                conexion.Open();
                

                //Convertir minutos a horas-Inicio
                var hora = "0" + (int.Parse(oCita.duracionEstimada) / 60).ToString();
                var minutos = int.Parse(oCita.duracionEstimada) % 60;
                var horamin = hora + ":" + minutos.ToString();
                //Fin
                var horafin = TimeSpan.Parse(oCita.horaInicio) + TimeSpan.Parse(horamin);
                string sentencia = "select * from CLlt05_cita where idEmpleado = " + oCita.idEmpleado + " and fecCita = '" + oCita.fecCita.ToString("MM-dd-yyyy") + "' and " +
                                   "convert(timestamp,horaInicio) >= convert(timestamp, '" + oCita.horaInicio + "') and " +
                                   "(convert(timestamp,horaInicio) + convert(timestamp,'0' + convert(varchar(1), convert(int, duracionEstimada)/60) + ':' + convert(varchar(2),convert(int,duracionEstimada%60)))) < convert(timestamp,'" + horafin.ToString(@"hh\:mm") + "') and idEstadoCita = "+oCita.idEstadoCita;
                using (SqlCommand oSqlCommand = new SqlCommand(sentencia, conexion))
                {
                    using (SqlDataReader oSqlDataReader = oSqlCommand.ExecuteReader())
                    {
                        while (oSqlDataReader.Read())
                        {
                            CLlt05_cita oCitaBE = new CLlt05_cita();
                            oCitaBE.idCita = (long)(oSqlDataReader["idCita"]);
                            oCitaBE.codCita = (string)(oSqlDataReader["codCita"]);
                            oCitaBE.fecCita = (DateTime)(oSqlDataReader["fecCita"]);
                            oCitaBE.fecRegistro = (DateTime)(oSqlDataReader["fecRegistro"]);
                            //oCitaBE.fecModificacion = (DateTime)(oSqlDataReader["fecModificacion"]);
                            oCitaBE.horaInicio = (string)(oSqlDataReader["horaInicio"]);
                            oCitaBE.duracionEstimada = (string)(oSqlDataReader["duracionEstimada"]);
                            oCitaBE.numReprogramacion = (byte)(oSqlDataReader["numReprogramacion"]);
                            oCitaBE.esOnline = (bool)(oSqlDataReader["esOnline"]);
                            oCitaBE.esCerrado = (bool)(oSqlDataReader["esCerrado"]);
                            //oCitaBE.idCitaPadre = (long)(oSqlDataReader["idCitaPadre"]);
                            //oCitaBE.obsvCita = (string)(oSqlDataReader["obsvCita"]);
                            oCitaBE.mtoTotal = (decimal)(oSqlDataReader["mtoTotal"]);
                            oCitaBE.idEstadoCita = (short)(oSqlDataReader["idEstadoCita"]);
                            oCitaBE.idPaciente = (long)(oSqlDataReader["idPaciente"]);
                            oCitaBE.idEmpleado = (long)(oSqlDataReader["idEmpleado"]);
                            oCitaBE.idUsuario = (string)(oSqlDataReader["idUsuario"]);
                            //oCitaBE.idUsuarioModificar = (long)(oSqlDataReader["idUsuarioModificar"]);
                            oListCita.Add(oCitaBE);
                        }
                    }
                }
                return oListCita;
            }
        }
    }
}
