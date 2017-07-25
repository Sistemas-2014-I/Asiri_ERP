using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Helper;
using Mvc_Entity;
using CLIt02_archivoEstudioCompl = Mvc_Entity.CLIt02_archivoEstudioCompl;
using CLlt03_atencion = Mvc_Entity.CLlt03_atencion;
using CLlt08_diagnostico = Mvc_Entity.CLlt08_diagnostico;
using CLlt10_estudioCompl = Mvc_Entity.CLlt10_estudioCompl;
using CLlt11_evolucion = Mvc_Entity.CLlt11_evolucion;
using CLlt12_examenFisico = Mvc_Entity.CLlt12_examenFisico;
using CLlt16_tratamiento = Mvc_Entity.CLlt16_tratamiento;
using CLlt17_tratamientoDtl = Mvc_Entity.CLlt17_tratamientoDtl;
using Microsoft.AspNet.Identity;
using System.Text;
using System.Globalization;

namespace Asiri_ERP_Capas.Controllers.Clinica
{

    
    public class AtencionController : Controller
    {
        private AsiriEntities db = new AsiriEntities();


        // GET: CLlt03_atencion
        public ActionResult Index()
        {
            var cLlt03_atencion = db.CLlt03_atencion.Include(c => c.CLlt05_cita);
            return View(cLlt03_atencion.ToList());
        }

        // GET: CLlt03_atencion/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt03_atencion cLlt03_atencion = db.CLlt03_atencion.Find(id);
            if (cLlt03_atencion == null)
            {
                return HttpNotFound();
            }
            return View(cLlt03_atencion);
        }

        public string Codigo()
        {
            Random obj = new Random();
            string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 10;
            string nuevacadena = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[obj.Next(longitud)];
                nuevacadena += letra.ToString();
            }
            return nuevacadena;
        }
        public static string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public JsonResult getCie10(string palabra)
        {
            if (palabra != "")
            {
                //getEmpleado("");
                var Cie10 = db.CLlt04_cie10.ToList().Where(p => RemoveDiacritics(p.descCie10.ToLower()).Contains(RemoveDiacritics(palabra.ToLower())));
                return Json(new SelectList(Cie10, "idCie10", "descCie10"));
            }
            else
            {
                return Json("");
            }
        }
        public JsonResult getServicioProducto(long? id, long idCita)
        {
            if (id == 1)
            {
                //getEmpleado("");
                var citadtl = db.CLlt06_citaDtl.Where(x => x.idCita == idCita).ToList();
                var idServici = citadtl[0].idServicio;
                var servicioP = db.PROt04_servicio.Single(x => x.idServicio == idServici);
                var ServicioProducto = db.PROt04_servicio.Where(x => x.idEspecialidad == servicioP.idEspecialidad);
                return Json(new SelectList(ServicioProducto, "idServicio", "nombreServicio"));
            }
            if (id==2)
            {
                var ServicioProducto = db.PROt02_producto;
                return Json(new SelectList(ServicioProducto, "idProducto", "nombreProductoComercial"));
            }
            
                return Json("");
            
        }
        // GET: CLlt03_atencion/Create
        public ActionResult Create(long? id)
        {
            if (id == null)
            {

                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: 'Ha ocurrido un error inesperado'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("IndexDoctor","Cita");
            }
            var citas = db.CLlt05_cita.Find(id);
            if (citas == null)
            {
                
                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: 'Error, No existe cita'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("IndexDoctor", "Cita");
            }
            var existe = db.CLlt03_atencion.Where(x => x.idCita == id).ToList();
            if (existe.Count != 0)
            {
                
                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: 'Error, La cita insertada ya fue atendida'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("IndexDoctor", "Cita");
            }
            //Calcular Codigo Atencion
            CLlt03_atencion oCLlt03_atencion = new CLlt03_atencion();
            for (int i = 0; i >= 0; i++)
            {
                string nuevacadena = Codigo();
                var lista = db.CLlt03_atencion.Where(x => x.codAtencion == nuevacadena).ToList();
                if (lista.Count == 0)
                {
                    oCLlt03_atencion.codAtencion = nuevacadena;
                    break;

                }
                i++;
            }
            oCLlt03_atencion.idCita = id;
            var cita = db.CLlt05_cita.Find(id);
            //Informacion basica
            ViewBag.paciente = cita.RHUt07_paciente.RHUt09_persona.apellidoPaterno + " " + cita.RHUt07_paciente.RHUt09_persona.apellidoMaterno + ", " + cita.RHUt07_paciente.RHUt09_persona.nombrePersona;
            ViewBag.numDocumento = cita.RHUt07_paciente.RHUt09_persona.numDocIdentidad;
            ViewBag.numHistoria = cita.RHUt07_paciente.numHistoriaClinica;
            List<Mvc_Entity.CLlt04_cie10> cie10 = new List<Mvc_Entity.CLlt04_cie10>();
            //ViewBag.idCie10 = new SelectList(db.CLlt04_cie10, "idCie10", "descCie10");
            ViewBag.idCie10 = new SelectList(cie10, "idCie10", "descCie10");
            
            ViewBag.idProducto = new SelectList(db.PROt02_producto, "idProducto", "nombreProductoComercial");
            var citadtl = db.CLlt06_citaDtl.Where(x=>x.idCita==cita.idCita).ToList();
            var idServici = citadtl[0].idServicio;
            var servicio = db.PROt04_servicio.Single(x=>x.idServicio == idServici);
            ViewBag.idServicio = new SelectList(db.PROt04_servicio.Where(x => x.idEspecialidad == servicio.idEspecialidad), "idServicio", "nombreServicio");
            oCLlt03_atencion.oListAtencion = db.CLlt03_atencion.Include(x => x.CLlt05_cita).Where(x => x.CLlt05_cita.idPaciente == cita.idPaciente).ToList();
            return View(oCLlt03_atencion);
        }

        // POST: CLlt03_atencion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public JsonResult CreateAtencion(CLlt03_atencion cLlt03_atencion)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/Atencion/Create/" + cLlt03_atencion.idCita,
                error = ""
            };
            var idUsu = User.Identity.GetUserId();
            cLlt03_atencion.idUsuario = idUsu;
            cLlt03_atencion.fecRegistro = DateTime.Now;
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    if (cLlt03_atencion.oListDiagnostico.Count != 0 && ModelState.IsValid)
                    {

                        //Atencion
                        db.CLlt03_atencion.Add(cLlt03_atencion);
                        db.SaveChanges();

                        //Cita
                        var oCitaAtn = db.CLlt05_cita.Find(cLlt03_atencion.idCita);
                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaAtendida).ToList();
                        var idEstadocita = estadoCita[0].idEstadoCita;
                        oCitaAtn.idEstadoCita = idEstadocita;
                        db.SaveChanges();

                        //var ultimaAtencion = db.CLlt03_atencion.OrderByDescending(x => x.idAtencion).Take(1).ToList();

                        //Diagnostico y Tratamiento
                        CLlt08_diagnostico oCLlt08_diagnostico;
                        CLlt16_tratamiento oCLlt16_tratamiento;
                        for (int i = 0; i < cLlt03_atencion.oListDiagnostico.Count; i++)
                        {
                            oCLlt08_diagnostico = new CLlt08_diagnostico();
                            oCLlt16_tratamiento = new CLlt16_tratamiento();
                            oCLlt08_diagnostico.idCie10 = cLlt03_atencion.oListDiagnostico[i].idCie10;
                            oCLlt08_diagnostico.descDiagnostico = cLlt03_atencion.oListDiagnostico[i].descDiagnostico;
                            oCLlt08_diagnostico.idAtencion = cLlt03_atencion.idAtencion;
                            db.CLlt08_diagnostico.Add(oCLlt08_diagnostico);
                            db.SaveChanges();

                            //var ultimoDiagnostico = db.CLlt08_diagnostico.OrderByDescending(x => x.idDiagnostico).Take(1).ToList();

                            oCLlt16_tratamiento.descTratamiento = cLlt03_atencion.oListDiagnostico[i].oListTratamiento[0].descTratamiento;
                            oCLlt16_tratamiento.idDiagnostico = oCLlt08_diagnostico.idDiagnostico;
                            db.CLlt16_tratamiento.Add(oCLlt16_tratamiento);
                            db.SaveChanges();
                            CLlt17_tratamientoDtl oCLlt17_tratamientoDtl;
                            for (int j = 0; j < cLlt03_atencion.oListDiagnostico[i].oListTratamiento[0].oListTratamientoDtl.Count; j++)
                            {
                                oCLlt17_tratamientoDtl = new CLlt17_tratamientoDtl();
                                oCLlt17_tratamientoDtl.indicacionServicio = cLlt03_atencion.oListDiagnostico[i].oListTratamiento[0].oListTratamientoDtl[j].indicacionServicio;
                                oCLlt17_tratamientoDtl.idServicio = cLlt03_atencion.oListDiagnostico[i].oListTratamiento[0].oListTratamientoDtl[j].idServicio;
                                oCLlt17_tratamientoDtl.idProducto = cLlt03_atencion.oListDiagnostico[i].oListTratamiento[0].oListTratamientoDtl[j].idProducto;
                                oCLlt17_tratamientoDtl.idTratamiento = oCLlt16_tratamiento.idTratamiento;
                                db.CLlt17_tratamientoDtl.Add(oCLlt17_tratamientoDtl);
                                db.SaveChanges();
                            }


                        }

                        //Anamnesis
                        if (cLlt03_atencion.oAnamnesis.descAnamnesis != null)
                        {
                            cLlt03_atencion.oAnamnesis.idAtencion = cLlt03_atencion.idAtencion;
                            db.CLIt01_anamnesis.Add(cLlt03_atencion.oAnamnesis);
                            db.SaveChanges();
                        }


                        //Evolucion
                        if (cLlt03_atencion.oListEvolucion.Count != 0)
                        {

                            CLlt11_evolucion oCLlt11_evolucion = new CLlt11_evolucion();
                            for (int i = 0; i < cLlt03_atencion.oListEvolucion.Count; i++)
                            {
                                oCLlt11_evolucion.pesimo = false;
                                oCLlt11_evolucion.malo = false;
                                oCLlt11_evolucion.regular = false;
                                oCLlt11_evolucion.bueno = false;
                                oCLlt11_evolucion.excelente = false;
                                if (cLlt03_atencion.oListEvolucion[i].idAtencion == 1)
                                {
                                    oCLlt11_evolucion.pesimo = true;
                                }
                                if (cLlt03_atencion.oListEvolucion[i].idAtencion == 2)
                                {
                                    oCLlt11_evolucion.malo = true;
                                }
                                if (cLlt03_atencion.oListEvolucion[i].idAtencion == 3)
                                {
                                    oCLlt11_evolucion.regular = true;
                                }
                                if (cLlt03_atencion.oListEvolucion[i].idAtencion == 4)
                                {
                                    oCLlt11_evolucion.bueno = true;
                                }
                                if (cLlt03_atencion.oListEvolucion[i].idAtencion == 5)
                                {
                                    oCLlt11_evolucion.excelente = true;
                                }
                                oCLlt11_evolucion.idAtencion = cLlt03_atencion.idAtencion;
                                oCLlt11_evolucion.descEvolucion = cLlt03_atencion.oListEvolucion[i].descEvolucion;
                                db.CLlt11_evolucion.Add(oCLlt11_evolucion);
                                db.SaveChanges();
                            }

                        }


                        //Estudio Complementario
                        if (cLlt03_atencion.oListEstudioCompl.Count != 0)
                        {
                            CLlt10_estudioCompl oCLlt10_estudioCompl;

                            for (int i = 0; i < cLlt03_atencion.oListEstudioCompl.Count; i++)
                            {
                                oCLlt10_estudioCompl = new CLlt10_estudioCompl();
                                oCLlt10_estudioCompl.idAtencion = cLlt03_atencion.idAtencion;
                                oCLlt10_estudioCompl.descEstudioCompl = cLlt03_atencion.oListEstudioCompl[i].descEstudioCompl;
                                db.CLlt10_estudioCompl.Add(oCLlt10_estudioCompl);
                                db.SaveChanges();
                                CLIt02_archivoEstudioCompl oCLIt02_archivoEstudioCompl;
                                for (int j = 0; j < cLlt03_atencion.oListEstudioCompl[i].oListArchivoEstudioCompl.Count; j++)
                                {
                                    var path = Encoding.UTF8.GetString(cLlt03_atencion.oListEstudioCompl[i].oListArchivoEstudioCompl[j].pathArchivo);

                                    string[] foto = path.Split(',');
                                    string b = "";
                                    foreach (string word in foto)
                                    {
                                        b = word;
                                    }
                                    var imagenes = Convert.FromBase64String(b);

                                    //var d = path.Substring(23);
                                    //var imagenes = Convert.FromBase64String(path.Substring(23));

                                    oCLIt02_archivoEstudioCompl = new CLIt02_archivoEstudioCompl();
                                    oCLIt02_archivoEstudioCompl.pathArchivo = imagenes;
                                    oCLIt02_archivoEstudioCompl.idEstudioCompl = oCLlt10_estudioCompl.idEstudioCompl;
                                    db.CLIt02_archivoEstudioCompl.Add(oCLIt02_archivoEstudioCompl);
                                    db.SaveChanges();
                                }
                            }
                        }



                        //Funcion Vital
                        if (cLlt03_atencion.oFuncionVital.altura != null && cLlt03_atencion.oFuncionVital.diastole != null &&
                            cLlt03_atencion.oFuncionVital.imc != null && cLlt03_atencion.oFuncionVital.peso != null &&
                            cLlt03_atencion.oFuncionVital.pulsacion != null && cLlt03_atencion.oFuncionVital.ritmoRespiratorio != null &&
                            cLlt03_atencion.oFuncionVital.sistole != null && cLlt03_atencion.oFuncionVital.temperatura != null)
                        {
                            cLlt03_atencion.oFuncionVital.idAtencion = cLlt03_atencion.idAtencion;
                            db.CLlt13_funcionVital.Add(cLlt03_atencion.oFuncionVital);
                            db.SaveChanges();
                        }

                        //Examen Fisico
                        if (cLlt03_atencion.oListExamenFisico.Count != 0)
                        {
                            CLlt12_examenFisico oCLlt12_examenFisico = new CLlt12_examenFisico();
                            for (int i = 0; i < cLlt03_atencion.oListExamenFisico.Count; i++)
                            {
                                oCLlt12_examenFisico = new CLlt12_examenFisico();
                                oCLlt12_examenFisico.idAtencion = cLlt03_atencion.idAtencion;
                                oCLlt12_examenFisico.descExamenFisico = cLlt03_atencion.oListExamenFisico[i].descExamenFisico;
                                db.CLlt12_examenFisico.Add(oCLlt12_examenFisico);
                                db.SaveChanges();
                            }
                        }


                        respuesta.redirect = "/Cita/IndexDoctor";

                    }
                    else
                    {
                        bool estado = ModelState.IsValid;
                        if (cLlt03_atencion.oListDiagnostico.Count == 0)
                        {
                            ModelState.AddModelError("diagnostico-tratamiento", "Debe agregar un diagnostico y/o tratamiento");
                            respuesta.respuesta = false;
                            respuesta.error = "Debe agregar un diagnostico y/o tratamiento";

                        }
                        if (estado == false)
                        {
                            ModelState.AddModelError("diagnostico-tratamiento", "Debe completar todos los campos necesarios");
                            respuesta.respuesta = false;
                            respuesta.error = "Debe completar todos los campos necesarios";

                        }
                        if (cLlt03_atencion.oListDiagnostico.Count == 0 && estado == false)
                        {
                            ModelState.AddModelError("diagnostico-tratamiento", "Debe completar todos los campos necesarios");
                            respuesta.respuesta = false;
                            respuesta.error = "Debe completar todos los campos necesarios";

                        }
                    }

                    dbTransaction.Commit();

                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                }
            }


            return Json(respuesta);

        }

        public ActionResult obtenerAtencion(long id)
        {
            var atencion = db.CLlt03_atencion.Find(id);
            atencion.oAnamnesis = db.CLIt01_anamnesis.SingleOrDefault(x => x.idAtencion == atencion.idAtencion);
            atencion.oListDiagnostico = db.CLlt08_diagnostico.Where(x => x.idAtencion == atencion.idAtencion).ToList();
            for (int i = 0; i < atencion.oListDiagnostico.Count; i++)
            {
                var idDiagnostico = atencion.oListDiagnostico[i].idDiagnostico;
                atencion.oListDiagnostico[i].oListTratamiento = db.CLlt16_tratamiento.Where(x => x.idDiagnostico == idDiagnostico).ToList();
                var idTratamiento = atencion.oListDiagnostico[i].oListTratamiento[0].idTratamiento;
                atencion.oListDiagnostico[i].oListTratamiento[0].oListTratamientoDtl = db.CLlt17_tratamientoDtl.Where(x => x.idTratamiento == idTratamiento).ToList();
            }
            atencion.oFuncionVital = db.CLlt13_funcionVital.SingleOrDefault(x => x.idAtencion == atencion.idAtencion);
            atencion.oListEvolucion = db.CLlt11_evolucion.Where(x => x.idAtencion == atencion.idAtencion).ToList();
            atencion.oListExamenFisico = db.CLlt12_examenFisico.Where(x => x.idAtencion == atencion.idAtencion).ToList();
            atencion.oListEstudioCompl = db.CLlt10_estudioCompl.Where(x => x.idAtencion == atencion.idAtencion).ToList();
            for (int i = 0; i < atencion.oListEstudioCompl.Count; i++)
            {
                var idEstudio = atencion.oListEstudioCompl[i].idEstudioCompl;
                atencion.oListEstudioCompl[i].oListArchivoEstudioCompl = db.CLIt02_archivoEstudioCompl.Where(x => x.idEstudioCompl == idEstudio).ToList();
            }
            return PartialView(atencion);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
