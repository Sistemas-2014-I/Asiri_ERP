using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common.Helper;
using Mvc_Entity;
using Service.Clinica;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Security.Permissions;
using System.Text;
using Asiri_ERP_Capas.DA;

namespace Asiri_ERP_Capas.Controllers.Clinica
{
    
    public class CitaController : Controller
    {
        private AsiriEntities db = new AsiriEntities();

        
        // GET: Cita
        public ActionResult Index()
        {

            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlCita);
            var validar1 = false;

            for (int i = 0; i < custom.Count; i++)
            {
                if (custom[i].MenuID == MenuUrl.MenuID)
                {
                    validar1 = true;
                    break;
                }
            }


            AspNetUserRolesDA oAspNetUserRolesDA = new AspNetUserRolesDA();
            var userRol = oAspNetUserRolesDA.obtenerPermisos(idusuario);
            var idRol = userRol[0].RoleId;
            var permiso = db.Permiso.Where(x => x.RoleID == idRol).ToList();
            var validar2 = false;
            for (int i = 0; i < permiso.Count; i++)
            {
                if (permiso[i].MenuID == MenuUrl.MenuID)
                {
                    validar2 = true;
                    break;
                }
            }
            if (validar1 == false && validar2 == false)
            {
                return RedirectToAction("ErrorPermiso", "Account");
            }

            #endregion


            var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
            var pendiente = estadoCita[0].idEstadoCita;
            var cLlt05_cita = db.CLlt05_cita.Include(c => c.CLlt09_estadoCita).Include(c => c.RHUt01_empleado).Include(c => c.RHUt07_paciente).OrderBy(x => x.idEstadoCita).ToList();
            return View(cLlt05_cita);
        }

        
        public ActionResult IndexDoctor()
        {

            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlAtencion);
            var validar1 = false;

            for (int i = 0; i < custom.Count; i++)
            {
                if (custom[i].MenuID == MenuUrl.MenuID)
                {
                    validar1 = true;
                    break;
                }
            }


            AspNetUserRolesDA oAspNetUserRolesDA = new AspNetUserRolesDA();
            var userRol = oAspNetUserRolesDA.obtenerPermisos(idusuario);
            var idRol = userRol[0].RoleId;
            var permiso = db.Permiso.Where(x => x.RoleID == idRol).ToList();
            var validar2 = false;
            for (int i = 0; i < permiso.Count; i++)
            {
                if (permiso[i].MenuID == MenuUrl.MenuID)
                {
                    validar2 = true;
                    break;
                }
            }
            if (validar1 == false && validar2 == false)
            {
                return RedirectToAction("ErrorPermiso", "Account");
            }

            #endregion

            return View();
        }

        public ActionResult IndexPartial()
        {
            var idUsu = User.Identity.GetUserId();
            var emplea = db.AspNetUsers.Find(idUsu);
            var estado = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
            var idestado = estado[0].idEstadoCita;
            var cLlt05_cita = db.CLlt05_cita.Where(x => x.idEmpleado == emplea.idEmpleado && x.idEstadoCita == idestado && x.fecCita == DateTime.Today).Include(c => c.CLlt09_estadoCita).Include(c => c.RHUt01_empleado).Include(c => c.RHUt07_paciente).ToList();
            return PartialView(cLlt05_cita);
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
        public JsonResult getPaciente(string palabra, long idEmpleado)
        {
            if (palabra != "")
            {
                //getEmpleado("");
                var empleado = db.RHUt01_empleado.Find(idEmpleado);
                
                var paciente = db.RHUt07_paciente.Include(c => c.RHUt09_persona).Where(x => x.RHUt09_persona.idPersona != empleado.idPersona).ToList().Select(x => new {
                    idPaciente = x.idPaciente,
                    nombreCompleto = x.RHUt09_persona.apellidoPaterno + " " +
                                    x.RHUt09_persona.apellidoMaterno + ", " +
                                    x.RHUt09_persona.nombrePersona
                }).Where(p => RemoveDiacritics(p.nombreCompleto.ToLower()).Contains(RemoveDiacritics(palabra.ToLower())));
                return Json(new SelectList(paciente, "idPaciente", "nombreCompleto"));
            }
            else
            {
                return Json("");
            }
        }

        public ActionResult Horario()
        {
            
            return View();
        }

        //Mandar lista al calendario
        
        public JsonResult ListarCitasCalendar()
        {   

            var idUsu = User.Identity.GetUserId();
            var emplea = db.AspNetUsers.Find(idUsu);
            var estado = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
            var idestado = estado[0].idEstadoCita;
            var listas = db.CLlt05_cita.Where(x => x.idEmpleado == emplea.idEmpleado && x.idEstadoCita == idestado).Include(c => c.CLlt09_estadoCita).Include(c => c.RHUt01_empleado).Include(c => c.RHUt07_paciente).ToList();
            List<dynamic> oListCalendar = new List<dynamic>();
            for (int i = 0; i < listas.Count; i++)
            {
                var hora = "0" + (int.Parse(listas[i].duracionEstimada) / 60).ToString();
                //var horaN = "0"+hora.ToString();

                var minutos = int.Parse(listas[i].duracionEstimada) % 60;
                var horamin = hora + ":" + minutos.ToString();
                var horaFin = TimeSpan.Parse(listas[i].horaInicio) + (TimeSpan.Parse(horamin));
                var paciente = listas[i].RHUt07_paciente.RHUt09_persona.apellidoPaterno + " " + listas[i].RHUt07_paciente.RHUt09_persona.apellidoMaterno + ", " + listas[i].RHUt07_paciente.RHUt09_persona.nombrePersona;
                oListCalendar.Add(new
                {
                    id = listas[i].idCita.ToString(),
                    title = paciente,
                    start = listas[i].fecCita.ToString("yyyy-MM-dd") + "T" + listas[i].horaInicio + ":00",
                    end = listas[i].fecCita.ToString("yyyy-MM-dd") + "T" + horaFin.ToString(),
                    allday = false
                });
            }
            var filas = oListCalendar.ToArray();
            return Json(filas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerHorario(long id)
        {
            var cita = new CLlt05_cita();
            cita.idEmpleado = id;
            return PartialView(cita);
        }

        public JsonResult MostrarHorario(long id)
        {

            var estado = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
            var idestado = estado[0].idEstadoCita;
            var listas = db.CLlt05_cita.Where(x => x.idEmpleado == id && x.idEstadoCita == idestado).Include(c => c.CLlt09_estadoCita).Include(c => c.RHUt01_empleado).Include(c => c.RHUt07_paciente).ToList();
            List<dynamic> oListCalendar = new List<dynamic>();
            for (int i = 0; i < listas.Count; i++)
            {
                var hora = "0" + (int.Parse(listas[i].duracionEstimada) / 60).ToString();
                //var horaN = "0"+hora.ToString();
                var minutos = int.Parse(listas[i].duracionEstimada) % 60;
                var horamin = hora + ":" + minutos.ToString();
                var horaFin = TimeSpan.Parse(listas[i].horaInicio) + (TimeSpan.Parse(horamin));
                var paciente = listas[i].RHUt07_paciente.RHUt09_persona.apellidoPaterno + " " + listas[i].RHUt07_paciente.RHUt09_persona.apellidoMaterno + ", " + listas[i].RHUt07_paciente.RHUt09_persona.nombrePersona;
                oListCalendar.Add(new
                {
                    id = listas[i].idCita.ToString(),
                    title = paciente,
                    start = listas[i].fecCita.ToString("yyyy-MM-dd") + "T" + listas[i].horaInicio + ":00",
                    end = listas[i].fecCita.ToString("yyyy-MM-dd") + "T" + horaFin.ToString(),
                    allday = false
                });
            }
            var filas = oListCalendar.ToArray();
            return Json(filas, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ObtenerCitaDtl(long id)
        {
            //var cita = db.CLlt05_cita.Include(x => x.CLlt06_citaDtl).Where(x => x.idCita == id).ToList();
            var citadtl = db.CLlt06_citaDtl.Where(x => x.idCita == id).ToList();
            //cita[0].oListCitaDtl = citadtl;

            return PartialView(citadtl);
        }

        
        public ActionResult ConfirmarCita(long id)
        {
            var ocita = db.CLlt05_cita.Find(id);
            var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
            var idEstadocita = estadoCita[0].idEstadoCita;
            ocita.idEstadoCita = idEstadocita;
            var idUsu = User.Identity.GetUserId();
            ocita.idUsuarioModificar = idUsu;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult AnularCita(long id)
        {
            var ocita = db.CLlt05_cita.Find(id);
            var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaAnulada).ToList();
            var idEstadocita = estadoCita[0].idEstadoCita;
            ocita.idEstadoCita = idEstadocita;
            var idUsu = User.Identity.GetUserId();
            ocita.idUsuarioModificar = idUsu;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult Reprogramar(long id)
        {
            var oCita = db.CLlt05_cita.Find(id);
            var ocitadtl = db.CLlt06_citaDtl.Where(x => x.idCita == oCita.idCita).ToList();
            long idservicio = ocitadtl[0].idServicio;
            var servicio = db.PROt04_servicio.Where(x => x.idServicio == idservicio).ToList();
            long idespecialidad = servicio[0].idEspecialidad;
            var empleado = db.RHUt02_empleadoEspecialidad.Where(x => x.idEspecialidad == idespecialidad).Select(x => new {
                idEmpleado = x.idEmpleado,
                nombreCompleto = x.RHUt01_empleado.RHUt09_persona.apellidoPaterno + " " +
                                    x.RHUt01_empleado.RHUt09_persona.apellidoMaterno + ", " +
                                    x.RHUt01_empleado.RHUt09_persona.nombrePersona
            });
            ViewBag.idEmpleado = new SelectList(empleado, "idEmpleado", "nombreCompleto", oCita.idEmpleado);
            ViewBag.fecCita = oCita.fecCita.ToShortDateString();
            return PartialView(oCita);
        }

        private int generarCodCita2()
        {
            CLlt05_cita modelo = (from c in db.CLlt05_cita orderby c.idCita descending select c).First();
            string codigocita = modelo.codCita.Substring(4);
            if (codigocita != null)
            {
                int num = int.Parse(codigocita);
                return ++num;
            }
            else
            {
                return 1;
            }
        }

        //public long? SumarReprogramacion(long? id)
        //{

        //    if (id != null)
        //    {
        //        var citaPadre = db.CLlt05_cita.Single(x => x.idCita == id);
        //        citaPadre.numReprogramacion = (byte)(Convert.ToInt16(citaPadre.numReprogramacion + 1));
        //        db.SaveChanges();
        //        id = citaPadre.idCitaPadre;
        //    }
        //    return id;
        //}




        
        [HttpPost]
        public ActionResult ReprogramarCita(CLlt05_cita oCita)
        {
            var param = Appointment.GetReprogramacionxCita(oCita.idCita);
            if (param != "")
            {
                
                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: '" + param+"'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("Index");
            }
            //var day = new CultureInfo("es-ES");
            //day.DateTimeFormat.GetDayName(oCita.fecCita.DayOfWeek);
            var fechaActual = DateTime.Today;
            TimeSpan horaActual = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString());
            //if (!System.IO.Directory.Exists(@"C:\inetpub\Asiri\"))
            //{
            //    System.IO.Directory.CreateDirectory(@"C:\inetpub\Asiri");
            //}
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var horario = db.RHUt06_horario.Where(x => x.idEmpleado == oCita.idEmpleado).ToList();
                    if (horario.Count != 0)
                    {
                        DateTime fecInicio;
                        fecInicio = oCita.fecCita.Add(TimeSpan.Parse(oCita.horaInicio));
                        DateTime fecFin = fecInicio.AddMinutes(double.Parse(oCita.duracionEstimada));
                        //Parametro

                        var parametroCita = oCita.fecCita.Add(TimeSpan.Parse(Appointment.HoraFinxDia()));
                        if (fecFin > parametroCita)
                        {
                            var hora12 = oCita.fecCita.AddDays(1);

                            if (fecFin >= hora12)
                            {
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'Error, No se puede generar una cita con 2 días distintos'}, {type: 'danger',allow_dismiss: false});});</script>";
                                return RedirectToAction("Index");
                            }

                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'No se puede generar citas a partir de " + parametroCita.ToString("hh:mm tt") + "'}, {type: 'danger',allow_dismiss: false});});</script>";
                            return RedirectToAction("Index");
                        }
                        //TimeSpan horainicio = TimeSpan.Parse(oCita.horaInicio);
                        //TimeSpan duracion = TimeSpan.Parse("00:" + oCita.duracionEstimada);
                        //var horafin = TimeSpan.Parse(oCita.horaInicio) + TimeSpan.Parse("00:" + oCita.duracionEstimada);
                        //var disponibe = db.CLlt05_cita.Where(x => x.idEmpleado == oCita.idEmpleado && x.fecCita == oCita.fecCita &&
                        //                                       TimeSpan.Parse(x.horaInicio) >= horainicio && 
                        //                                     (TimeSpan.Parse(x.horaInicio) + TimeSpan.Parse("00:"+x.duracionEstimada)) <= horafin).ToList();
                        CitaDA oCitaDA = new CitaDA();
                        var estadoCitaPendiente = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                        var idEstadocitaPendiente = estadoCitaPendiente[0].idEstadoCita;
                        oCita.idEstadoCita = idEstadocitaPendiente;
                        var disponibe = oCitaDA.verificarCita(oCita);

                        if (oCita.fecCita == DateTime.Today && fecInicio.TimeOfDay <= horaActual)
                        {
                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close', message: 'La hora es incorrecta'}, {type: 'danger',allow_dismiss: false});});</script>";
                            return RedirectToAction("Index");
                        }

                        if (oCita.fecCita.DayOfWeek.ToString() == "Monday")
                        {

                            var labora = horario[0].laboraLunes;
                            if (labora == true)
                            {
                                var horainiciolunes = TimeSpan.Parse(horario[0].horaInicioLunes.ToString());
                                var horafinlunes = TimeSpan.Parse(horario[0].horaFinLunes.ToString());
                                if (horainiciolunes <= fecInicio.TimeOfDay && horafinlunes >= fecFin.TimeOfDay)
                                {
                                    if (disponibe.Count == 0)
                                    {
                                        //Cita PADRE
                                        var ocita = db.CLlt05_cita.Single(x => x.idCita == oCita.idCita);
                                        CodSystem oCodsystem = new CodSystem();
                                        ocita.fecModificacion = DateTime.Now;
                                        var idUsu = User.Identity.GetUserId();
                                        ocita.idUsuarioModificar = idUsu;
                                        //ocita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var usuario = db.AspNetUsers.Find(idUsu);
                                        var empleado = db.RHUt01_empleado.Where(x => x.idEmpleado == usuario.idEmpleado).ToList();
                                        var idper = empleado[0].idPersona;
                                        var persona = db.RHUt09_persona.Where(x => x.idPersona == idper).ToList();
                                        var nombre = persona[0].apellidoPaterno + " " + persona[0].apellidoMaterno + ", " + persona[0].nombrePersona;
                                        string espacio1 = "Código de Cita Reprogramada";
                                        string espacio2 = "Empleado";
                                        string espacio3 = "Fecha de Modificación";
                                        
                                        if (System.IO.File.Exists(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(espacio1.PadRight(30) + "|" + espacio2.PadRight(250) + "|" + espacio3.PadRight(25));
                                                auditoria.WriteLine("---------------------------".PadRight(30) + "|" + "--------".PadRight(250) + "|" + "---------------------".PadRight(25));
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaReprogramada).ToList();
                                        var idEstadocita = estadoCita[0].idEstadoCita;
                                        ocita.idEstadoCita = idEstadocita;
                                        ocita.esReprogramado = true;
                                        db.SaveChanges();

                                        //var idPadre = SumarReprogramacion(ocita.idCitaPadre);
                                        //for (int i = 0; i >= 0; i++)
                                        //{
                                            
                                        //    if (idPadre == null)
                                        //    {
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        idPadre = SumarReprogramacion(idPadre);
                                        //    }
                                            
                                        //}
                                            
                                            
                                        


                                        int num = (from c in db.CLlt05_cita select c).Count();
                                        if (num != 0)
                                        {
                                            num = generarCodCita2();
                                        }
                                        else
                                        {
                                            num = 1;
                                        }

                                        oCita.codCita = "CT00" + (num).ToString();
                                        Session["idCita"] = oCita.idCita;

                                        //Cita reprogramada

                                        oCita.idCitaPadre = ocita.idCita;
                                        oCita.obsvCita = ocita.obsvCita;
                                        oCita.mtoTotal = ocita.mtoTotal;
                                        oCita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var estadoCitahijo = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                                        var idEstadocitahijo = estadoCitahijo[0].idEstadoCita;
                                        oCita.idEstadoCita = idEstadocitahijo;
                                        oCita.idPaciente = ocita.idPaciente;
                                        oCita.idUsuario = idUsu;
                                        oCita.fecRegistro = DateTime.Now;
                                        db.CLlt05_cita.Add(oCita);
                                        db.SaveChanges();
                                        ocita.oListCitaDtl = db.CLlt06_citaDtl.Where(x => x.idCita == ocita.idCita).ToList();
                                        CLlt06_citaDtl oCitaDtl;
                                        for (int i = 0; i < ocita.oListCitaDtl.Count; i++)
                                        {
                                            oCitaDtl = new CLlt06_citaDtl();
                                            //oCitaDtl.fecRegistro = DateTime.Now;
                                            oCitaDtl.precio = ocita.oListCitaDtl[i].precio;
                                            oCitaDtl.cantidad = ocita.oListCitaDtl[i].cantidad;
                                            oCitaDtl.esGratis = false;
                                            oCitaDtl.activo = true;
                                            oCitaDtl.idCita = oCita.idCita;
                                            oCitaDtl.idServicio = ocita.oListCitaDtl[i].idServicio;
                                            db.CLlt06_citaDtl.Add(oCitaDtl);
                                            db.SaveChanges();
                                        }
                                        dbTransaction.Commit();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'Ya existe el horario asignado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    }
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    
                                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    //el empleado no trabaja en el rango de hora insertado
                                }
                                return RedirectToAction("Index");
                            }
                            else
                            {

                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //el empleado no trabaja ese dia
                            }
                            return RedirectToAction("Index");
                        }
                        if (oCita.fecCita.DayOfWeek.ToString() == "Tuesday")
                        {

                            var labora = horario[0].laboraMartes;
                            if (labora == true)
                            {
                                var horainiciomartes = TimeSpan.Parse(horario[0].horaInicioMartes.ToString());
                                var horafinmartes = TimeSpan.Parse(horario[0].horaFinMartes.ToString());
                                if (horainiciomartes <= fecInicio.TimeOfDay && horafinmartes >= fecFin.TimeOfDay)
                                {
                                    if (disponibe.Count == 0)
                                    {
                                        //Cita PADRE
                                        var ocita = db.CLlt05_cita.Single(x => x.idCita == oCita.idCita);
                                        CodSystem oCodsystem = new CodSystem();
                                        ocita.fecModificacion = DateTime.Now;
                                        var idUsu = User.Identity.GetUserId();
                                        ocita.idUsuarioModificar = idUsu;
                                        //ocita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var usuario = db.AspNetUsers.Find(idUsu);
                                        var empleado = db.RHUt01_empleado.Where(x => x.idEmpleado == usuario.idEmpleado).ToList();
                                        var idper = empleado[0].idPersona;
                                        var persona = db.RHUt09_persona.Where(x => x.idPersona == idper).ToList();
                                        var nombre = persona[0].apellidoPaterno + " " + persona[0].apellidoMaterno + ", " + persona[0].nombrePersona;
                                        string espacio1 = "Código de Cita Reprogramada";
                                        string espacio2 = "Empleado";
                                        string espacio3 = "Fecha de Modificación";
                                        if (System.IO.File.Exists(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(espacio1.PadRight(30) + "|" + espacio2.PadRight(250) + "|" + espacio3.PadRight(25));
                                                auditoria.WriteLine("---------------------------".PadRight(30) + "|" + "--------".PadRight(250) + "|" + "---------------------".PadRight(25));
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaReprogramada).ToList();
                                        var idEstadocita = estadoCita[0].idEstadoCita;
                                        ocita.idEstadoCita = idEstadocita;
                                        ocita.esReprogramado = true;
                                        db.SaveChanges();
                                        //var idPadre = SumarReprogramacion(ocita.idCitaPadre);
                                        //for (int i = 0; i >= 0; i++)
                                        //{

                                        //    if (idPadre == null)
                                        //    {
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        idPadre = SumarReprogramacion(idPadre);
                                        //    }

                                        //}

                                        int num = (from c in db.CLlt05_cita select c).Count();
                                        if (num != 0)
                                        {
                                            num = generarCodCita2();
                                        }
                                        else
                                        {
                                            num = 1;
                                        }

                                        oCita.codCita = "CT00" + (num).ToString();
                                        Session["idCita"] = oCita.idCita;
                                        //Cita reprogramada

                                        oCita.idCitaPadre = ocita.idCita;
                                        oCita.obsvCita = ocita.obsvCita;
                                        oCita.mtoTotal = ocita.mtoTotal;
                                        oCita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var estadoCitahijo = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                                        var idEstadocitahijo = estadoCitahijo[0].idEstadoCita;
                                        oCita.idEstadoCita = idEstadocitahijo;
                                        oCita.idPaciente = ocita.idPaciente;
                                        oCita.idUsuario = idUsu;
                                        oCita.fecRegistro = DateTime.Now;
                                        db.CLlt05_cita.Add(oCita);
                                        db.SaveChanges();
                                        ocita.oListCitaDtl = db.CLlt06_citaDtl.Where(x => x.idCita == ocita.idCita).ToList();
                                        CLlt06_citaDtl oCitaDtl;
                                        for (int i = 0; i < ocita.oListCitaDtl.Count; i++)
                                        {
                                            oCitaDtl = new CLlt06_citaDtl();
                                            //oCitaDtl.fecRegistro = DateTime.Now;
                                            oCitaDtl.precio = ocita.oListCitaDtl[i].precio;
                                            oCitaDtl.cantidad = ocita.oListCitaDtl[i].cantidad;
                                            oCitaDtl.esGratis = false;
                                            oCitaDtl.activo = true;
                                            oCitaDtl.idCita = oCita.idCita;
                                            oCitaDtl.idServicio = ocita.oListCitaDtl[i].idServicio;
                                            db.CLlt06_citaDtl.Add(oCitaDtl);
                                            db.SaveChanges();
                                        }
                                        dbTransaction.Commit();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        
                                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                        //ya existe el horario asigando
                                    }
                                    return RedirectToAction("Index");
                                }
                                else
                                {

                                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    //el empleado no trabaja en el rango de hora insertado
                                }
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //el empleado no trabaja ese dia
                            }
                            return RedirectToAction("Index");
                        }
                        if (oCita.fecCita.DayOfWeek.ToString() == "Wednesday")
                        {

                            var labora = horario[0].laboraMiercoles;
                            if (labora == true)
                            {
                                var horainiciomiercoles = TimeSpan.Parse(horario[0].horaInicioMiercoles.ToString());
                                var horafinmiercoles = TimeSpan.Parse(horario[0].horaFinMiercoles.ToString());
                                if (horainiciomiercoles <= fecInicio.TimeOfDay && horafinmiercoles >= fecFin.TimeOfDay)
                                {
                                    if (disponibe.Count == 0)
                                    {
                                        //Cita PADRE
                                        var ocita = db.CLlt05_cita.Single(x => x.idCita == oCita.idCita);
                                        CodSystem oCodsystem = new CodSystem();
                                        ocita.fecModificacion = DateTime.Now;
                                        var idUsu = User.Identity.GetUserId();
                                        ocita.idUsuarioModificar = idUsu;
                                        //ocita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var usuario = db.AspNetUsers.Find(idUsu);
                                        var empleado = db.RHUt01_empleado.Where(x => x.idEmpleado == usuario.idEmpleado).ToList();
                                        var idper = empleado[0].idPersona;
                                        var persona = db.RHUt09_persona.Where(x => x.idPersona == idper).ToList();
                                        var nombre = persona[0].apellidoPaterno + " " + persona[0].apellidoMaterno + ", " + persona[0].nombrePersona;
                                        string espacio1 = "Código de Cita Reprogramada";
                                        string espacio2 = "Empleado";
                                        string espacio3 = "Fecha de Modificación";
                                        if (System.IO.File.Exists(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(espacio1.PadRight(30) + "|" + espacio2.PadRight(250) + "|" + espacio3.PadRight(25));
                                                auditoria.WriteLine("---------------------------".PadRight(30) + "|" + "--------".PadRight(250) + "|" + "---------------------".PadRight(25));
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaReprogramada).ToList();
                                        var idEstadocita = estadoCita[0].idEstadoCita;
                                        ocita.idEstadoCita = idEstadocita;
                                        ocita.esReprogramado = true;
                                        db.SaveChanges();
                                        //var idPadre = SumarReprogramacion(ocita.idCitaPadre);
                                        //for (int i = 0; i >= 0; i++)
                                        //{

                                        //    if (idPadre == null)
                                        //    {
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        idPadre = SumarReprogramacion(idPadre);
                                        //    }

                                        //}

                                        int num = (from c in db.CLlt05_cita select c).Count();
                                        if (num != 0)
                                        {
                                            num = generarCodCita2();
                                        }
                                        else
                                        {
                                            num = 1;
                                        }

                                        oCita.codCita = "CT00" + (num).ToString();
                                        Session["idCita"] = oCita.idCita;
                                        //Cita reprogramada

                                        oCita.idCitaPadre = ocita.idCita;
                                        oCita.obsvCita = ocita.obsvCita;
                                        oCita.mtoTotal = ocita.mtoTotal;
                                        oCita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var estadoCitahijo = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                                        var idEstadocitahijo = estadoCitahijo[0].idEstadoCita;
                                        oCita.idEstadoCita = idEstadocitahijo;
                                        oCita.idPaciente = ocita.idPaciente;
                                        oCita.idUsuario = idUsu;
                                        oCita.fecRegistro = DateTime.Now;
                                        db.CLlt05_cita.Add(oCita);
                                        db.SaveChanges();
                                        ocita.oListCitaDtl = db.CLlt06_citaDtl.Where(x => x.idCita == ocita.idCita).ToList();
                                        CLlt06_citaDtl oCitaDtl;
                                        for (int i = 0; i < ocita.oListCitaDtl.Count; i++)
                                        {
                                            oCitaDtl = new CLlt06_citaDtl();
                                            //oCitaDtl.fecRegistro = DateTime.Now;
                                            oCitaDtl.precio = ocita.oListCitaDtl[i].precio;
                                            oCitaDtl.cantidad = ocita.oListCitaDtl[i].cantidad;
                                            oCitaDtl.esGratis = false;
                                            oCitaDtl.activo = true;
                                            oCitaDtl.idCita = oCita.idCita;
                                            oCitaDtl.idServicio = ocita.oListCitaDtl[i].idServicio;
                                            db.CLlt06_citaDtl.Add(oCitaDtl);
                                            db.SaveChanges();
                                        }
                                        dbTransaction.Commit();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {

                                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                        //ya existe el horario asigando
                                    }
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    
                                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    //el empleado no trabaja en el rango de hora insertado
                                }
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //el empleado no trabaja ese dia
                            }
                            return RedirectToAction("Index");
                        }
                        if (oCita.fecCita.DayOfWeek.ToString() == "Thursday")
                        {

                            var labora = horario[0].laboraJueves;
                            if (labora == true)
                            {
                                var horainiciojueves = TimeSpan.Parse(horario[0].horaInicioJueves.ToString());
                                var horafinjueves = TimeSpan.Parse(horario[0].horaFinJueves.ToString());
                                if (horainiciojueves <= fecInicio.TimeOfDay && horafinjueves >= fecFin.TimeOfDay)
                                {
                                    if (disponibe.Count == 0)
                                    {
                                        //Cita PADRE
                                        var ocita = db.CLlt05_cita.Single(x => x.idCita == oCita.idCita);
                                        CodSystem oCodsystem = new CodSystem();
                                        ocita.fecModificacion = DateTime.Now;
                                        var idUsu = User.Identity.GetUserId();
                                        ocita.idUsuarioModificar = idUsu;
                                        //ocita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var usuario = db.AspNetUsers.Find(idUsu);
                                        var empleado = db.RHUt01_empleado.Where(x => x.idEmpleado == usuario.idEmpleado).ToList();
                                        var idper = empleado[0].idPersona;
                                        var persona = db.RHUt09_persona.Where(x => x.idPersona == idper).ToList();
                                        var nombre = persona[0].apellidoPaterno + " " + persona[0].apellidoMaterno + ", " + persona[0].nombrePersona;
                                        string espacio1 = "Código de Cita Reprogramada";
                                        string espacio2 = "Empleado";
                                        string espacio3 = "Fecha de Modificación";
                                        if (System.IO.File.Exists(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(espacio1.PadRight(30) + "|" + espacio2.PadRight(250) + "|" + espacio3.PadRight(25));
                                                auditoria.WriteLine("---------------------------".PadRight(30) + "|" + "--------".PadRight(250) + "|" + "---------------------".PadRight(25));
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaReprogramada).ToList();
                                        var idEstadocita = estadoCita[0].idEstadoCita;
                                        ocita.idEstadoCita = idEstadocita;
                                        ocita.esReprogramado = true;
                                        db.SaveChanges();
                                        //var idPadre = SumarReprogramacion(ocita.idCitaPadre);
                                        //for (int i = 0; i >= 0; i++)
                                        //{

                                        //    if (idPadre == null)
                                        //    {
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        idPadre = SumarReprogramacion(idPadre);
                                        //    }

                                        //}

                                        int num = (from c in db.CLlt05_cita select c).Count();
                                        if (num != 0)
                                        {
                                            num = generarCodCita2();
                                        }
                                        else
                                        {
                                            num = 1;
                                        }

                                        oCita.codCita = "CT00" + (num).ToString();
                                        Session["idCita"] = oCita.idCita;
                                        //Cita reprogramada

                                        oCita.idCitaPadre = ocita.idCita;
                                        oCita.obsvCita = ocita.obsvCita;
                                        oCita.mtoTotal = ocita.mtoTotal;
                                        oCita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var estadoCitahijo = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                                        var idEstadocitahijo = estadoCitahijo[0].idEstadoCita;
                                        oCita.idEstadoCita = idEstadocitahijo;
                                        oCita.idPaciente = ocita.idPaciente;
                                        oCita.idUsuario = idUsu;
                                        oCita.fecRegistro = DateTime.Now;
                                        db.CLlt05_cita.Add(oCita);
                                        db.SaveChanges();
                                        ocita.oListCitaDtl = db.CLlt06_citaDtl.Where(x => x.idCita == ocita.idCita).ToList();
                                        CLlt06_citaDtl oCitaDtl;
                                        for (int i = 0; i < ocita.oListCitaDtl.Count; i++)
                                        {
                                            oCitaDtl = new CLlt06_citaDtl();
                                            //oCitaDtl.fecRegistro = DateTime.Now;
                                            oCitaDtl.precio = ocita.oListCitaDtl[i].precio;
                                            oCitaDtl.cantidad = ocita.oListCitaDtl[i].cantidad;
                                            oCitaDtl.esGratis = false;
                                            oCitaDtl.activo = true;
                                            oCitaDtl.idCita = oCita.idCita;
                                            oCitaDtl.idServicio = ocita.oListCitaDtl[i].idServicio;
                                            db.CLlt06_citaDtl.Add(oCitaDtl);
                                            db.SaveChanges();
                                        }
                                        dbTransaction.Commit();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        
                                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                        //ya existe el horario asigando
                                    }
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    //el empleado no trabaja en el rango de hora insertado
                                }
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //el empleado no trabaja ese dia
                            }
                            return RedirectToAction("Index");
                        }
                        if (oCita.fecCita.DayOfWeek.ToString() == "Friday")
                        {

                            var labora = horario[0].laboraViernes;
                            if (labora == true)
                            {
                                var horainicioviernes = TimeSpan.Parse(horario[0].horaInicioViernes.ToString());
                                var horafinviernes = TimeSpan.Parse(horario[0].horaFinViernes.ToString());
                                if (horainicioviernes <= fecInicio.TimeOfDay && horafinviernes >= fecFin.TimeOfDay)
                                {
                                    if (disponibe.Count == 0)
                                    {
                                        //Cita PADRE
                                        var ocita = db.CLlt05_cita.Single(x => x.idCita == oCita.idCita);
                                        CodSystem oCodsystem = new CodSystem();
                                        ocita.fecModificacion = DateTime.Now;
                                        var idUsu = User.Identity.GetUserId();
                                        ocita.idUsuarioModificar = idUsu;
                                        //ocita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var usuario = db.AspNetUsers.Find(idUsu);
                                        var empleado = db.RHUt01_empleado.Where(x => x.idEmpleado == usuario.idEmpleado).ToList();
                                        var idper = empleado[0].idPersona;
                                        var persona = db.RHUt09_persona.Where(x => x.idPersona == idper).ToList();
                                        var nombre = persona[0].apellidoPaterno + " " + persona[0].apellidoMaterno + ", " + persona[0].nombrePersona;
                                        string espacio1 = "Código de Cita Reprogramada";
                                        string espacio2 = "Empleado";
                                        string espacio3 = "Fecha de Modificación";
                                        if (System.IO.File.Exists(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(espacio1.PadRight(30) + "|" + espacio2.PadRight(250) + "|" + espacio3.PadRight(25));
                                                auditoria.WriteLine("---------------------------".PadRight(30) + "|" + "--------".PadRight(250) + "|" + "---------------------".PadRight(25));
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaReprogramada).ToList();
                                        var idEstadocita = estadoCita[0].idEstadoCita;
                                        ocita.idEstadoCita = idEstadocita;
                                        ocita.esReprogramado = true;
                                        db.SaveChanges();
                                        //var idPadre = SumarReprogramacion(ocita.idCitaPadre);
                                        //for (int i = 0; i >= 0; i++)
                                        //{

                                        //    if (idPadre == null)
                                        //    {
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        idPadre = SumarReprogramacion(idPadre);
                                        //    }

                                        //}

                                        int num = (from c in db.CLlt05_cita select c).Count();
                                        if (num != 0)
                                        {
                                            num = generarCodCita2();
                                        }
                                        else
                                        {
                                            num = 1;
                                        }

                                        oCita.codCita = "CT00" + (num).ToString();
                                        Session["idCita"] = oCita.idCita;
                                        //Cita reprogramada

                                        oCita.idCitaPadre = ocita.idCita;
                                        oCita.obsvCita = ocita.obsvCita;
                                        oCita.mtoTotal = ocita.mtoTotal;
                                        oCita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var estadoCitahijo = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                                        var idEstadocitahijo = estadoCitahijo[0].idEstadoCita;
                                        oCita.idEstadoCita = idEstadocitahijo;
                                        oCita.idPaciente = ocita.idPaciente;
                                        oCita.idUsuario = idUsu;
                                        oCita.fecRegistro = DateTime.Now;
                                        db.CLlt05_cita.Add(oCita);
                                        db.SaveChanges();
                                        ocita.oListCitaDtl = db.CLlt06_citaDtl.Where(x => x.idCita == ocita.idCita).ToList();
                                        CLlt06_citaDtl oCitaDtl;
                                        for (int i = 0; i < ocita.oListCitaDtl.Count; i++)
                                        {
                                            oCitaDtl = new CLlt06_citaDtl();
                                            //oCitaDtl.fecRegistro = DateTime.Now;
                                            oCitaDtl.precio = ocita.oListCitaDtl[i].precio;
                                            oCitaDtl.cantidad = ocita.oListCitaDtl[i].cantidad;
                                            oCitaDtl.esGratis = false;
                                            oCitaDtl.activo = true;
                                            oCitaDtl.idCita = oCita.idCita;
                                            oCitaDtl.idServicio = ocita.oListCitaDtl[i].idServicio;
                                            db.CLlt06_citaDtl.Add(oCitaDtl);
                                            db.SaveChanges();
                                        }
                                        dbTransaction.Commit();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        
                                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                        //ya existe el horario asigando
                                    }
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    
                                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    //el empleado no trabaja en el rango de hora insertado
                                }
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //el empleado no trabaja ese dia
                            }
                            return RedirectToAction("Index");
                        }

                        if (oCita.fecCita.DayOfWeek.ToString() == "Saturday")
                        {

                            var labora = horario[0].laboraSabado;
                            if (labora == true)
                            {
                                var horainiciosabado = TimeSpan.Parse(horario[0].horaInicioSabado.ToString());
                                var horafinsabado = TimeSpan.Parse(horario[0].horaFinSabado.ToString());
                                if (horainiciosabado <= fecInicio.TimeOfDay && horafinsabado >= fecFin.TimeOfDay)
                                {
                                    if (disponibe.Count == 0)
                                    {
                                        //Cita PADRE
                                        var ocita = db.CLlt05_cita.Single(x => x.idCita == oCita.idCita);
                                        CodSystem oCodsystem = new CodSystem();
                                        ocita.fecModificacion = DateTime.Now;
                                        var idUsu = User.Identity.GetUserId();
                                        ocita.idUsuarioModificar = idUsu;
                                        //ocita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var usuario = db.AspNetUsers.Find(idUsu);
                                        var empleado = db.RHUt01_empleado.Where(x => x.idEmpleado == usuario.idEmpleado).ToList();
                                        var idper = empleado[0].idPersona;
                                        var persona = db.RHUt09_persona.Where(x => x.idPersona == idper).ToList();
                                        var nombre = persona[0].apellidoPaterno + " " + persona[0].apellidoMaterno + ", " + persona[0].nombrePersona;
                                        string espacio1 = "Código de Cita Reprogramada";
                                        string espacio2 = "Empleado";
                                        string espacio3 = "Fecha de Modificación";
                                        if (System.IO.File.Exists(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(espacio1.PadRight(30) + "|" + espacio2.PadRight(250) + "|" + espacio3.PadRight(25));
                                                auditoria.WriteLine("---------------------------".PadRight(30) + "|" + "--------".PadRight(250) + "|" + "---------------------".PadRight(25));
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaReprogramada).ToList();
                                        var idEstadocita = estadoCita[0].idEstadoCita;
                                        ocita.idEstadoCita = idEstadocita;
                                        ocita.esReprogramado = true;
                                        db.SaveChanges();
                                        //var idPadre = SumarReprogramacion(ocita.idCitaPadre);
                                        //for (int i = 0; i >= 0; i++)
                                        //{

                                        //    if (idPadre == null)
                                        //    {
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        idPadre = SumarReprogramacion(idPadre);
                                        //    }

                                        //}

                                        int num = (from c in db.CLlt05_cita select c).Count();
                                        if (num != 0)
                                        {
                                            num = generarCodCita2();
                                        }
                                        else
                                        {
                                            num = 1;
                                        }

                                        oCita.codCita = "CT00" + (num).ToString();
                                        Session["idCita"] = oCita.idCita;

                                        //Cita reprogramada
                                        oCita.idCitaPadre = ocita.idCita;
                                        oCita.obsvCita = ocita.obsvCita;
                                        oCita.mtoTotal = ocita.mtoTotal;
                                        oCita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var estadoCitahijo = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                                        var idEstadocitahijo = estadoCitahijo[0].idEstadoCita;
                                        oCita.idEstadoCita = idEstadocitahijo;
                                        oCita.idPaciente = ocita.idPaciente;
                                        oCita.idUsuario = idUsu;
                                        oCita.fecRegistro = DateTime.Now;
                                        db.CLlt05_cita.Add(oCita);
                                        db.SaveChanges();
                                        ocita.oListCitaDtl = db.CLlt06_citaDtl.Where(x => x.idCita == ocita.idCita).ToList();
                                        CLlt06_citaDtl oCitaDtl;
                                        for (int i = 0; i < ocita.oListCitaDtl.Count; i++)
                                        {
                                            oCitaDtl = new CLlt06_citaDtl();
                                            //oCitaDtl.fecRegistro = DateTime.Now;
                                            oCitaDtl.precio = ocita.oListCitaDtl[i].precio;
                                            oCitaDtl.cantidad = ocita.oListCitaDtl[i].cantidad;
                                            oCitaDtl.esGratis = false;
                                            oCitaDtl.activo = true;
                                            oCitaDtl.idCita = oCita.idCita;
                                            oCitaDtl.idServicio = ocita.oListCitaDtl[i].idServicio;
                                            db.CLlt06_citaDtl.Add(oCitaDtl);
                                            db.SaveChanges();
                                        }
                                        dbTransaction.Commit();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        
                                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                        //ya existe el horario asigando
                                    }
                                    return RedirectToAction("Index");
                                }
                                else
                                {

                                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    //el empleado no trabaja en el rango de hora insertado
                                }
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //el empleado no trabaja ese dia
                            }
                            return RedirectToAction("Index");
                        }
                        if (oCita.fecCita.DayOfWeek.ToString() == "Sunday")
                        {


                            var labora = horario[0].laboraDomingo;
                            if (labora == true)
                            {
                                var horainiciodomingo = TimeSpan.Parse(horario[0].horaInicioDomingo);
                                var horafindomingo = TimeSpan.Parse(horario[0].horaFinDomingo);
                                if (horainiciodomingo <= fecInicio.TimeOfDay && horafindomingo >= fecFin.TimeOfDay)
                                {
                                    if (disponibe.Count == 0)
                                    {
                                        //Cita PADRE
                                        var ocita = db.CLlt05_cita.Single(x => x.idCita == oCita.idCita);

                                        ocita.fecModificacion = DateTime.Now;
                                        var idUsu = User.Identity.GetUserId();
                                        ocita.idUsuarioModificar = idUsu;
                                        //ocita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var usuario = db.AspNetUsers.Find(idUsu);
                                        var empleado = db.RHUt01_empleado.Where(x => x.idEmpleado == usuario.idEmpleado).ToList();
                                        var idper = empleado[0].idPersona;
                                        var persona = db.RHUt09_persona.Where(x => x.idPersona == idper).ToList();
                                        var nombre = persona[0].apellidoPaterno + " " + persona[0].apellidoMaterno + ", " + persona[0].nombrePersona;
                                        string espacio1 ="Código de Cita Reprogramada";
                                        string espacio2 = "Empleado";
                                        string espacio3 = "Fecha de Modificación";
                                        if (System.IO.File.Exists(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|"+ nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        else
                                        {
                                            using (StreamWriter auditoria = System.IO.File.AppendText(@"C:\inetpub\Asiri\AuditoriaCita.txt"))
                                            {
                                                auditoria.WriteLine(espacio1.PadRight(30) + "|" + espacio2.PadRight(250) + "|" + espacio3.PadRight(25));
                                                auditoria.WriteLine("---------------------------".PadRight(30) + "|" + "--------".PadRight(250) + "|" + "---------------------".PadRight(25));
                                                auditoria.WriteLine(ocita.codCita.PadRight(30) + "|" + nombre.PadRight(250) + "|" + ocita.fecModificacion.ToString().PadRight(25));
                                            }
                                        }
                                        var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaReprogramada).ToList();
                                        var idEstadocita = estadoCita[0].idEstadoCita;
                                        ocita.idEstadoCita = idEstadocita;
                                        ocita.esReprogramado = true;
                                        db.SaveChanges();
                                        //var idPadre = SumarReprogramacion(ocita.idCitaPadre);
                                        //for (int i = 0; i >= 0; i++)
                                        //{

                                        //    if (idPadre == null)
                                        //    {
                                        //        break;
                                        //    }
                                        //    else
                                        //    {
                                        //        idPadre = SumarReprogramacion(idPadre);
                                        //    }

                                        //}

                                        int num = (from c in db.CLlt05_cita select c).Count();
                                        if (num != 0)
                                        {
                                            num = generarCodCita2();
                                        }
                                        else
                                        {
                                            num = 1;
                                        }

                                        oCita.codCita = "CT00" + (num).ToString();
                                        Session["idCita"] = oCita.idCita;
                                        //Cita reprogramada

                                        oCita.idCitaPadre = ocita.idCita;
                                        oCita.obsvCita = ocita.obsvCita;
                                        oCita.mtoTotal = ocita.mtoTotal;
                                        oCita.numReprogramacion = (byte)(Convert.ToInt16(ocita.numReprogramacion + 1));
                                        var estadoCitahijo = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                                        var idEstadocitahijo = estadoCitahijo[0].idEstadoCita;
                                        oCita.idEstadoCita = idEstadocitahijo;
                                        oCita.idPaciente = ocita.idPaciente;
                                        oCita.idUsuario = idUsu;
                                        oCita.fecRegistro = DateTime.Now;
                                        db.CLlt05_cita.Add(oCita);
                                        db.SaveChanges();
                                        ocita.oListCitaDtl = db.CLlt06_citaDtl.Where(x => x.idCita == ocita.idCita).ToList();
                                        CLlt06_citaDtl oCitaDtl;
                                        for (int i = 0; i < ocita.oListCitaDtl.Count; i++)
                                        {
                                            oCitaDtl = new CLlt06_citaDtl();
                                            //oCitaDtl.fecRegistro = DateTime.Now;
                                            oCitaDtl.precio = ocita.oListCitaDtl[i].precio;
                                            oCitaDtl.cantidad = ocita.oListCitaDtl[i].cantidad;
                                            oCitaDtl.esGratis = false;
                                            oCitaDtl.activo = true;
                                            oCitaDtl.idCita = oCita.idCita;
                                            oCitaDtl.idServicio = ocita.oListCitaDtl[i].idServicio;
                                            db.CLlt06_citaDtl.Add(oCitaDtl);
                                            db.SaveChanges();
                                        }
                                        dbTransaction.Commit();
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        
                                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-close', message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                        //ya existe el horario asigando
                                    }
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                    //el empleado no trabaja en el rango de hora insertado
                                }
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //el empleado no trabaja ese dia
                            }
                            dbTransaction.Commit();
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-close',message: 'El empleado no tiene un horario asignado'}, {type: 'danger',allow_dismiss: false});});</script>";
                    }

                }
                catch (Exception ex)
                {

                    dbTransaction.Rollback();
                }
            }
            return View();
        }

       

        //Busca el paciente por numero documento
        public ActionResult BuscarPacientePorND(string numeroDocumento)
        {
            var ListaPaciente = db.RHUt09_persona.Where(x => x.numDocIdentidad == numeroDocumento).ToList();
            return View();
        }
        // GET: CLlt05_cita/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLlt05_cita cLlt05_cita = db.CLlt05_cita.Find(id);
            if (cLlt05_cita == null)
            {
                return HttpNotFound();
            }
            return View(cLlt05_cita);
        }

        public ActionResult Calendario()
        {
            return View();
        }

        // GET: CLlt05_cita/Create
        
        public ActionResult Create()
        {
            ViewBag.idEstadoCita = new SelectList(db.CLlt09_estadoCita, "idEstadoCita", "descEstadoCita");
            //ViewBag.idEmpleado = new SelectList(db.RHUt01_empleado, "idEmpleado", "codEmpleado");
            List<RHUt01_empleado> oli = new List<RHUt01_empleado>();
            ViewBag.idEmpleado = new SelectList(oli, "idEmpleado", "codEmpleado");
            ViewBag.idServicio = new SelectList(db.PROt04_servicio, "idServicio", "nombreServicio");
            //ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad., "idEspecialidad", "nombreEspecialidad");
            List<RHUt04_especialidad> oliE = new List<RHUt04_especialidad>();
            ViewBag.idEspecialidad = new SelectList(oliE, "idEspecialidad", "nombreEspecialidad");
            ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "nombreEspecialidad");
            var ListaServicio = db.PROt04_servicio.ToList();
            ViewBag.precio = ListaServicio[1].precio.ToString();
            return View();
        }



        public JsonResult getEspecialidad(string Id)
        {
            if (Id != "")
            {
                //getEmpleado("");
                var Especialidad = db.RHUt04_especialidad.ToList().Where(p => p.idTipoDeEspeciliadad == int.Parse(Id));
                return Json(new SelectList(Especialidad, "idEspecialidad", "nombreEspecialidad"));
            }
            else
            {
                return Json("");
            }
        }

        public JsonResult getEmpleado(string Id)
        {
            if (Id != "")
            {
                var Empleado = db.RHUt02_empleadoEspecialidad.ToList().Where(p => p.idEspecialidad == int.Parse(Id)).Select(
                    s => new { idEmpleado = s.idEmpleado, d = s.RHUt01_empleado.RHUt09_persona.apellidoPaterno + " " + s.RHUt01_empleado.RHUt09_persona.apellidoMaterno + ", " + s.RHUt01_empleado.RHUt09_persona.nombrePersona }
                    );

                return Json(new SelectList(Empleado, "idEmpleado", "d"));
            }
            else
            {
                return Json("");
            }
        }

        public JsonResult getServicio(string Id)
        {
            if (Id != "")
            {
                //getEmpleado("");
                var Servicio = db.PROt04_servicio.ToList().Where(p => p.idEspecialidad == int.Parse(Id));
                return Json(new SelectList(Servicio, "idServicio", "nombreServicio"));
            }
            else
            {
                return Json("");
            }
        }


        //Obtiene el servicio y manda el precio
        
        [HttpGet]
        public ActionResult ObtenerServicio(int idServicio)
        {
            var ListaServicio = db.PROt04_servicio.Where(x => x.idServicio == idServicio).ToList();
            var prec = ListaServicio[0].precio;
            var esgrt = ListaServicio[0].esGratis;
            if (esgrt == true)
            {
                prec = 0;
            }
            ViewBag.precio = prec.ToString();
            ViewBag.esGratis = esgrt;
            var servicio = new PROt04_servicio();
            servicio.precio = prec;
            servicio.esGratis =esgrt;
            return Json(servicio, JsonRequestBehavior.AllowGet);
        }

        //Crear Cita Medica
        [HttpPost]
        
        public ActionResult Create(CLlt05_cita oCita)
        {
            if (oCita.idEmpleado == 0)
            {
                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: 'Debe Seleccionar un Doctor'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("Create");
            }
            var citas = Appointment.GetCitasxDia();
            if (citas != "")
            {
                
                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: '" + citas + "'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("Create");
            }

            var fechaActual = DateTime.Today;
            TimeSpan horaActual = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString());
            var estado = db.CLlt09_estadoCita.SingleOrDefault(x => x.codEstadoCita == CodSystem.CitaPendiente);
            oCita.idEstadoCita = estado.idEstadoCita;
            var FechaValid = db.CLlt05_cita.Where(x => x.fecCita == oCita.fecCita).ToList();
            
            var day = new CultureInfo("es-ES");
            day.DateTimeFormat.GetDayName(oCita.fecCita.DayOfWeek);
            

            var horario = db.RHUt06_horario.Where(x => x.idEmpleado == oCita.idEmpleado).ToList();
            if (horario.Count != 0)
            {
                DateTime fecInicio;
                fecInicio = oCita.fecCita.Add(TimeSpan.Parse(oCita.horaInicio));
                DateTime fecFin = fecInicio.AddMinutes(double.Parse(oCita.duracionEstimada));
                //Parametro
                var parametroCita = oCita.fecCita.Add(TimeSpan.Parse(Appointment.HoraFinxDia()));
                if (fecFin > parametroCita)
                {
                    var hora12 = oCita.fecCita.AddDays(1);
                    
                    if (fecFin >= hora12)
                    {
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'Error, No se puede generar una cita con 2 días distintos'}, {type: 'danger',allow_dismiss: false});});</script>";
                        return RedirectToAction("Create");
                    }    
                    
                    
                    
                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'No se puede generar citas a partir de " + parametroCita.ToString("hh:mm tt") + "'}, {type: 'danger',allow_dismiss: false});});</script>";
                    return RedirectToAction("Create");
                }

                




                //TimeSpan horainicio = TimeSpan.Parse(oCita.horaInicio);
                //TimeSpan duracion = TimeSpan.Parse("00:" + oCita.duracionEstimada);
                //var horafin = TimeSpan.Parse(oCita.horaInicio) + TimeSpan.Parse("00:" + oCita.duracionEstimada);
                CitaDA oCitaDA = new CitaDA();
                var disponibe = oCitaDA.verificarCita(oCita);

                if (oCita.fecCita == DateTime.Today && fecInicio.TimeOfDay <= horaActual)
                {
                    
                    TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'La hora es incorrecta'}, {type: 'danger',allow_dismiss: false});});</script>";
                    return RedirectToAction("Create");
                }

                if (oCita.fecCita.DayOfWeek.ToString() == "Monday")
                {

                    var labora = horario[0].laboraLunes;
                    if (labora == true)
                    {
                        var horainiciolunes = TimeSpan.Parse(horario[0].horaInicioLunes);
                        var horafinlunes = TimeSpan.Parse(horario[0].horaFinLunes);
                        if (horainiciolunes <= fecInicio.TimeOfDay && horafinlunes >= fecFin.TimeOfDay)
                        {
                            if (disponibe.Count == 0)
                            {
                                return RedirectToAction("GenerarCita", oCita);
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //ya existe el horario asigando
                            }
                        }
                        else
                        {
                            
                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                            //el empleado no trabaja en el rango de hora insertado
                        }
                    }
                    else
                    {

                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                        //el empleado no trabaja ese dia
                    }
                }


                if (oCita.fecCita.DayOfWeek.ToString() == "Tuesday")
                {

                    var labora = horario[0].laboraMartes;
                    if (labora == true)
                    {
                        var horainiciomartes = TimeSpan.Parse(horario[0].horaInicioMartes);
                        var horafinmartes = TimeSpan.Parse(horario[0].horaFinMartes);
                        if (horainiciomartes <= fecInicio.TimeOfDay && horafinmartes >= fecFin.TimeOfDay)
                        {
                            if (disponibe.Count == 0)
                            {
                                return RedirectToAction("GenerarCita", oCita);
                            }
                            else
                            {
                                

                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'Ya existe el horario asignado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //ya existe el horario asigando
                            }
                        }
                        else
                        {

                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                            //el empleado no trabaja en el rango de hora insertado
                        }
                    }
                    else
                    {
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                        //el empleado no trabaja ese dia
                    }
                }
                if (oCita.fecCita.DayOfWeek.ToString() == "Wednesday")
                {

                    var labora = horario[0].laboraMiercoles;
                    if (labora == true)
                    {
                        var horainiciomiercoles = TimeSpan.Parse(horario[0].horaInicioMiercoles);
                        var horafinmiercoles = TimeSpan.Parse(horario[0].horaFinMiercoles);
                        if (horainiciomiercoles <= fecInicio.TimeOfDay && horafinmiercoles >= fecFin.TimeOfDay)
                        {
                            if (disponibe.Count == 0)
                            {
                                return RedirectToAction("GenerarCita", oCita);
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'Ya existe el horario asignado'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //ya existe el horario asigando
                            }
                        }
                        else
                        {
                            
                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                            //el empleado no trabaja en el rango de hora insertado
                        }
                    }
                    else
                    {
                        
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                        //el empleado no trabaja ese dia
                    }
                }
                if (oCita.fecCita.DayOfWeek.ToString() == "Thursday")
                {

                    var labora = horario[0].laboraJueves;
                    if (labora == true)
                    {
                        var horainiciojueves = TimeSpan.Parse(horario[0].horaInicioJueves);
                        var horafinjueves = TimeSpan.Parse(horario[0].horaFinJueves);
                        if (horainiciojueves <= fecInicio.TimeOfDay  && horafinjueves >= fecFin.TimeOfDay)
                        {
                            if (disponibe.Count == 0)
                            {
                                return RedirectToAction("GenerarCita", oCita);
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //ya existe el horario asigando
                            }
                        }
                        else
                        {
                            
                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                            //el empleado no trabaja en el rango de hora insertado
                        }
                    }
                    else
                    {
                        
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                        //el empleado no trabaja ese dia
                    }
                }
                if (oCita.fecCita.DayOfWeek.ToString() == "Friday")
                {

                    var labora = horario[0].laboraViernes;
                    if (labora == true)
                    {
                        var horainicioviernes = TimeSpan.Parse(horario[0].horaInicioViernes);
                        var horafinviernes = TimeSpan.Parse(horario[0].horaFinViernes);
                        if (horainicioviernes <= fecInicio.TimeOfDay && horafinviernes >= fecFin.TimeOfDay)
                        {
                            if (disponibe.Count == 0)
                            {
                                return RedirectToAction("GenerarCita", oCita);

                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //ya existe el horario asigando
                            }
                        }
                        else
                        {
                            
                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                            //el empleado no trabaja en el rango de hora insertado
                        }
                    }
                    else
                    {
                        
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                        //el empleado no trabaja ese dia
                    }
                }
                if (oCita.fecCita.DayOfWeek.ToString() == "Saturday")
                {

                    var labora = horario[0].laboraSabado;
                    if (labora == true)
                    {
                        var horainiciosabado = TimeSpan.Parse(horario[0].horaInicioSabado);
                        var horafinsabado = TimeSpan.Parse(horario[0].horaFinSabado);
                        if (horainiciosabado <= fecInicio.TimeOfDay && horafinsabado >= fecFin.TimeOfDay)
                        {
                            if (disponibe.Count == 0)
                            {
                                return RedirectToAction("GenerarCita", oCita);

                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //ya existe el horario asigando
                            }
                        }
                        else
                        {
                            
                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                            //el empleado no trabaja en el rango de hora insertado
                        }
                    }
                    else
                    {
                        
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({icon: 'fa fa-lg fa-check', message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                        //el empleado no trabaja ese dia
                    }
                }
                if (oCita.fecCita.DayOfWeek.ToString() == "Sunday")
                {

                    var labora = horario[0].laboraDomingo;
                    if (labora == true)
                    {
                        var horainiciodomingo = TimeSpan.Parse(horario[0].horaInicioDomingo);
                        var horafindomingo = TimeSpan.Parse(horario[0].horaFinDomingo);
                        if (horainiciodomingo <= fecInicio.TimeOfDay && horafindomingo >= fecFin.TimeOfDay)
                        {
                            if (disponibe.Count == 0)
                            {
                                return RedirectToAction("GenerarCita", oCita);
                            }
                            else
                            {
                                
                                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'Ya existe el horario asigando'}, {type: 'danger',allow_dismiss: false});});</script>";
                                //ya existe el horario asigando
                            }
                        }
                        else
                        {
                            
                            TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'El empleado no trabaja en el rango de hora insertado'}, {type: 'danger',allow_dismiss: false});});</script>";
                            //el empleado no trabaja en el rango de hora insertado
                        }
                    }
                    else
                    {
                        
                        TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'El empleado no trabaja ese dia'}, {type: 'danger',allow_dismiss: false});});</script>";
                        //el empleado no trabaja ese dia
                    }
                }
            }
            else
            {
                
                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ icon: 'fa fa-lg fa-check',message: 'El empleado no tiene un horario asignado'}, {type: 'danger',allow_dismiss: false});});</script>";
            }




            return RedirectToAction("Create");

        }

        
        public ActionResult GenerarCita(CLlt05_cita oCita)
        {
            ViewBag.idEstadoCita = new SelectList(db.CLlt09_estadoCita, "idEstadoCita", "descEstadoCita");
            ViewBag.idEmpleado = new SelectList(db.RHUt01_empleado, "idEmpleado", "codEmpleado");
            var empleado = db.RHUt01_empleado.Find(oCita.idEmpleado);
            ViewBag.idPaciente = new SelectList(db.RHUt07_paciente.Where(x => x.RHUt09_persona.idPersona != empleado.idPersona).Select
                (s => new { idPaciente = s.idPaciente, d = s.RHUt09_persona.apellidoPaterno + " " + s.RHUt09_persona.apellidoMaterno + ", " + s.RHUt09_persona.nombrePersona }
                    ), "idPaciente", "d");
            ViewBag.idServicio = new SelectList(db.PROt04_servicio.Where(x => x.idEspecialidad == oCita.idEspecialidad), "idServicio", "nombreServicio");
            ViewBag.idEspecialidad = new SelectList(db.RHUt04_especialidad, "idEspecialidad", "nombreEspecialidad");
            ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "nombreEspecialidad");
            var ListaServicio = db.PROt04_servicio.Where(x => x.idEspecialidad == oCita.idEspecialidad).ToList();
            var prec = ListaServicio[0].precio.ToString("F");
            var esgrt = ListaServicio[0].esGratis;
            if (esgrt == true)
            {
                prec = "0.00";
            }
            ViewBag.precio = prec;
            ViewBag.esGratis = esgrt;
            return View(oCita);
        }
        private int generarCodCita()
        {
            CLlt05_cita modelo = (from c in db.CLlt05_cita orderby c.idCita descending select c).First();
            string codigocita = modelo.codCita.Substring(4);
            if (codigocita != null)
            {
                int num = int.Parse(codigocita);
                return ++num;
            }
            else
            {
                return 1;
            }
        }

        [HttpPost]
        public JsonResult generarCita(CLlt05_cita oCita)
        {
            var respuesta = new Common.ResponseModel
            {
                respuesta = true,
                redirect = "/Cita/GenerarCita" + oCita,
                error = ""
            };
            int num = (from c in db.CLlt05_cita select c).Count();
            if (num != 0)
            {
                num = generarCodCita();
            }
            else
            {
                num = 1;
            }

            oCita.codCita = "CT00" + (num).ToString();
            Session["idCita"] = oCita.idCita;
            using (var dbTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    oCita.esOnline = false;
                    oCita.fecRegistro = DateTime.Now;
                    oCita.numReprogramacion = 0;
                    oCita.esCerrado = false;
                    var idUsu = User.Identity.GetUserId();
                    var estadoCita = db.CLlt09_estadoCita.Where(x => x.codEstadoCita == CodSystem.CitaPendiente).ToList();
                    var idEstadocita = estadoCita[0].idEstadoCita;
                    oCita.idUsuario = idUsu;
                    oCita.idEstadoCita = idEstadocita;
                    if (ModelState.IsValid && oCita.oListCitaDtl.Count != 0)
                    {

                        db.CLlt05_cita.Add(oCita);
                        db.SaveChanges();
                        CLlt06_citaDtl oCitaDtl = new CLlt06_citaDtl();
                        for (int i = 0; i < oCita.oListCitaDtl.Count; i++)
                        {
                            //oCitaDtl.fecRegistro = DateTime.Now;
                            oCitaDtl.precio = oCita.oListCitaDtl[i].precio;
                            oCitaDtl.cantidad = oCita.oListCitaDtl[i].cantidad;
                            oCitaDtl.esGratis = oCita.oListCitaDtl[i].esGratis;
                            oCitaDtl.activo = true;
                            oCitaDtl.idCita = oCita.idCita;
                            oCitaDtl.idServicio = oCita.oListCitaDtl[i].idServicio;
                            //oCitaDtl.idUsuario = 1;

                            db.CLlt06_citaDtl.Add(oCitaDtl);
                            db.SaveChanges();
                        }

                        respuesta.redirect = "/Cita/Index";

                    }
                    else
                    {
                        bool estado = ModelState.IsValid;
                        if (oCita.oListCitaDtl.Count == 0)
                        {
                            ModelState.AddModelError("detalle", "Debe agregar un detalle para la cita");
                            respuesta.respuesta = false;
                            respuesta.error = "Debe agregar un detalle para la cita";

                        }
                        if (estado == false)
                        {
                            ModelState.AddModelError("detalle", "Debe completar todos los campos necesarios");
                            respuesta.respuesta = false;
                            respuesta.error = "Debe completar todos los campos necesarios";

                        }
                        if (oCita.oListCitaDtl.Count == 0 && estado == false)
                        {
                            ModelState.AddModelError("detalle", "Debe completar todos los campos necesarios");
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
