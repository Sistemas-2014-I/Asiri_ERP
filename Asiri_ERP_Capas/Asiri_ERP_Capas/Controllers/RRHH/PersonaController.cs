using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tecactus.Api.Reniec;
using Tecactus.Api.Sunat;
using Common;
using Microsoft.AspNet.Identity;
using Model;


namespace Asiri_ERP.Controllers.RRHH
{
    
    public class PersonaController : Controller
    {
        private AsiriContext db = new AsiriContext();

        // GET: Persona
        public ActionResult Index()
        {
            var rHUt09_persona = db.RHUt09_persona.Include(r => r.RHUt05_estadoCivil).Include(r => r.RHUt12_tipoDocIdentidad).Include(r => r.UBIt01_distrito).Include(r => r.UBIt04_via).Include(r => r.UBIt05_zona);
            return View(rHUt09_persona.ToList());
        }

        // GET: Persona/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            if (rHUt09_persona == null)
            {
                return HttpNotFound();
            }
            //IF EXITS

            var FindidDistrito = rHUt09_persona.idDistrito;
            if (FindidDistrito > 0)
            {
                ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", FindidDistrito);
                var resultadoProvincia = from idDi in db.UBIt01_distrito
                                         where idDi.idDistrito == FindidDistrito
                                         select new { idDi.idProvincia };
                var FindIdProvincia = resultadoProvincia.First();
                ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", FindIdProvincia.idProvincia);

                var resultadoRegion = from idPro in db.UBIt02_provincia
                                      where idPro.idProvincia == FindIdProvincia.idProvincia
                                      select new { idPro.idRegion };
                var FindIdRegion = resultadoRegion.First();
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", FindIdRegion.idRegion);

            }
            else
            {
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                List<SelectListItem> provs = new List<SelectListItem>();
                ViewBag.idProvincia = provs;
                List<SelectListItem> list_dists = new List<SelectListItem>();
                ViewBag.idDistrito = list_dists;
            }

            //SEXO YA
            var selectListSexo1 = new SelectList(
                    new List<SelectListItem>
                    {
                                   new SelectListItem {Text = "Hombre", Value = "H"},
                                   new SelectListItem {Text = "Mujer", Value = "M"},
                    }, "Value", "Text");


            if (rHUt09_persona.sexo != "" || rHUt09_persona.sexo != null)
            {
                if (rHUt09_persona.sexo == "H")
                {
                    ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "H");
                }
                else
                {
                    ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "M");
                }
            }
            else
            {
                ViewBag.sexo = selectListSexo1;
            }


            ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", rHUt09_persona.idVia);
            ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", rHUt09_persona.idZona);
            ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", rHUt09_persona.idEstadoCivil);
            //no funciona xq???
            //var ObjListidTipoDocIdentidad1 = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");            
            //var SelectIdTipoDocIdentidad = rHUt09_persona.idTipoDocIdentidad;
            //ViewData["idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad.Where(x=>x.idTipoDocIdentidad==o), "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewData["idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");

            ViewBag.idTipoDocIdentidadData = rHUt09_persona.idTipoDocIdentidad;
            ViewBag.valida = "0";

            return View(rHUt09_persona);
        }

        // GET: Persona/Create
        public ActionResult Create()
        {
            try
            {
                //ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil");
                //ViewData["idEstadoCivil"] = new SelectList(new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil"), "Value", "Text");

                ViewData["idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil");
                //ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
                //IList<SelectListItem> lstItems = new List<SelectListItem>();
                //lstItems.Add(new SelectListItem { Value = "1", Text = "Yes, I am a Human" });
                //lstItems.Add(new SelectListItem { Value = "2", Text = "No, I am a Robot" });
                var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
                ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");

                //SELECT REGION
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                //SELECT PROVINCIA
                //ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia");
                List<SelectListItem> provs = new List<SelectListItem>();
                //provs.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "0" });
                ViewBag.idProvincia = provs;
                //SELECT DISTRITO
                //ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito");
                List<SelectListItem> list_dists = new List<SelectListItem>();
                //list_dists.Add(new SelectListItem { Text = "-- Seleccionar --", Value = "0" });
                ViewBag.idDistrito = list_dists;

                ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia");
                ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona");

                //SEXO
                //IList<SelectListItem> lstItemsSexo = new List<SelectListItem>();
                //lstItemsSexo.Add(new SelectListItem {Selected=true, Value = "H", Text = "Hombre" });
                //lstItemsSexo.Add(new SelectListItem { Value = "M", Text = "Mujer" });
                //lstItemsSexo.Add(new SelectListItem { Value = "O", Text = "Otro" });
                //lstItemsSexo.Add(new SelectListItem { Value = "P", Text = "Prefiero no decirlo" });
                //ViewBag.validar = "0";

                var selectListSexo = new SelectList(
                new List<SelectListItem>
                {
                   new SelectListItem {Text = "Hombre", Value = "H"},
                   new SelectListItem {Text = "Mujer", Value = "M"},
                }, "Value", "Text");
                ViewBag.sexo = selectListSexo;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message + "";
                return View("Error");
                throw;
            }
        }

        // POST: Persona/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RHUt09_persona objPersona, string validar, HttpPostedFileBase pathFoto = null, int idProvincia = 0, int idRegion = 0)
        {
            try
            {
                //DEFAULT DATA CREATE
                objPersona.difunto = false;
                objPersona.activo = true;
                objPersona.fecRegistro = DateTime.Today;
                objPersona.idUsuario = "1";

                //VALIDATION OF VIEW
                var valorValidar = validar;

                if (objPersona.sexo == null)
                {
                    objPersona.sexo = "N";
                }
                if (objPersona.idTipoDocIdentidad == 3)
                {
                    objPersona.tipoPersoneria = "J";
                }
                else
                {
                    objPersona.tipoPersoneria = "N";
                }

                if (ModelState.IsValid)
                {
                    if (validar == "2")
                    {
                        
                        objPersona.sexo = objPersona.sexo;
                        objPersona.difunto = false;
                        objPersona.activo = true;
                        objPersona.fecRegistro = DateTime.Today;
                        objPersona.idUsuario = "1";
                        string idDistritot = objPersona.idDistrito + "";
                        db.RHUt09_persona.Add(objPersona);
                        db.SaveChanges();
                        var finObjobjPersona = db.RHUt09_persona.Where(x => x.idTipoDocIdentidad == objPersona.idTipoDocIdentidad && x.numDocIdentidad == objPersona.numDocIdentidad).First();
                        return RedirectToAction("Create","Paciente", new { idPersona = objPersona.idPersona });
                    }
                    else if (validar == "1")
                    {
                        var idRegionFind1 = idRegion;
                        var idProvinciaFind1 = idProvincia;
                        var idDistrito1 = objPersona.idDistrito;
                        if (idRegionFind1 > 0)
                        {
                            ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegion);

                            ViewBag.idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegionFind1), "idProvincia", "nombreProvincia");
                            if (idProvinciaFind1 > 0)
                            {
                                ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", idProvinciaFind1);

                                ViewBag.idDistrito = new SelectList(db.UBIt01_distrito.Where(p => p.idProvincia == idProvinciaFind1), "idDistrito", "nombreDistrito");
                                if (idDistrito1 > 0)
                                {
                                    List<SelectListItem> list_dists = new List<SelectListItem>();
                                    ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", idDistrito1); ;
                                }
                            }
                            else
                            {
                                List<SelectListItem> list_dists = new List<SelectListItem>();
                                ViewBag.idDistrito = list_dists;
                            }
                        }
                        else
                        {
                            ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                            List<SelectListItem> provs = new List<SelectListItem>();
                            ViewBag.idProvincia = provs;
                            List<SelectListItem> list_dists = new List<SelectListItem>();
                            ViewBag.idDistrito = list_dists;
                        }

                        //SEXO
                        var selectListSexo1 = new SelectList(
                                new List<SelectListItem>
                                {
                                   new SelectListItem {Text = "Hombre", Value = "H"},
                                   new SelectListItem {Text = "Mujer", Value = "M"},
                                }, "Value", "Text");


                        if (objPersona.sexo!="" || objPersona.sexo!=null)
                        {
                            if (objPersona.sexo == "H")
                            {
                                ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "H");
                            }
                            else
                            {
                                ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "M");
                            }
                        }
                        else
                        {
                            ViewBag.sexo = selectListSexo1;
                        }
                        ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", objPersona.idEstadoCivil);
                        //ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", objPersona.idTipoDocIdentidad);
                        var ObjListidTipoDocIdentidad1 = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", objPersona.idTipoDocIdentidad);
                        ViewData["idTipoDocIdentidad"] = new SelectList(ObjListidTipoDocIdentidad1, "Value", "Text", objPersona.idTipoDocIdentidad);
                        ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", objPersona.idVia);
                        ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", objPersona.idZona);
                        ViewBag.validar = "2";

                        return View(objPersona);
                    }
                }
         
                //WHEN THE MODEL ISN'T VALID
                if (validar == "1")
                {
                    ViewBag.validar = "0";
                }
                else
                {
                    ViewBag.validar = "2";
                }
                var idRegionFind = idRegion;
                var idProvinciaFind = idProvincia;
                var idDistrito = objPersona.idDistrito;
                if (idRegionFind > 0)
                {
                    ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegion);

                    ViewBag.idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegionFind), "idProvincia", "nombreProvincia");
                    if (idRegionFind > 0)
                    {
                        ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegion);

                        ViewBag.idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegionFind), "idProvincia", "nombreProvincia");
                        if (idProvinciaFind > 0)
                        {
                            ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", idProvinciaFind);

                            ViewBag.idDistrito = new SelectList(db.UBIt01_distrito.Where(p => p.idProvincia == idProvinciaFind), "idDistrito", "nombreDistrito");
                            if (idDistrito > 0)
                            {
                                List<SelectListItem> list_dists = new List<SelectListItem>();
                                ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", idDistrito); ;
                            }
                        }
                        else
                        {
                            List<SelectListItem> list_dists = new List<SelectListItem>();
                            ViewBag.idDistrito = list_dists;
                        }
                    }
                    else
                    {
                        List<SelectListItem> provs = new List<SelectListItem>();
                        ViewBag.idProvincia = provs;
                        List<SelectListItem> list_dists = new List<SelectListItem>();
                        ViewBag.idDistrito = list_dists;
                    }
                }
                else
                {
                    ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                    List<SelectListItem> provs = new List<SelectListItem>();
                    ViewBag.idProvincia = provs;
                    List<SelectListItem> list_dists = new List<SelectListItem>();
                    ViewBag.idDistrito = list_dists;
                }

                //SEXO
                var selectListSexo = new SelectList(
                                new List<SelectListItem>
                                {
                                   new SelectListItem {Text = "Hombre", Value = "H"},
                                   new SelectListItem {Text = "Mujer", Value = "M"},
                                }, "Value", "Text");


                if (objPersona.sexo != "" || objPersona.sexo != null)
                {
                    if (objPersona.sexo == "H")
                    {
                        ViewBag.sexo = new SelectList(selectListSexo, "Value", "Text", "H");
                    }
                    else
                    {
                        ViewBag.sexo = new SelectList(selectListSexo, "Value", "Text", "M");
                    }
                }
                else
                {
                    ViewBag.sexo = selectListSexo;
                }

                ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia");
                ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona");
                ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", objPersona.idEstadoCivil);
                //ViewBag.idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", objPersona.idTipoDocIdentidad);
                var ObjListidTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", objPersona.idTipoDocIdentidad);
                ViewData["idTipoDocIdentidad"] = new SelectList(ObjListidTipoDocIdentidad, "Value", "Text");
                ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", objPersona.idVia);
                ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", objPersona.idZona);

                //CLEAR MODELSTATE
                ModelState.Clear();
                var modelMessage = ModelState.Values;

                return View(objPersona);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message + "";
                return View("Error");
                throw;
            }

        }




        // GET: Persona/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            if (rHUt09_persona == null)
            {
                return HttpNotFound();
            }
            //IF EXITS
            
            var FindidDistrito = rHUt09_persona.idDistrito;
            if (FindidDistrito > 0)
            {
                ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", FindidDistrito);
                var resultadoProvincia = from idDi in db.UBIt01_distrito
                                         where idDi.idDistrito == FindidDistrito
                                         select new { idDi.idProvincia};
                var FindIdProvincia = resultadoProvincia.First();
                ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", FindIdProvincia.idProvincia);

                var resultadoRegion = from idPro in db.UBIt02_provincia
                                         where idPro.idProvincia == FindIdProvincia.idProvincia
                                         select new { idPro.idRegion };
                var FindIdRegion = resultadoRegion.First();
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", FindIdRegion.idRegion);

            }
            else
            {
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                List<SelectListItem> provs = new List<SelectListItem>();
                ViewBag.idProvincia = provs;
                List<SelectListItem> list_dists = new List<SelectListItem>();
                ViewBag.idDistrito = list_dists;
            }

            //SEXO YA
            var selectListSexo1 = new SelectList(
                    new List<SelectListItem>
                    {
                                   new SelectListItem {Text = "Hombre", Value = "H"},
                                   new SelectListItem {Text = "Mujer", Value = "M"},
                    }, "Value", "Text");


            if (rHUt09_persona.sexo != "" || rHUt09_persona.sexo != null)
            {
                if (rHUt09_persona.sexo == "H")
                {
                    ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "H");
                }
                else
                {
                    ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "M");
                }
            }
            else
            {
                ViewBag.sexo = selectListSexo1;
            }


            ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", rHUt09_persona.idVia);
            ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", rHUt09_persona.idZona);
            ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", rHUt09_persona.idEstadoCivil);
            //no funciona xq???
            //var ObjListidTipoDocIdentidad1 = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");            
            //var SelectIdTipoDocIdentidad = rHUt09_persona.idTipoDocIdentidad;
            //ViewData["idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad.Where(x=>x.idTipoDocIdentidad==o), "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewData["idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");

            ViewBag.idTipoDocIdentidadData = rHUt09_persona.idTipoDocIdentidad;
            ViewBag.valida = "0";

            return View(rHUt09_persona);
        }

        // POST: Persona/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RHUt09_persona objPersona, string validar, HttpPostedFileBase pathFoto = null, int idProvincia = 0, int idRegion = 0)
        {
            //DEFAULT DATA CREATE
            objPersona.idUsuario = "1";
            //VALIDATION OF VIEW
            var valorValidar = validar;
            //SEXO
            if (objPersona.sexo == null)
            {
                objPersona.sexo = "N";
            }
            if (objPersona.idTipoDocIdentidad == 3)
            {
                objPersona.tipoPersoneria = "J";
            }
            else
            {
                objPersona.tipoPersoneria = "N";
            }

            if (ModelState.IsValid)
            {
                if (validar == "2")
                {
                    objPersona.fecModificacion = DateTime.Today;
                    objPersona.idUsuario = "1";
                    objPersona.fecDefuncion = null;
                    objPersona.fecRegistro = DateTime.Today;
                    objPersona.fecNacimiento = null;            
                    db.Entry(objPersona).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    var finObjobjPersona = db.RHUt09_persona.Where(x => x.idTipoDocIdentidad == objPersona.idTipoDocIdentidad && x.numDocIdentidad == objPersona.numDocIdentidad).First();
                    return RedirectToAction("Edit", "Paciente", new { idPersona = objPersona.idPersona });
                }
                else if (validar == "1")
                {
                    var idRegionFind1 = idRegion;
                    var idProvinciaFind1 = idProvincia;
                    var idDistrito1 = objPersona.idDistrito;
                    if (idRegionFind1 > 0)
                    {
                        ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegion);

                        ViewBag.idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegionFind1), "idProvincia", "nombreProvincia");
                        if (idProvinciaFind1 > 0)
                        {
                            ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", idProvinciaFind1);

                            ViewBag.idDistrito = new SelectList(db.UBIt01_distrito.Where(p => p.idProvincia == idProvinciaFind1), "idDistrito", "nombreDistrito");
                            if (idDistrito1 > 0)
                            {
                                List<SelectListItem> list_dists = new List<SelectListItem>();
                                ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", idDistrito1); ;
                            }
                        }
                        else
                        {
                            List<SelectListItem> list_dists = new List<SelectListItem>();
                            ViewBag.idDistrito = list_dists;
                        }
                    }
                    else
                    {
                        ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                        List<SelectListItem> provs = new List<SelectListItem>();
                        ViewBag.idProvincia = provs;
                        List<SelectListItem> list_dists = new List<SelectListItem>();
                        ViewBag.idDistrito = list_dists;
                    }
                    //SEXO
                    var selectListSexo1 = new SelectList(
                            new List<SelectListItem>
                            {
                                   new SelectListItem {Text = "Hombre", Value = "H"},
                                   new SelectListItem {Text = "Mujer", Value = "M"},
                            }, "Value", "Text");


                    if (objPersona.sexo != "" || objPersona.sexo != null)
                    {
                        if (objPersona.sexo == "H")
                        {
                            ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "H");
                        }
                        else
                        {
                            ViewBag.sexo = new SelectList(selectListSexo1, "Value", "Text", "M");
                        }
                    }
                    else
                    {
                        ViewBag.sexo = selectListSexo1;
                    }
                    ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", objPersona.idEstadoCivil);
                    ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", objPersona.idVia);
                    ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", objPersona.idZona);
                    ViewData["idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
                    ViewBag.idTipoDocIdentidadData = objPersona.idTipoDocIdentidad;
                    ViewBag.validar = "2";
                    return View(objPersona);
                }
            }

            //WHEN THE MODEL ISN'T VALID
            if (validar == "1")
            {
                ViewBag.validar = "0";
            }
            else
            {
                ViewBag.validar = "2";
            }
            var idRegionFind = idRegion;
            var idProvinciaFind = idProvincia;
            var idDistrito = objPersona.idDistrito;
            if (idRegionFind > 0)
            {
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegion);

                ViewBag.idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegionFind), "idProvincia", "nombreProvincia");
                if (idRegionFind > 0)
                {
                    ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegion);

                    ViewBag.idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegionFind), "idProvincia", "nombreProvincia");
                    if (idProvinciaFind > 0)
                    {
                        ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", idProvinciaFind);

                        ViewBag.idDistrito = new SelectList(db.UBIt01_distrito.Where(p => p.idProvincia == idProvinciaFind), "idDistrito", "nombreDistrito");
                        if (idDistrito > 0)
                        {
                            List<SelectListItem> list_dists = new List<SelectListItem>();
                            ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", idDistrito); ;
                        }
                    }
                    else
                    {
                        List<SelectListItem> list_dists = new List<SelectListItem>();
                        ViewBag.idDistrito = list_dists;
                    }
                }
                else
                {
                    List<SelectListItem> provs = new List<SelectListItem>();
                    ViewBag.idProvincia = provs;
                    List<SelectListItem> list_dists = new List<SelectListItem>();
                    ViewBag.idDistrito = list_dists;
                }
            }
            else
            {
                ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
                List<SelectListItem> provs = new List<SelectListItem>();
                ViewBag.idProvincia = provs;
                List<SelectListItem> list_dists = new List<SelectListItem>();
                ViewBag.idDistrito = list_dists;
            }

            //SEXO
            var selectListSexo = new SelectList(
                            new List<SelectListItem>
                            {
                                   new SelectListItem {Text = "Hombre", Value = "H"},
                                   new SelectListItem {Text = "Mujer", Value = "M"},
                            }, "Value", "Text");
            if (objPersona.sexo != "" || objPersona.sexo != null)
            {
                if (objPersona.sexo == "H")
                {
                    ViewBag.sexo = new SelectList(selectListSexo, "Value", "Text", "H");
                }
                else
                {
                    ViewBag.sexo = new SelectList(selectListSexo, "Value", "Text", "M");
                }
            }
            else
            {
                ViewBag.sexo = selectListSexo;
            }
            ViewBag.idVia = new SelectList(db.UBIt04_via, "idVia", "descVia", objPersona.idVia);
            ViewBag.idZona = new SelectList(db.UBIt05_zona, "idZona", "descZona", objPersona.idZona);
            ViewBag.idEstadoCivil = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", objPersona.idEstadoCivil);                       
            ViewData["idTipoDocIdentidad"] = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewBag.idTipoDocIdentidadData = objPersona.idTipoDocIdentidad;
            //CLEAR MODELSTATE
            ModelState.Clear();
            var modelMessage = ModelState.Values;
            return View(objPersona);
        }

        // GET: Persona/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            if (rHUt09_persona == null)
            {
                return HttpNotFound();
            }
            return View(rHUt09_persona);
        }


        //CAMBIAR DE ESTADO PERSONA
        public ActionResult Estado(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
                if (rHUt09_persona == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    if (rHUt09_persona.activo == false)
                    {
                        rHUt09_persona.activo = true;
                        //FECHA DE MODIFICACION DELETE
                        rHUt09_persona.fecModificacion = DateTime.Today;
                        db.Entry(rHUt09_persona).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        rHUt09_persona.activo = false;
                        //FECHA DE ELIMINACIÓN
                        rHUt09_persona.fecEliminacion = DateTime.Today;
                        db.Entry(rHUt09_persona).State = EntityState.Modified;
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



        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RHUt09_persona rHUt09_persona = db.RHUt09_persona.Find(id);
            db.RHUt09_persona.Remove(rHUt09_persona);
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
















        #region REGION VALIDAR NRO DOC
        //VALIDAR EXISTENCIA PERSONA
        public bool ValidarExistenciaPersona(string numDocIdentidad, int idTipoDocIdentidad)
        {
            bool estadoPersona = true;
            List<RHUt09_persona> objPersona = db.RHUt09_persona.Where(x => x.idTipoDocIdentidad == idTipoDocIdentidad).ToList();
            objPersona = objPersona.Where(x => x.numDocIdentidad == numDocIdentidad).ToList();
            return estadoPersona = (objPersona.Count > 0) ? estadoPersona : false;
        }

        //VALIDAR DOC
        [HttpPost]
        public JsonResult validarDoc(string numDocIdentidad, int idTipoDocIdentidad)
        {
            try
            {
                bool estadoPersona = ValidarExistenciaPersona(numDocIdentidad, idTipoDocIdentidad);
                if (estadoPersona == true)
                {
                    //1 EXISTE
                    return Json("1");
                }
                else
                {
                    //0 NO EXISTE
                    return Json("0");
                    //if (idTipoDocIdentidad == 1)
                    //{
                    //    return validarDni(numDocIdentidad);
                    //}
                    //else if (idTipoDocIdentidad == 2)
                    //{
                    //    return validarRuc(numDocIdentidad);
                    //}
                    //else
                    //{
                    //    return Json("0");
                    //}
                }
            }
            catch (Exception e)
            {
                return Json(new { Success = 0, ex = e.Message.ToString() });
                throw;
            }
        }

        //VALIDAR DNI
        [HttpPost]
        public JsonResult validarDni(string numDocIdentidad, int idTipoDocIdentidad)
        {
            try
            {
                bool estadoPersona = ValidarExistenciaPersona(numDocIdentidad, idTipoDocIdentidad);
                if (estadoPersona == true)
                {
                    //1 EXISTE
                    return Json("1");
                }
                else
                {
                    //0 NO EXISTE

                    // instanciar un objecto de la clase Dni
                    var dni = new Tecactus.Api.Reniec.Dni("eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjQ5Zjc4ZjRhMzBhOWU4ZjI4OTdhYmU2ODU2MGY0ZTMwNzZjOTg0YmMyMzhjMWI2ZmMzM2RjYzIwYzY2OTU5YThhYzQ3OTZlNDIzZmZmN2EyIn0.eyJhdWQiOiIxIiwianRpIjoiNDlmNzhmNGEzMGE5ZThmMjg5N2FiZTY4NTYwZjRlMzA3NmM5ODRiYzIzOGMxYjZmYzMzZGNjMjBjNjY5NTlhOGFjNDc5NmU0MjNmZmY3YTIiLCJpYXQiOjE0OTcyMjc2OTUsIm5iZiI6MTQ5NzIyNzY5NSwiZXhwIjoxODEyNzYwNDk1LCJzdWIiOiIzNDgiLCJzY29wZXMiOlsidXNlLXJlbmllYyIsInVzZS1zdW5hdCJdfQ.CbmBj_u9GsIV0rTuifgnaGX8GWxzN_ElAEsVSrV4WRdY0-46Q8rodWpRI7Qos5ADNXDJVCzx7OATa0ZRk1OxVRBEGMtXO91jMCmH255kuQMKEvLiDAq9KDkMAa_GML0XEuJmHn1mqGZuZdM2U3ImIKSQ7iOUTOgnh4izJuQ8xXlmE8qwSLWDN155xsJ1dkrKN4Hj-fkceSjMeZNm_dTn3aSsTE6gyU3YHkXpBjDFunGtFfXud0u8-Kv_Hu7ucbJw8IV3JUOtrzAGjttSQo1GpqrqOWAU05tQGur1rLlhiIpH4XZKe7aGzHNc6Y9XChPMxsDslslJgY56TIKUOX4goJckTslmAzj6F6RoWFnrGHluagK5M9LoPD5fKuAXAjgLTJexeWFsqMYfQIPUsIKhYZC4TQEvlzEU-PBASYj3tYnKFZnjaglLRSIb2w0wchJ9ZXhI2ZCpXNVnjyX6EPpbukMraA-nJ68oOMDzfhRKJvdC5Pl6qUymFxzsFVl93kCrBibFsnf6f2CI_0kCHV17yyfANJf54kALgGnFQMhw8Xd1EneeglH_LFukfd-MU7J0zjenxzNNO037FMrCm4OiITIAH3yR2_6mtSsbytFNpTral50jc8dyg2nP1bQtnCtoI3swCS419aGTl7xzLmeOOHYTOw-v4mxwf6CH826lW00");
                    ////el método 'get' devuelve un objeto de la clase Person.
                    ////Caso contrario lanza una excepción cuyo mensaje describe el error sucitado.
                    Tecactus.Api.Reniec.Person person = dni.get(numDocIdentidad);
                    //Tecactus.Api.Reniec.Person person = new Person();
                    //person.dni = "71842016";
                    //person.nombres = "ROY BRAYAN";
                    //person.apellido_paterno = "FELIX";
                    //person.apellido_materno = "TRINIDAD";
                    return Json(person);
                    //return Json("0");
                }
            }
            catch (Exception e)
            {
                //return Json(new { Success = 0, ex = e.Message.ToString() });
                return Json("0");
                throw;
            }
        }
        [HttpPost]
        public JsonResult validarRuc(string numDocIdentidad, int idTipoDocIdentidad)
        {
            try
            {
                bool estadoPersona = ValidarExistenciaPersona(numDocIdentidad, idTipoDocIdentidad);
                if (estadoPersona == true)
                {
                    //1 EXISTE
                    return Json("1");
                }
                else
                {
                    //0 NO EXISTE

                    var ruc = new Tecactus.Api.Sunat.Ruc("eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjQ5Zjc4ZjRhMzBhOWU4ZjI4OTdhYmU2ODU2MGY0ZTMwNzZjOTg0YmMyMzhjMWI2ZmMzM2RjYzIwYzY2OTU5YThhYzQ3OTZlNDIzZmZmN2EyIn0.eyJhdWQiOiIxIiwianRpIjoiNDlmNzhmNGEzMGE5ZThmMjg5N2FiZTY4NTYwZjRlMzA3NmM5ODRiYzIzOGMxYjZmYzMzZGNjMjBjNjY5NTlhOGFjNDc5NmU0MjNmZmY3YTIiLCJpYXQiOjE0OTcyMjc2OTUsIm5iZiI6MTQ5NzIyNzY5NSwiZXhwIjoxODEyNzYwNDk1LCJzdWIiOiIzNDgiLCJzY29wZXMiOlsidXNlLXJlbmllYyIsInVzZS1zdW5hdCJdfQ.CbmBj_u9GsIV0rTuifgnaGX8GWxzN_ElAEsVSrV4WRdY0-46Q8rodWpRI7Qos5ADNXDJVCzx7OATa0ZRk1OxVRBEGMtXO91jMCmH255kuQMKEvLiDAq9KDkMAa_GML0XEuJmHn1mqGZuZdM2U3ImIKSQ7iOUTOgnh4izJuQ8xXlmE8qwSLWDN155xsJ1dkrKN4Hj-fkceSjMeZNm_dTn3aSsTE6gyU3YHkXpBjDFunGtFfXud0u8-Kv_Hu7ucbJw8IV3JUOtrzAGjttSQo1GpqrqOWAU05tQGur1rLlhiIpH4XZKe7aGzHNc6Y9XChPMxsDslslJgY56TIKUOX4goJckTslmAzj6F6RoWFnrGHluagK5M9LoPD5fKuAXAjgLTJexeWFsqMYfQIPUsIKhYZC4TQEvlzEU-PBASYj3tYnKFZnjaglLRSIb2w0wchJ9ZXhI2ZCpXNVnjyX6EPpbukMraA-nJ68oOMDzfhRKJvdC5Pl6qUymFxzsFVl93kCrBibFsnf6f2CI_0kCHV17yyfANJf54kALgGnFQMhw8Xd1EneeglH_LFukfd-MU7J0zjenxzNNO037FMrCm4OiITIAH3yR2_6mtSsbytFNpTral50jc8dyg2nP1bQtnCtoI3swCS419aGTl7xzLmeOOHYTOw-v4mxwf6CH826lW00");
                    Tecactus.Api.Sunat.Company company = ruc.get(numDocIdentidad);
                    //Tecactus.Api.Sunat.Company company = new Tecactus.Api.Sunat.Company();
                    //company.ruc = "123456789";
                    //company.nombre_comercial = "NOMBRE COMERCIAL";
                    //company.razon_social = "RAZÓN SOCIAL";
                    //company.tipo_contribuyente = "TIPO DE CONTRIBUYENTE";
                    //company.direccion = "DIRECCIÓN";
                    //company.estado_contribuyente = "ESTADO";
                    return Json(company);
                    //return Json("0");
                }
            }
            catch (Exception e)
            {
                return Json(new { Success = 0, ex = e.Message.ToString() });
                throw;
            }
        }


        [HttpPost]
        public JsonResult validarDni2(string numDocIdentidad)
        {
            try
            {
                // instanciar un objecto de la clase Dni
                //var dni = new Tecactus.Api.Reniec.Dni("eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjQ5Zjc4ZjRhMzBhOWU4ZjI4OTdhYmU2ODU2MGY0ZTMwNzZjOTg0YmMyMzhjMWI2ZmMzM2RjYzIwYzY2OTU5YThhYzQ3OTZlNDIzZmZmN2EyIn0.eyJhdWQiOiIxIiwianRpIjoiNDlmNzhmNGEzMGE5ZThmMjg5N2FiZTY4NTYwZjRlMzA3NmM5ODRiYzIzOGMxYjZmYzMzZGNjMjBjNjY5NTlhOGFjNDc5NmU0MjNmZmY3YTIiLCJpYXQiOjE0OTcyMjc2OTUsIm5iZiI6MTQ5NzIyNzY5NSwiZXhwIjoxODEyNzYwNDk1LCJzdWIiOiIzNDgiLCJzY29wZXMiOlsidXNlLXJlbmllYyIsInVzZS1zdW5hdCJdfQ.CbmBj_u9GsIV0rTuifgnaGX8GWxzN_ElAEsVSrV4WRdY0-46Q8rodWpRI7Qos5ADNXDJVCzx7OATa0ZRk1OxVRBEGMtXO91jMCmH255kuQMKEvLiDAq9KDkMAa_GML0XEuJmHn1mqGZuZdM2U3ImIKSQ7iOUTOgnh4izJuQ8xXlmE8qwSLWDN155xsJ1dkrKN4Hj-fkceSjMeZNm_dTn3aSsTE6gyU3YHkXpBjDFunGtFfXud0u8-Kv_Hu7ucbJw8IV3JUOtrzAGjttSQo1GpqrqOWAU05tQGur1rLlhiIpH4XZKe7aGzHNc6Y9XChPMxsDslslJgY56TIKUOX4goJckTslmAzj6F6RoWFnrGHluagK5M9LoPD5fKuAXAjgLTJexeWFsqMYfQIPUsIKhYZC4TQEvlzEU-PBASYj3tYnKFZnjaglLRSIb2w0wchJ9ZXhI2ZCpXNVnjyX6EPpbukMraA-nJ68oOMDzfhRKJvdC5Pl6qUymFxzsFVl93kCrBibFsnf6f2CI_0kCHV17yyfANJf54kALgGnFQMhw8Xd1EneeglH_LFukfd-MU7J0zjenxzNNO037FMrCm4OiITIAH3yR2_6mtSsbytFNpTral50jc8dyg2nP1bQtnCtoI3swCS419aGTl7xzLmeOOHYTOw-v4mxwf6CH826lW00");

                ////el método 'get' devuelve un objeto de la clase Person.
                ////Caso contrario lanza una excepción cuyo mensaje describe el error sucitado.

                //Tecactus.Api.Reniec.Person person = dni.get(numDocIdentidad);
                Tecactus.Api.Reniec.Person person = new Person();
                person.dni = "71842016";
                person.nombres = "ROY BRAYAN";
                person.apellido_paterno = "FELIX";
                person.apellido_materno = "TRINIDAD";
                return Json(person);
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Json("" + exception.Message);
            }
        }

        #endregion

        #region REGION UBIGEO
        public JsonResult GetProvincia(int idRegion)
        {
            var idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegion), "idProvincia", "nombreProvincia");
            return Json(new SelectList(idProvincia, "Value", "Text"));
        }

        public JsonResult GetDistrito(int idProvincia)
        {
            var idDistrito = new SelectList(db.UBIt01_distrito.Where(p => p.idProvincia == idProvincia), "idDistrito", "nombreDistrito");
            return Json(new SelectList(idDistrito, "Value", "Text"));
        }

        #endregion



    }
}
