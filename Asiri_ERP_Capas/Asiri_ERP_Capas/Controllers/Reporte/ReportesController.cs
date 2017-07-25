using Asiri_ERP_Capas.DA;
using Common;
using Common.Helper;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.AspNet.Identity;
using Service.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asiri_ERP_Capas.Controllers.Reporte
{
    
    public class ReportesController : Controller
    {
        Common.AsiriContext ctx = new Common.AsiriContext();
       

        public ActionResult IndexReport()
        {
            #region
            var idusuario = User.Identity.GetUserId();

            if (idusuario == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var custom = ctx.CustomPermiso.Where(x => x.UserID == idusuario).ToList();
            var MenuUrl = ctx.Menu.SingleOrDefault(x => x.MenuURL == UrlSystem.UrlReportes);
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
            var permiso = ctx.Permiso.Where(x => x.RoleID == idRol).ToList();
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

        //---------------------------------- Resporte de estado de cita
        public ActionResult ReporteEstadoCita()
        {
            IEnumerable<Common.CLlt09_estadoCita> Estados = ctx.CLlt09_estadoCita.ToList();
            ViewBag.idEstadoCita = new SelectList(ctx.CLlt09_estadoCita, "idEstadoCita", "descEstadoCita");
            return View(Estados);
        }

        public ActionResult RepEstCita(CLlt05_cita cLlt05_cita)
        {
            CitaDA objClienteDA = new CitaDA();

            this.HttpContext.Session["ReportName"] = "ReporteEstadoCita.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtidEstadoCita;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteEstadoCita(cLlt05_cita.idEstadoCita.ToString());

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



        // ----------------Reporte Rango de fechas de los pacientes------------------------

        public ActionResult RangoFecha()
        {
            return View();
        }
        public ActionResult RangoFechas(DateTime txtFromDate, DateTime txtToDate)
        {
            if (txtToDate < txtFromDate)
            {
                ModelState.AddModelError("", "La Fecha 'hasta' es inferior a la fecha 'desde'");
                return View("RangoFecha");
            }
            else
            {
                RangoFechaDA objClienteDA = new RangoFechaDA();

                this.HttpContext.Session["ReportName"] = "RepRangoFecha.rpt";
                this.HttpContext.Session["rptFromDate"] = txtFromDate;
                this.HttpContext.Session["rptToDate"] = txtToDate;
                this.HttpContext.Session["rptSource"] = objClienteDA.ReporteFechaPac(txtFromDate, txtToDate);

                return RedirectToAction("ShowGenericRpt1");
            }

        }

        public void ShowGenericRpt1()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate 
                string strToDate = System.Web.HttpContext.Current.Session["rptToDate"].ToString();         // Setting ToDate    
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
                    if (!string.IsNullOrEmpty(strFromDate))
                        rd.SetParameterValue("@fecha1", strFromDate);
                    if (!string.IsNullOrEmpty(strToDate))
                        rd.SetParameterValue("@fecha2", strToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");


                    // Clear all sessions value
                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
                    Session["rptToDate"] = null;
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



        // ----------------Reporte Estado de cuenta en Particular------------------------
        public ActionResult ReporteEstadoCuentaParticular()
        {
            return View();
        }

        public ActionResult RepEstCuePar(int txtDato)
        {
            var prue = Convert.ToString(ctx.TNSt05_estadoDeCuenta.SingleOrDefault(x => x.idEstadoDeCuenta == txtDato));

            if (prue != "")
            {
                ReporteDA objClienteDA = new ReporteDA();

                this.HttpContext.Session["ReportName"] = "RepEstCuePart.rpt";
                this.HttpContext.Session["rptFromDate"] = txtDato;
                //this.HttpContext.Session["rptToDate"] = txtToDate;
                this.HttpContext.Session["rptSource"] = objClienteDA.ReporteEstCuePar(txtDato);

                return RedirectToAction("ShowGenericRpt2");
            }
            else
            {
                ModelState.AddModelError("", "No existe registro!!!!!!");

            }
            return View("ReporteEstadoCuentaParticular");
        }

        public void ShowGenericRpt2()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate 
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
                    if (!string.IsNullOrEmpty(strFromDate))
                        rd.SetParameterValue("@id", strFromDate);
                    //if (!string.IsNullOrEmpty(strToDate))
                    //    rd.SetParameterValue("@fecha2", strToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");


                    // Clear all sessions value
                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
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


        // ---------------------------ESTADOS DE CUENTA DE UN CLIENTE-----------------------------

        public ActionResult ReporteEstadoDeCuenta()
        {
            return View();
        }

        public ActionResult RepEstCuenta(string txtDato)
        {
            var prue = ctx.RHUt09_persona.SingleOrDefault(c => c.numDocIdentidad == txtDato);

            if (prue != null)
            {
                ReporteDA objClienteDA = new ReporteDA();

                this.HttpContext.Session["ReportName"] = "RepEstCuenta.rpt";
                this.HttpContext.Session["rptFromDate"] = txtDato;
                //this.HttpContext.Session["rptToDate"] = txtToDate;
                this.HttpContext.Session["rptSource"] = objClienteDA.ReporteEstCuenta(txtDato);

                return RedirectToAction("ShowGenericRpt3");
            }
            else
            {
                ModelState.AddModelError("", "No existe registro!!");
                return View("ReporteEstadoDeCuenta");
            }
        }


        public void ShowGenericRpt3()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate 
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
                    if (!string.IsNullOrEmpty(strFromDate))
                        rd.SetParameterValue("@id", strFromDate);
                    //if (!string.IsNullOrEmpty(strToDate))
                    //    rd.SetParameterValue("@fecha2", strToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");


                    // Clear all sessions value
                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
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


        //------------------------------------------------ESTADO PACIENTE ACTIVO O INACTIVO--------------------

        public ActionResult ReportePacienteInactivo()
        {
            return View();
        }
        public ActionResult RepPacInac(string txtDato)
        {
            PacienteDA objClienteDA = new PacienteDA();

            this.HttpContext.Session["ReportName"] = "RepPacInac.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtidEstadoCita;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteEstadoPac(txtDato);

            return RedirectToAction("ShowGenericRpt4");
        }

        public void ShowGenericRpt4()
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

        //-----------------------------REPORTE PACIENTE EN PARTICULAR---------------------------------------
        public ActionResult ReportePacienteParticular()
        {
            IEnumerable<RHUt12_tipoDocIdentidad> Estados = ctx.RHUt12_tipoDocIdentidad.ToList();
            return View(Estados);
        }

        public ActionResult Rep(int txtDato, string txtDato1)
        {
            //var pru = (from c in ctx.RHUt12_tipoDocIdentidad select c.idTipoDocIdentidad);
            //var prue = Convert.ToString( ctx.RHUt09_persona.SingleOrDefault(c => c.numDocIdentidad == txtDato1).
            //               RHUt12_tipoDocIdentidad.SingleOrDefault(x => x.idTipoDocIdentidad =txtDato));
            var prue = Convert.ToString(ctx.RHUt12_tipoDocIdentidad.SingleOrDefault(x => x.idTipoDocIdentidad == txtDato).
                           RHUt09_persona.SingleOrDefault(c => c.numDocIdentidad == txtDato1));

            if (prue != "")
            {

                PacienteDA objClienteDA = new PacienteDA();

                this.HttpContext.Session["ReportName"] = "RepPacPart.rpt";
                //this.HttpContext.Session["rptFromDate"] = txtDato;
                //this.HttpContext.Session["rptToDate"] = txtToDate;
                this.HttpContext.Session["rptSource"] = objClienteDA.RepPacPartic(txtDato, txtDato1);
                //if(txtDato ==1)
                //    {
                //        txtDato1.Length ;
                //    }

                return RedirectToAction("ShowGenericRpt5");
            }
            else
            {
                //prue = "";
                ModelState.AddModelError("", "No existe este registro!!");
                return RedirectToAction("ReportePacienteParticular");
            }

        }

        public void ShowGenericRpt5()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                //string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate 
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
                    //  if (!string.IsNullOrEmpty(strFromDate))
                    //    rd.SetParameterValue("@ident", strFromDate);
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



        // ---------------------------- TOP N DE PACIENTES MAS CONCURRIENTES-----------------------
        public ActionResult ReportePacienteTop()
        {
            return View();
        }

        public ActionResult RepPacTopN(string txtDato)
        {
            PacienteDA objClienteDA = new PacienteDA();

            this.HttpContext.Session["ReportName"] = "RepPacTopN.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtidEstadoCita;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.RepTopN(txtDato);

            return RedirectToAction("ShowGenericRpt6");
        }

        public void ShowGenericRpt6()
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


        //------------------------------------ Reporte producto------------------------------
        public ActionResult ReporteDeProducto()
        {
            return View();
        }
        public ActionResult ReporteProducto(string txtDato)
        {
            ProductoDA objClienteDA = new ProductoDA();

            this.HttpContext.Session["ReportName"] = "RepProducto.rpt";
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteProducto();

            return RedirectToAction("ShowGenericRpt7");
        }

        public void ShowGenericRpt7()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
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
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Black");



                    Session["ReportName"] = null;
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



        //--------------------------------Reporte de servicios------------------
        public ActionResult ReporteServicios()
        {
            return View();
        }
        public ActionResult ReporteServicio(string txtDato)
        {
            ServicioDA objClienteDA = new ServicioDA();

            this.HttpContext.Session["ReportName"] = "RepServicio.rpt";
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteServicio();

            return RedirectToAction("ShowGenericRpt8");
        }

        public void ShowGenericRpt8()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
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
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Black");



                    Session["ReportName"] = null;
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

        //-------------------------------------TOP N DE ESPECIALIDADES ------------------------------

        public ActionResult ReporteEspecialidad()
        {
            return View();
        }
        public ActionResult ReportesEspecialidad(string txtDato)
        {
            EspecialidadDA objClienteDA = new EspecialidadDA();

            this.HttpContext.Session["ReportName"] = "RepTopEsp.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtDato;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteEspecialidad(txtDato);

            return RedirectToAction("ShowGenericRpt9");
        }

        public void ShowGenericRpt9()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                //string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate 
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
                    //if (!string.IsNullOrEmpty(strFromDate))
                    //    rd.SetParameterValue("@num", strFromDate);
                    //if (!string.IsNullOrEmpty(strToDate))
                    //    rd.SetParameterValue("@fecha2", strToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Black");


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

        //-----------------------TOP N medicos----------------------------
        public ActionResult ReporteMedico()
        {
            return View();
        }
        public ActionResult ReportMedico(string txtDato)
        {
            EmpleadoDA objClienteDA = new EmpleadoDA();

            this.HttpContext.Session["ReportName"] = "RepEmpSolTop.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtDato;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteMedico(txtDato);

            return RedirectToAction("ShowGenericRpt10");
        }

        public void ShowGenericRpt10()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                //string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate 
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
                    //if (!string.IsNullOrEmpty(strFromDate))
                    //    rd.SetParameterValue("@num", strFromDate);
                    //if (!string.IsNullOrEmpty(strToDate))
                    //    rd.SetParameterValue("@fecha2", strToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "Black");


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

        //-----------------------------Atencion en Particular-----------------------------

        // GET: RepAtencionParticular
        public ActionResult ReporteAtencionParticular()
        {
            return View();
        }
        public ActionResult RepAtencionParticular(string txtDato)
        {
            AtencionDA objClienteDA = new AtencionDA();

            this.HttpContext.Session["ReportName"] = "ReporAtencion.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtidEstadoCita;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteAtencion(txtDato);

            return RedirectToAction("ShowGenericRpt11");
        }

        public void ShowGenericRpt11()
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

        //--------------------Busqueda de doctor en particular-------------
        public ActionResult ReporteBuscarDoctor()
        {
            return View();
        }
        public ActionResult RepBusqueDoct(string txtDato)
        {
            //var prue = ( ctx.RHUt09_persona.Where(c=> c.numDocIdentidad == txtDato).FirstOrDefault());
            var prue = ctx.RHUt09_persona.SingleOrDefault(c => c.numDocIdentidad == txtDato).
                           RHUt01_empleado.SingleOrDefault(x => x.idTipoDeEmpleado == 2);

            if (prue != null)
            {
                RepBuscDocDA objClienteDA = new RepBuscDocDA();

                this.HttpContext.Session["ReportName"] = "CrystalReport1.rpt";
                this.HttpContext.Session["rptFromDate"] = txtDato;
                this.HttpContext.Session["rptSource"] = objClienteDA.ReporteBusquedaDoctor(txtDato);

                return RedirectToAction("ShowGenericRpt");
            }
            else
            {
                ModelState.AddModelError("", "No existe registro!!!!");
                return View("ReporteBuscarDoctor");
            }
        }
        public ActionResult RepBusquePact(string txtDatos)
        {
            RepBuscPacDA objClienteDA = new RepBuscPacDA();

            this.HttpContext.Session["ReportName"] = "CrystalReport2.rpt";
            this.HttpContext.Session["rptFromDate"] = txtDatos;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteBusquedaPaciente(txtDatos);

            return RedirectToAction("ShowGenericRpts");
        }
        public void ShowGenericRpt12()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate   
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
                    if (!string.IsNullOrEmpty(strFromDate))
                        rd.SetParameterValue("@num", strFromDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");


                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
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
        public void ShowGenericRpts()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();     // Setting FromDate   
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
                    if (!string.IsNullOrEmpty(strFromDate))
                        rd.SetParameterValue("@num1", strFromDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");


                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
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


        //------------------Buscar cita por fecha----------------

        public ActionResult ReporteRangoCitas()
        {
            return View();
        }

        public ActionResult ShowGeneric(DateTime txtFromDate, DateTime txtToDate)
        {

            if (txtToDate < txtFromDate)
            {
                ModelState.AddModelError("", "La Fecha 'hasta' es inferior a la fecha 'desde'");
                return View("ReporteRangoCitas");
            }
            else
            {
                RepBusFecCitaDA objClienteDA = new RepBusFecCitaDA();

                this.HttpContext.Session["ReportName"] = "CrystalReport3.rpt";
                this.HttpContext.Session["rptFromDate"] = txtFromDate;
                this.HttpContext.Session["rptToDate"] = txtToDate;
                this.HttpContext.Session["rptSource"] = objClienteDA.ReporteFechaPac(txtFromDate, txtToDate);

                return RedirectToAction("ShowGenericRptss");
            }
        }

        public void ShowGenericRptss()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();
                string strFromDate = System.Web.HttpContext.Current.Session["rptFromDate"].ToString();
                string strToDate = System.Web.HttpContext.Current.Session["rptToDate"].ToString();
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
                    if (!string.IsNullOrEmpty(strFromDate))
                        rd.SetParameterValue("@Fecha1", strFromDate);
                    if (!string.IsNullOrEmpty(strToDate))
                        rd.SetParameterValue("@Fecha2", strToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
                    Session["rptToDate"] = null;
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

        //-----------------------------reporte por dia mes y año---------------------
        public ActionResult ReporteDiaMesAño()
        {
            return View();
        }


        public ActionResult ShowGenerico(string txtDia, string txtMes, string txtAño)
        {
            RepBusFeDA objClienteDA = new RepBusFeDA();

            this.HttpContext.Session["ReportName"] = "CrystalReport4.rpt";
            this.HttpContext.Session["rptDia"] = txtDia;
            this.HttpContext.Session["rptMes"] = txtMes;
            this.HttpContext.Session["rptAño"] = txtAño;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteFechaPac(txtDia, txtMes, txtAño);

            return RedirectToAction("ShowGenericRptsss");
        }

        public void ShowGenericRptsss()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();
                string strDia = System.Web.HttpContext.Current.Session["rptDia"].ToString();
                string strMes = System.Web.HttpContext.Current.Session["rptMes"].ToString();
                string strAño = System.Web.HttpContext.Current.Session["rptAño"].ToString();
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
                    if (!string.IsNullOrEmpty(strDia))
                        rd.SetParameterValue("@Dia1", strDia);
                    if (!string.IsNullOrEmpty(strMes))
                        rd.SetParameterValue("@Mes1", strMes);
                    if (!string.IsNullOrEmpty(strAño))
                        rd.SetParameterValue("@Año1", strAño);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
                    Session["ReportName"] = null;
                    Session["rptDia"] = null;
                    Session["rptMes"] = null;
                    Session["rptAño"] = null;
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
        //-------------------ultima cita del cliente-----------------------
        public ActionResult ShowGeneric45(int txtDias)
        {
            RepUltimaCitaDA objClienteDA = new RepUltimaCitaDA();

            this.HttpContext.Session["ReportName"] = "CrystalReport5.rpt";
            this.HttpContext.Session["rptDias"] = txtDias;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteUltimaCita(txtDias);

            return RedirectToAction("ShowGenericRptss4");
        }

        public void ShowGenericRptss4()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();
                string strFromDate = System.Web.HttpContext.Current.Session["rptDias"].ToString();
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
                    if (!string.IsNullOrEmpty(strFromDate))
                        rd.SetParameterValue("@Dias", strFromDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
                    Session["rptToDate"] = null;
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

        //-----------------------reporte por mes y año------------------------------------

        public ActionResult ReporteMesAño()
        {
            return View();
        }

        public ActionResult ShowGenerico14(string txtMes, string txtAño)
        {
            RepBusFeDA objClienteDA = new RepBusFeDA();

            this.HttpContext.Session["ReportName"] = "CrystalReport6.rpt";
            this.HttpContext.Session["rptMes"] = txtMes;
            this.HttpContext.Session["rptAño"] = txtAño;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteFechaPac1(txtMes, txtAño);

            return RedirectToAction("ShowGenericRptsss14");
        }

        public void ShowGenericRptsss14()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();
                string strMes = System.Web.HttpContext.Current.Session["rptMes"].ToString();
                string strAño = System.Web.HttpContext.Current.Session["rptAño"].ToString();
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
                    if (!string.IsNullOrEmpty(strMes))
                        rd.SetParameterValue("@Mes", strMes);
                    if (!string.IsNullOrEmpty(strAño))
                        rd.SetParameterValue("@Año", strAño);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
                    Session["ReportName"] = null;
                    Session["rptMes"] = null;
                    Session["rptAño"] = null;
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
        //-----------------------reporte por año------------------------------------

        public ActionResult ReporteAño()
        {
            return View();
        }

        public ActionResult ShowGenerico41(string txtAño)
        {
            RepBusFeDA objClienteDA = new RepBusFeDA();

            this.HttpContext.Session["ReportName"] = "CrystalReport7.rpt";
            this.HttpContext.Session["rptAño"] = txtAño;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteFechaPac2(txtAño);

            return RedirectToAction("ShowGenericRptsss41");
        }

        public void ShowGenericRptsss41()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();
                string strAño = System.Web.HttpContext.Current.Session["rptAño"].ToString();
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
                    if (!string.IsNullOrEmpty(strAño))
                        rd.SetParameterValue("@Año2", strAño);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
                    Session["ReportName"] = null;
                    Session["rptAño"] = null;
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

        //-----------------------reporte evolucion del paciente------------------------------------
        public ActionResult ShowGenerico42(string txtCodigo)
        {
            RepEvoPacDA objClienteDA = new RepEvoPacDA();

            this.HttpContext.Session["ReportName"] = "CrystalReport8.rpt";
            this.HttpContext.Session["rptCodigo"] = txtCodigo;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteEvolucionPaciente(txtCodigo);

            return RedirectToAction("ShowGenericRptsss42");
        }

        public void ShowGenericRptsss42()
        {
            try
            {
                bool isValid = true;

                string strReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();
                string strCodigo = System.Web.HttpContext.Current.Session["rptCodigo"].ToString();
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
                    if (!string.IsNullOrEmpty(strCodigo))
                        rd.SetParameterValue("@num", strCodigo);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
                    Session["ReportName"] = null;
                    Session["rptCodigo"] = null;
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

        //-----------------------------------------------comprobante--------------------

        public ActionResult ReporteComprobante()
        {
            return View();
        }
        public ActionResult RepComprobante(string txtDato)
        {
            ComprobanteDA objClienteDA = new ComprobanteDA();

            this.HttpContext.Session["ReportName"] = "Reporte.rpt";
            //this.HttpContext.Session["rptFromDate"] = txtidEstadoCita;
            //this.HttpContext.Session["rptToDate"] = txtToDate;
            this.HttpContext.Session["rptSource"] = objClienteDA.ReporteComprobante(txtDato);

            return RedirectToAction("ShowGenericReporte");
        }

        public void ShowGenericReporte()
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

    }

}


