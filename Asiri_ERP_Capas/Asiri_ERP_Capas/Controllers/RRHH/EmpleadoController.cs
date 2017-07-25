using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Common;
using Microsoft.AspNet.Identity;
using Model;
using Service.RRHH;
using Common.ViewModel;
using Asiri_ERP_Capas.DA;
using Common.Helper;
using System.IO;

namespace Asiri_ERP_Capas.Controllers.RRHH
{

    public class EmpleadoController : Controller
    {

        private AsiriContext db = new AsiriContext();
        private PersonaBL personaBL = new PersonaBL();
        private EmpleadoBL empleadoBL = new EmpleadoBL();

        // GET: Empleado
        public ActionResult Index()
        {

            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlEmpleado);
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

            return View(empleadoBL.Listar());
        }

        public ActionResult ObtenerImagenUser(string Id)
        {
            var idEmpl = db.AspNetUsers.Where(u => u.Id == Id).Select(z => z.idEmpleado).Single();
            var idPer = db.RHUt01_empleado.Where(p => p.idEmpleado == idEmpl).Select(s => s.idPersona).Single();
            var img = personaBL.obtenerPersona(idPer).pathFoto;
            if (img != null)
                return File(img, "image/jpg");
            else
                return File(Server.MapPath("~/Content/img/user-icon.jpg"), "image/jpg");
        }

        public ActionResult Create()
        {
            ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito");
            ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia");
            ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona");
            ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil");
            ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera");
            ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado");
            ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion");
            ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago");
            ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio");
            return View();
        }

        public byte[] convertir(HttpPostedFileBase img)
        {
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(img.InputStream))
            {
                fileData = binaryReader.ReadBytes(img.ContentLength);
            }
            return fileData;
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if u like...

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsNumeric(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }

        public bool validacionNumDocumento(int id, string num)
        {
            if (id != 0)
            {
                var cod = db.RHUt12_tipoDocIdentidad.Where(t => t.idTipoDocIdentidad == id).Select(t => t.codTipoDocIdentidad).Single();
                if (cod != null)
                {
                    switch (cod)
                    {
                        case "01":
                            {
                                if (IsNumeric(num) && num.Length == 8)
                                    return true;
                                else
                                    return false;
                            }
                        case "04":
                            {
                                if (num.Length == 12)
                                {
                                    foreach (char c in num)
                                    {
                                        if (!Char.IsLetterOrDigit(c))
                                            return false;
                                    }
                                    return true;
                                }
                                else
                                    return false;
                            }
                        case "06":
                            {
                                if (IsNumeric(num) && num.Length == 11)
                                    return true;
                                else
                                    return false;
                            }
                        case "07":
                            {
                                if (num.Length == 12)
                                {
                                    foreach (char c in num)
                                    {
                                        if (!Char.IsLetterOrDigit(c))
                                            return false;
                                    }
                                    return true;
                                }
                                else
                                    return false;
                            }
                        case "00":
                            {
                                if (num.Length == 15)
                                {
                                    foreach (char c in num)
                                    {
                                        if (!Char.IsLetterOrDigit(c))
                                            return false;
                                    }
                                    return true;
                                }
                                else
                                    return false;
                            }
                        default:
                            {
                                return false;
                            }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        [HttpPost]
        public ActionResult Create(EmpleadoVM model, HttpPostedFileBase files)
        {
            model.Persona.idUsuario = User.Identity.GetUserId();
            model.Persona.fecRegistro = DateTime.Now.Date;
            model.Persona.activo = true;
            model.Persona.esOnline = true;
            model.Persona.difunto = false;
            model.Empleado.idUsuario = User.Identity.GetUserId();
            model.Empleado.fecRegistro = DateTime.Now.Date;
            model.Empleado.activo = true;
            model.Empleado.esHorarioTurno = false;
            if (model.Persona.tipoPersoneria == "N" || model.Persona.tipoPersoneria == "J")
            {

                if (model.Persona.tipoPersoneria == "N")
                {
                    if (model.Persona.nombrePersona != "" && model.Persona.nombrePersona != null)
                    {
                        if (model.Persona.apellidoPaterno != "" && model.Persona.apellidoPaterno != null)
                        {
                            if (model.Persona.apellidoMaterno != "" && model.Persona.apellidoMaterno != null)
                            {
                                #region aaa
                                if (files != null)
                                {
                                    if (IsImage(files))
                                    {
                                        model.Persona.pathFoto = convertir(files);
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("Persona.pathFoto", "El archivo debe ser una imagen.");
                                    }
                                }
                                else
                                { //Nada
                                }

                                if (validacionNumDocumento(model.Persona.idTipoDocIdentidad, model.Persona.numDocIdentidad) && model.Persona.idTipoDocIdentidad > 0)
                                {
                                    if (!(model.Persona.fecNacimiento > DateTime.Now.Date))
                                    {
                                        if (ModelState.IsValid)
                                        {
                                            if (model.Persona.sexo == "M" || model.Persona.sexo == "F")
                                            {
                                                if (!db.RHUt09_persona.Where(p => p.numDocIdentidad.Equals(model.Persona.numDocIdentidad)).Select(p => p.numDocIdentidad).Any())
                                                {
                                                    if (model.Empleado.horasPorPeriodo != null)
                                                    {
                                                        if (IsNumeric(model.Empleado.horasPorPeriodo))
                                                        {
                                                            if (int.Parse(model.Empleado.horasPorPeriodo) > 0)
                                                            {
                                                                if (model.Empleado.mtoRemuneracion > 0)
                                                                {
                                                                    if (personaBL.agregarPersona(model.Persona))
                                                                    {
                                                                        model.Empleado.idPersona = model.Persona.idPersona;
                                                                        empleadoBL.agregarEmpleado(model.Empleado);
                                                                    }
                                                                    return RedirectToAction("Index");
                                                                }
                                                                else
                                                                {
                                                                    ModelState.AddModelError("Empleado.mtoRemuneracion", "Ingrese un monto válido.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese una cantidad válida.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                    }
                                                }
                                                else
                                                {
                                                    ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad ya se encuentra registrado.");
                                                }
                                            }
                                            else
                                            {
                                                ModelState.AddModelError("Persona.sexo", "Opción no válida.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("Persona.fecNacimiento", "Seleccione una fecha válida.");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad no es válido.");
                                }
                                #endregion
                            }
                            else
                            {
                                ModelState.AddModelError("Persona.apellidoMaterno", "Ingrese su apellido materno.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Persona.apellidoPaterno", "Ingrese su apellido paterno.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Persona.nombrePersona", "Ingrese sus nombres.");
                    }
                }
                else
                {
                    if (model.Persona.razonSocial != "" && model.Persona.razonSocial != null)
                    {
                        if (model.Persona.nombreRepresentante != "" && model.Persona.nombreRepresentante != null)
                        {
                            #region aaa
                            if (files != null)
                            {
                                if (IsImage(files))
                                {
                                    model.Persona.pathFoto = convertir(files);
                                }
                                else
                                {
                                    ModelState.AddModelError("Persona.pathFoto", "El archivo debe ser una imagen.");
                                }
                            }
                            else
                            { //Nada
                            }

                            if (validacionNumDocumento(model.Persona.idTipoDocIdentidad, model.Persona.numDocIdentidad) && model.Persona.idTipoDocIdentidad > 0)
                            {
                                if (!(model.Persona.fecNacimiento > DateTime.Now.Date))
                                {
                                    if (ModelState.IsValid)
                                    {
                                        if (model.Persona.sexo == "M" || model.Persona.sexo == "F")
                                        {
                                            if (!db.RHUt09_persona.Where(p => p.numDocIdentidad.Equals(model.Persona.numDocIdentidad)).Select(p => p.numDocIdentidad).Any())
                                            {
                                                if (model.Empleado.horasPorPeriodo != null)
                                                {
                                                    if (IsNumeric(model.Empleado.horasPorPeriodo))
                                                    {
                                                        if (int.Parse(model.Empleado.horasPorPeriodo) > 0)
                                                        {
                                                            if (model.Empleado.mtoRemuneracion > 0)
                                                            {
                                                                if (personaBL.agregarPersona(model.Persona))
                                                                {
                                                                    model.Empleado.idPersona = model.Persona.idPersona;
                                                                    empleadoBL.agregarEmpleado(model.Empleado);
                                                                }
                                                                return RedirectToAction("Index");
                                                            }
                                                            else
                                                            {
                                                                ModelState.AddModelError("Empleado.mtoRemuneracion", "Ingrese un monto válido.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese una cantidad válida.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                    }
                                                }
                                                else
                                                {
                                                    ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                }
                                            }
                                            else
                                            {
                                                ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad ya se encuentra registrado.");
                                            }
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("Persona.sexo", "Opción no válida.");
                                        }
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("Persona.fecNacimiento", "Seleccione una fecha válida.");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad no es válido.");
                            }
                            #endregion
                        }
                        else
                        {
                            ModelState.AddModelError("Persona.nombreRepresentante", "Ingrese su nombre.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Persona.razonSocial", "Ingrese el nombre de la empresa.");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("Persona.tipoPersoneria", "Opción no válida.");
            }

            ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", model.Persona.idDistrito);
            ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", model.Persona.idVia);
            ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", model.Persona.idZona);
            ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", model.Persona.idTipoDocIdentidad);
            ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", model.Persona.idEstadoCivil);
            ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera", model.Empleado.idEntidadFinanciera);
            ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado", model.Empleado.idTipoDeEmpleado);
            ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion", model.Empleado.idPeriodoRemuneracion);
            ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago", model.Empleado.idMedioDePago);
            ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio", model.Empleado.idConsultorio);
            return View(model);
        }


        public ActionResult Edit(long id)
        {
            RHUt01_empleado empleado = empleadoBL.obtenerEmpleado(id);
            RHUt09_persona persona = personaBL.obtenerPersona(id);
            EmpleadoVM model = new EmpleadoVM();
            model.Persona = persona;
            model.Empleado = empleado;
            ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", model.Persona.idDistrito);
            ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", model.Persona.idVia);
            ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", model.Persona.idZona);
            ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", model.Persona.idTipoDocIdentidad);
            ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", model.Persona.idEstadoCivil);
            ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera", model.Empleado.idEntidadFinanciera);
            ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado", model.Empleado.idTipoDeEmpleado);
            ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion", model.Empleado.idPeriodoRemuneracion);
            ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago", model.Empleado.idMedioDePago);
            ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio", model.Empleado.idConsultorio);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmpleadoVM model, HttpPostedFileBase files)
        {
            model.Empleado.idPersona = model.Persona.idPersona;
            model.Persona.idUsuarioModificar = User.Identity.GetUserId();
            model.Persona.fecModificacion = DateTime.Now.Date;
            model.Empleado.idUsuarioModificar = User.Identity.GetUserId();
            model.Empleado.fecModificacion = DateTime.Now.Date;


            if (model.Persona.tipoPersoneria == "N" || model.Persona.tipoPersoneria == "J")
            {
                if (model.Persona.tipoPersoneria == "N")
                {
                    if (model.Persona.nombrePersona != "" && model.Persona.nombrePersona != null)
                    {
                        if (model.Persona.apellidoPaterno != "" && model.Persona.apellidoPaterno != null)
                        {
                            if (model.Persona.apellidoMaterno != "" && model.Persona.apellidoMaterno != null)
                            {
                                #region ifff
                                if (files != null)
                                {
                                    if (IsImage(files))
                                    {
                                        model.Persona.pathFoto = convertir(files);
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("Persona.pathFoto", "El archivo debe ser una imagen.");
                                    }
                                }
                                else
                                {

                                    var image = db.Database.SqlQuery<byte[]>($@"SELECT [pathFoto]  FROM [Asiri].[dbo].[RHUt09_persona] WHERE idPersona = {model.Persona.idPersona}").Single();
                                    model.Persona.pathFoto = image;
                                    //model.Persona.pathFoto = personaBL.obtenerPersona(model.Persona.idPersona).pathFoto;
                                }

                                if (validacionNumDocumento(model.Persona.idTipoDocIdentidad, model.Persona.numDocIdentidad))
                                {
                                    if (!(model.Persona.fecNacimiento > DateTime.Now.Date))
                                    {
                                        if (ModelState.IsValid)
                                        {
                                            if (model.Persona.sexo == "M" || model.Persona.sexo == "F")
                                            {
                                                if (!db.RHUt09_persona.Where(p => p.numDocIdentidad.Equals(model.Persona.numDocIdentidad))
                                                                .Except(db.RHUt09_persona.Where(x => x.idPersona == model.Persona.idPersona))
                                                                .Select(p => p.numDocIdentidad).Any())
                                                {



                                                    if (model.Empleado.horasPorPeriodo != null)
                                                    {
                                                        if (IsNumeric(model.Empleado.horasPorPeriodo))
                                                        {
                                                            if (int.Parse(model.Empleado.horasPorPeriodo) > 0)
                                                            {
                                                                if (model.Empleado.mtoRemuneracion > 0)
                                                                {
                                                                    if (personaBL.actualizarPersona(model.Persona))
                                                                    {
                                                                        if (empleadoBL.actualizarEmpleado(model.Empleado))
                                                                            return RedirectToAction("Index");
                                                                        else
                                                                        {
                                                                            ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", model.Persona.idDistrito);
                                                                            ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", model.Persona.idVia);
                                                                            ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", model.Persona.idZona);
                                                                            ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", model.Persona.idTipoDocIdentidad);
                                                                            ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", model.Persona.idEstadoCivil);
                                                                            ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera", model.Empleado.idEntidadFinanciera);
                                                                            ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado", model.Empleado.idTipoDeEmpleado);
                                                                            ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion", model.Empleado.idPeriodoRemuneracion);
                                                                            ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago", model.Empleado.idMedioDePago);
                                                                            ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio", model.Empleado.idConsultorio);
                                                                            return View(model);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", model.Persona.idDistrito);
                                                                        ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", model.Persona.idVia);
                                                                        ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", model.Persona.idZona);
                                                                        ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", model.Persona.idTipoDocIdentidad);
                                                                        ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", model.Persona.idEstadoCivil);
                                                                        ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera", model.Empleado.idEntidadFinanciera);
                                                                        ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado", model.Empleado.idTipoDeEmpleado);
                                                                        ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion", model.Empleado.idPeriodoRemuneracion);
                                                                        ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago", model.Empleado.idMedioDePago);
                                                                        ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio", model.Empleado.idConsultorio);
                                                                        return View(model);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ModelState.AddModelError("Empleado.mtoRemuneracion", "Ingrese un monto válido.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese una cantidad válida.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                    }



                                                }
                                                else
                                                {
                                                    ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad ya se encuentra registrado.");
                                                }
                                            }
                                            else
                                            {
                                                ModelState.AddModelError("Persona.sexo", "Opción no válida.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("Persona.fecNacimiento", "Seleccione una fecha válida.");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad no es válido.");
                                }
                                #endregion
                            }
                            else
                            {
                                ModelState.AddModelError("Persona.apellidoMaterno", "Ingrese su apellido materno.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Persona.apellidoPaterno", "Ingrese su apellido paterno.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Persona.nombrePersona", "Ingrese sus nombres.");
                    }
                }
                else
                {
                    if (model.Persona.razonSocial != "" && model.Persona.razonSocial != null)
                    {
                        if (model.Persona.nombreRepresentante != "" && model.Persona.nombreRepresentante != null)
                        {
                            #region ifff
                            if (files != null)
                            {
                                if (IsImage(files))
                                {
                                    model.Persona.pathFoto = convertir(files);
                                }
                                else
                                {
                                    ModelState.AddModelError("Persona.pathFoto", "El archivo debe ser una imagen.");
                                }
                            }
                            else
                            {

                                var image = db.Database.SqlQuery<byte[]>($@"SELECT [pathFoto]  FROM [Asiri].[dbo].[RHUt09_persona] WHERE idPersona = {model.Persona.idPersona}").Single();
                                model.Persona.pathFoto = image;
                                //model.Persona.pathFoto = personaBL.obtenerPersona(model.Persona.idPersona).pathFoto;
                            }

                            if (validacionNumDocumento(model.Persona.idTipoDocIdentidad, model.Persona.numDocIdentidad))
                            {
                                if (!(model.Persona.fecNacimiento > DateTime.Now.Date))
                                {
                                    if (ModelState.IsValid)
                                    {
                                        if (model.Persona.sexo == "M" || model.Persona.sexo == "F")
                                        {
                                            if (!db.RHUt09_persona.Where(p => p.numDocIdentidad.Equals(model.Persona.numDocIdentidad))
                                                            .Except(db.RHUt09_persona.Where(x => x.idPersona == model.Persona.idPersona))
                                                            .Select(p => p.numDocIdentidad).Any())
                                            {



                                                if (model.Empleado.horasPorPeriodo != null)
                                                {
                                                    if (IsNumeric(model.Empleado.horasPorPeriodo))
                                                    {
                                                        if (int.Parse(model.Empleado.horasPorPeriodo) > 0)
                                                        {
                                                            if (model.Empleado.mtoRemuneracion > 0)
                                                            {
                                                                if (personaBL.actualizarPersona(model.Persona))
                                                                {
                                                                    if (empleadoBL.actualizarEmpleado(model.Empleado))
                                                                        return RedirectToAction("Index");
                                                                    else
                                                                    {
                                                                        ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", model.Persona.idDistrito);
                                                                        ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", model.Persona.idVia);
                                                                        ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", model.Persona.idZona);
                                                                        ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", model.Persona.idTipoDocIdentidad);
                                                                        ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", model.Persona.idEstadoCivil);
                                                                        ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera", model.Empleado.idEntidadFinanciera);
                                                                        ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado", model.Empleado.idTipoDeEmpleado);
                                                                        ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion", model.Empleado.idPeriodoRemuneracion);
                                                                        ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago", model.Empleado.idMedioDePago);
                                                                        ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio", model.Empleado.idConsultorio);
                                                                        return View(model);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", model.Persona.idDistrito);
                                                                    ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", model.Persona.idVia);
                                                                    ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", model.Persona.idZona);
                                                                    ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", model.Persona.idTipoDocIdentidad);
                                                                    ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", model.Persona.idEstadoCivil);
                                                                    ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera", model.Empleado.idEntidadFinanciera);
                                                                    ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado", model.Empleado.idTipoDeEmpleado);
                                                                    ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion", model.Empleado.idPeriodoRemuneracion);
                                                                    ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago", model.Empleado.idMedioDePago);
                                                                    ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio", model.Empleado.idConsultorio);
                                                                    return View(model);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ModelState.AddModelError("Empleado.mtoRemuneracion", "Ingrese un monto válido.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese una cantidad válida.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                    }
                                                }
                                                else
                                                {
                                                    ModelState.AddModelError("Empleado.horasPorPeriodo", "Ingrese un número válido.");
                                                }



                                            }
                                            else
                                            {
                                                ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad ya se encuentra registrado.");
                                            }
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("Persona.sexo", "Opción no válida.");
                                        }
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("Persona.fecNacimiento", "Seleccione una fecha válida.");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("Persona.numDocIdentidad", "El Documento de Identidad no es válido.");
                            }
                            #endregion
                        }
                        else
                        {
                            ModelState.AddModelError("Persona.nombreRepresentante", "Ingrese su nombre.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Persona.razonSocial", "Ingrese el nombre de la empresa.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Persona.tipoPersoneria", "Opción no válida.");
            }

            ViewData["Persona.idDistrito"] = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", model.Persona.idDistrito);
            ViewData["Persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", model.Persona.idVia);
            ViewData["Persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", model.Persona.idZona);
            ViewData["Persona.idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", model.Persona.idTipoDocIdentidad);
            ViewData["Persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", model.Persona.idEstadoCivil);
            ViewData["Empleado.idEntidadFinanciera"] = new SelectList(db.SNTt01_entidadFinanciera, "idEntidadFinanciera", "nombreEntidadFinanciera", model.Empleado.idEntidadFinanciera);
            ViewData["Empleado.idTipoDeEmpleado"] = new SelectList(db.RHUt11_tipoDeEmpleado, "idTipoDeEmpleado", "descTipoDeEmpleado", model.Empleado.idTipoDeEmpleado);
            ViewData["Empleado.idPeriodoRemuneracion"] = new SelectList(db.RHUt08_periodoRemuneracion, "idPeriodoRemuneracion", "descPeriodoRemuneracion", model.Empleado.idPeriodoRemuneracion);
            ViewData["Empleado.idMedioDePago"] = new SelectList(db.MSTt01_medioDePago, "idMedioDePago", "descMedioDePago", model.Empleado.idMedioDePago);
            ViewData["Empleado.idConsultorio"] = new SelectList(db.CLlt07_consultorio, "idConsultorio", "codConsultorio", model.Empleado.idConsultorio);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            if (empleadoBL.eliminarEmpleado(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index");
        }

        public ActionResult asignacionEmpleado(long? id)
        {

            if (id == null)
            {

                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: 'Ha ocurrido un error inesperado'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("Index");
            }
            var empleado = db.RHUt01_empleado.Find(id);
            if (empleado == null)
            {
                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: 'Error, No existe el empleado'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("Index");
            }
            var existe = db.RHUt06_horario.Where(x => x.idEmpleado == id).ToList();
            if (existe.Count != 0)
            {

                TempData["msg"] = "<script>$(document).ready(function () {$.notify({ message: 'Error, Este empleado ya tiene una asignación'}, {type: 'danger',allow_dismiss: false});});</script>";
                return RedirectToAction("Index");
            }

            var horario = new RHUt06_horario();
            var d = id.ToString();
            var s = Convert.ToInt64(d);
            horario.idEmpleado = s;
            ViewBag.idTipoDeEspeciliadad = new SelectList(db.RHUt13_tipoEspecialidad, "idTipoDeEspeciliadad", "nombreEspecialidad");
            List<RHUt04_especialidad> oliE = new List<RHUt04_especialidad>();
            ViewBag.idEspecialidad = new SelectList(oliE, "idEspecialidad", "nombreEspecialidad");
            return View(horario);
        }

        [HttpPost]
        public JsonResult AsignacionEmpleado(RHUt06_horario rHUt06_horario, List<RHUt02_empleadoEspecialidad> listEspecialidad)
        {
            var respuesta = new ResponseModel
            {
                respuesta = true,
                redirect = "/asignacionEmpleado/Create/" + rHUt06_horario.idEmpleado,
                error = ""
            };
            rHUt06_horario.fecRegistro = DateTime.Now;
            var idUsu = User.Identity.GetUserId();
            rHUt06_horario.idUsuario = idUsu;

            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid && listEspecialidad != null)
                    {
                        //Horario

                        db.RHUt06_horario.Add(rHUt06_horario);
                        db.SaveChanges();

                        //Especialidad - Empleado
                        RHUt02_empleadoEspecialidad oRHUt02_empleadoEspecialidad;
                        for (int i = 0; i < listEspecialidad.Count; i++)
                        {
                            oRHUt02_empleadoEspecialidad = new RHUt02_empleadoEspecialidad();
                            oRHUt02_empleadoEspecialidad.idEmpleado = listEspecialidad[i].idEmpleado;
                            oRHUt02_empleadoEspecialidad.idEspecialidad = listEspecialidad[i].idEspecialidad;
                            oRHUt02_empleadoEspecialidad.fecRegistro = DateTime.Now;
                            oRHUt02_empleadoEspecialidad.idUsuario = "1";
                            oRHUt02_empleadoEspecialidad.activo = true;
                            db.RHUt02_empleadoEspecialidad.Add(oRHUt02_empleadoEspecialidad);
                            db.SaveChanges();
                        }
                        respuesta.redirect = "/Empleado/Index";
                    }
                    else
                    {
                        bool estado = ModelState.IsValid;
                        if (listEspecialidad == null)
                        {
                            ModelState.AddModelError("especialidades", "Debe asignar almenos una especialidad");
                            respuesta.respuesta = false;
                            respuesta.error = "Debe asignar almenos una especialidad";

                        }
                        if (estado == false)
                        {
                            ModelState.AddModelError("especialidades", "Debe completar todos los campos necesarios");
                            respuesta.respuesta = false;
                            respuesta.error = "Debe completar todos los campos necesarios";

                        }
                        if (listEspecialidad == null && estado == false)
                        {
                            ModelState.AddModelError("especialidades", "Debe completar todos los campos necesarios");
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
        public ActionResult Edit()
        {
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
