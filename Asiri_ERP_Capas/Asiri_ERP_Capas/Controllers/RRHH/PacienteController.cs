using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using Common;
using Microsoft.AspNet.Identity;
using Model;
using Service.Reportes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Asiri_ERP_Capas.DA;
using Common.Helper;

namespace Asiri_ERP.Controllers.RRHH
{
    
    public class PacienteController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: Paciente
        public ActionResult Index()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = db.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = db.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlPaciente);
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

            var rHUt07_paciente = db.RHUt07_paciente.Include(r => r.RHUt09_persona);
            return View(rHUt07_paciente.ToList());
        }

        public ActionResult ObtenerAtenciones(long id)
        {
            var atenciones = db.CLlt03_atencion.Include(x => x.CLlt05_cita).Where(x => x.CLlt05_cita.idPaciente == id).ToList();
            return PartialView(atenciones);
        }

        public ActionResult Historial(long id)
        {
            AtencionDA objClienteDA = new AtencionDA();

            this.HttpContext.Session["ReportName"] = "RepAtencionParticular.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtidEstadoCita;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteAtencion(id.ToString());

            return RedirectToAction("ShowGenericRpt");
        }
        public void ShowGenericRpt()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                                                                                                           //    string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate 
                                                                                                           //string strToDate = System.Web.HttpContext.Current.Session["rptToDate"].ToString();         // Setting ToDate    
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];

                if (string.IsNullOrEmpty(strReportName))
                {
                    isValid = false;
                }

                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reportes//" + strReportName;
                    rd.Load(strRptPath);
                    if (rptSource != null && rptSource.GetType().ToString() != "System.String")
                        rd.SetDataSource(rptSource);
                    // if (!string.IsNullOrEmpty(strFromDate))
                    //   rd.SetParameterValue("@id", strFromDate);
                    //if (!string.IsNullOrEmpty(strToDate))
                    //    rd.SetParameterValue("@fecha2", strToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");


                    // Clear all sessions value
                    Session["ReportName"] = null;
                    //Session["rptFromDate"] = null;
                    //Session["rptToDate"] = null;
                    Session["rptSource"] = null;
                }
                else
                {
                    Response.Write("<H2>No se encontró información</H2>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        // GET: Paciente/Details/5
        public ActionResult Details(long? id,int idPersona = 0)
        {
            RHUt07_paciente rHUt07_paciente = new RHUt07_paciente();
            if (id == null && idPersona != 0)
            {
                var rHUt07_pacienteWithIdPersona = db.RHUt07_paciente.SingleOrDefault(x => x.idPersona == idPersona);
                if (rHUt07_pacienteWithIdPersona != null)
                {
                    rHUt07_paciente = rHUt07_pacienteWithIdPersona;
                }
                else
                {
                    //REDIRECCIONAR AL INDEX PERSONA
                    return RedirectToAction("Index", "Persona");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                //rHUt07_paciente = db.RHUt07_paciente.Find(id);
                rHUt07_paciente = db.RHUt07_paciente.SingleOrDefault(x => x.idPersona == id);
            }

            if (rHUt07_paciente == null)
            {
                //NO LO ENCONTRO COMO PACIENTE
                return HttpNotFound();
            }
            //INICIANDO DETAIL
            ViewBag.codPaciente = rHUt07_paciente.codPaciente;
            ViewBag.numHistoriaClinica = rHUt07_paciente.numHistoriaClinica;

            ViewData["idPersona"] = new SelectList(db.RHUt09_persona, "idPersona", "nombrePersona", rHUt07_paciente.idPersona);
            ViewBag.Paciente = "1";

            //GRUPO SANGUÍNEO
            var selectListGrupoSanguineo = new SelectList(
                new List<SelectListItem>
                {
                   new SelectListItem {Text = "O+", Value = "O+"},
                   new SelectListItem {Text = "A+", Value = "A+"},
                   new SelectListItem {Text = "B+", Value = "B+"},
                   new SelectListItem {Text = "AB+", Value = "AB+"},
                   new SelectListItem {Text = "O-", Value = "O-"},
                   new SelectListItem {Text = "A-", Value = "A-"},
                   new SelectListItem {Text = "B-", Value = "B-"},
                   new SelectListItem {Text = "AB-", Value = "AB-"},
                }, "Value", "Text", rHUt07_paciente.grupoSanguineo);
            ViewBag.grupoSanguineo = selectListGrupoSanguineo;
            return View(rHUt07_paciente);
        }


        //GENERAL CODPACIENTE
        public string genCodPaciente(int idPersona)
        {
            int idPersonaLength = (idPersona.ToString()).Length;
            string cadena = "P00000000000000";
            //CADENA A ENVIAR
            string codPaciente = "";
            //WHITOUT + 1
            var cadenaSumUno = (idPersona).ToString();
            //if (cadenaSumUno.Length == idPersonaLength)
            //{
                codPaciente = cadena.Substring(0, cadena.Length - idPersonaLength);
            //    codPaciente = cadena + cadenaSumUno;
            //}
            //else
            //{
                //codPaciente = cadena.Substring(0, cadena.Length - idPersonaLength - 1);
                codPaciente = codPaciente + cadenaSumUno;
            //}
            return codPaciente;
        }
        //GENERAL NUMHISTORIA
        public string numHistoriaClinica()
        {
            var numHistoriaClinica = db.RHUt07_paciente.OrderByDescending(u => u.idPaciente).FirstOrDefault();
            int idPacienteLength = (numHistoriaClinica.idPaciente.ToString()).Length;
            string cadena = "000000000000000";
            //CADENA A ENVIAR
            string cadenanumHistoriaClinica = "";
            //
            var cadenaSumUno = (numHistoriaClinica.idPaciente + 1).ToString();
            if (cadenaSumUno.Length == idPacienteLength)
            {
                cadenanumHistoriaClinica = cadena.Substring(0, cadena.Length - idPacienteLength);
                cadenanumHistoriaClinica = cadenanumHistoriaClinica + cadenaSumUno;
            }
            else
            {
                cadenanumHistoriaClinica = cadena.Substring(0, cadena.Length - idPacienteLength - 1);
                cadenanumHistoriaClinica = cadenanumHistoriaClinica + cadenaSumUno;
            }
            return cadenanumHistoriaClinica;
        }


        // GET: Paciente/Create
        public ActionResult Create(int idPersona=0)
        {

            ViewBag.codPaciente = genCodPaciente(idPersona);
            ViewBag.numHistoriaClinica = numHistoriaClinica();
            if (idPersona!=0)
            {
                ViewData["idPersona"] = new SelectList(db.RHUt09_persona, "idPersona", "nombrePersona", idPersona);
                ViewBag.Paciente = "1";
            }
            else
            {
                var resultado = from per in db.RHUt09_persona
                                join left in db.RHUt07_paciente
                                on per.idPersona equals left.idPersona
                                into leftOrder
                                from order in leftOrder.DefaultIfEmpty()
                                where order == null
                                select new { per.idPersona, per.nombrePersona };
                ViewData["idPersona"] = new SelectList(resultado, "idPersona", "nombrePersona");
                ViewBag.Paciente = "0";
            }
            //GRUPO SANGUÍNEO
            var selectListGrupoSanguineo = new SelectList(
                new List<SelectListItem>
                {
                   new SelectListItem {Text = "O+", Value = "O+"},
                   new SelectListItem {Text = "A+", Value = "A+"},
                   new SelectListItem {Text = "B+", Value = "B+"},
                   new SelectListItem {Text = "AB+", Value = "AB+"},
                   new SelectListItem {Text = "O-", Value = "O-"},
                   new SelectListItem {Text = "A-", Value = "A-"},
                   new SelectListItem {Text = "B-", Value = "B-"},
                   new SelectListItem {Text = "AB-", Value = "AB-"},
                }, "Value", "Text");
            ViewBag.grupoSanguineo = selectListGrupoSanguineo;
            //ACTIVO
            ViewBag.activo = "true";
            return View();
        }




        // POST: Paciente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idPaciente,codPaciente,numHistoriaClinica,grupoSanguineo,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idPersona")] RHUt07_paciente rHUt07_paciente)
        public ActionResult Create(RHUt07_paciente rHUt07_paciente)
        {
            try
            {
                rHUt07_paciente.fecRegistro = DateTime.Today;
                rHUt07_paciente.idUsuario = "1";
                rHUt07_paciente.activo = true;
                if (ModelState.IsValid)
                {
                    db.RHUt07_paciente.Add(rHUt07_paciente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.idPersona = new SelectList(db.RHUt09_persona, "idPersona", "tipoPersoneria", rHUt07_paciente.idPersona);
                return View(rHUt07_paciente);
            }
            catch (DbEntityValidationException  exc)
            {
                ViewBag.Error = exc.InnerException + "" + exc.Source+ ""+exc.Message;
                return View("Error");
                throw;
            }
        }

        // GET: Paciente/Edit/5
        public ActionResult Edit(long? id,int idPersona=0)
        {

            RHUt07_paciente rHUt07_paciente = new RHUt07_paciente();
            if (id == null && idPersona!=0)
            {
                var rHUt07_pacienteWithIdPersona = db.RHUt07_paciente.Where(x => x.idPersona == idPersona).ToList();               
                if (rHUt07_pacienteWithIdPersona != null)
                {
                    rHUt07_paciente = rHUt07_pacienteWithIdPersona.First();
                }else
                {
                    //REDIRECCIONAR AL INDEX PERSONA
                    return RedirectToAction("Index", "Persona");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                rHUt07_paciente = db.RHUt07_paciente.Find(id);
            }

            if (rHUt07_paciente == null)
            {
                //NO LO ENCONTRO COMO PACIENTE
                return HttpNotFound();
            }

            //INICIANDO EDIT
            ViewBag.codPaciente = rHUt07_paciente.codPaciente;
            ViewBag.numHistoriaClinica = rHUt07_paciente.numHistoriaClinica;

            ViewData["idPersona"] = new SelectList(db.RHUt09_persona, "idPersona", "nombrePersona", rHUt07_paciente.idPersona);
            ViewBag.Paciente = "1";

            //GRUPO SANGUÍNEO
            var selectListGrupoSanguineo = new SelectList(
                new List<SelectListItem>
                {
                   new SelectListItem {Text = "O+", Value = "O+"},
                   new SelectListItem {Text = "A+", Value = "A+"},
                   new SelectListItem {Text = "B+", Value = "B+"},
                   new SelectListItem {Text = "AB+", Value = "AB+"},
                   new SelectListItem {Text = "O-", Value = "O-"},
                   new SelectListItem {Text = "A-", Value = "A-"},
                   new SelectListItem {Text = "B-", Value = "B-"},
                   new SelectListItem {Text = "AB-", Value = "AB-"},
                }, "Value", "Text",rHUt07_paciente.grupoSanguineo);
            ViewBag.grupoSanguineo = selectListGrupoSanguineo;

            return View(rHUt07_paciente);
        }

        // POST: Paciente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idPaciente,codPaciente,numHistoriaClinica,grupoSanguineo,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idPersona")] RHUt07_paciente rHUt07_paciente)
        public ActionResult Edit(RHUt07_paciente rHUt07_paciente)
        {
            rHUt07_paciente.fecModificacion = DateTime.Today;
            rHUt07_paciente.idUsuario = "1";
            rHUt07_paciente.activo = true;
            if (ModelState.IsValid)
            {
                db.Entry(rHUt07_paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPersona = new SelectList(db.RHUt09_persona, "idPersona", "tipoPersoneria", rHUt07_paciente.idPersona);
            return View(rHUt07_paciente);
        }

        // GET: Paciente/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt07_paciente rHUt07_paciente = db.RHUt07_paciente.Find(id);
            if (rHUt07_paciente == null)
            {
                return HttpNotFound();
            }
            return View(rHUt07_paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RHUt07_paciente rHUt07_paciente = db.RHUt07_paciente.Find(id);
            db.RHUt07_paciente.Remove(rHUt07_paciente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        #region Estado
        public ActionResult Estado(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RHUt07_paciente rHUt07_paciente = db.RHUt07_paciente.Find(id);
                if (rHUt07_paciente == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (rHUt07_paciente.activo == false)
                    {
                        rHUt07_paciente.activo = true;
                        //FECHA DE MODIFICACION DELETE
                        rHUt07_paciente.fecModificacion = DateTime.Today;
                        db.Entry(rHUt07_paciente).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        rHUt07_paciente.activo = false;
                        //FECHA DE ELIMINACIÓN
                        rHUt07_paciente.fecEliminacion = DateTime.Today;
                        db.Entry(rHUt07_paciente).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message + "";
                return View("Error");
                throw;
            }
        }
        #endregion

        #region CodPaciente
        public JsonResult GetCodPaciente(int idPersona)
        {
            int idPersonaLength = (idPersona.ToString()).Length;
            string cadena = "P00000000000000";
            string codPaciente = cadena.Substring(0, cadena.Length - idPersonaLength);
            codPaciente = codPaciente + idPersona + "";
            return Json(codPaciente);
        }

        #endregion


    }
}
