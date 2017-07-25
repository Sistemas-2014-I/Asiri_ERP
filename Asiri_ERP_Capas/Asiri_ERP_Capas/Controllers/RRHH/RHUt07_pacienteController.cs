using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tecactus.Api.Reniec;
using System.Text;
using System.IO;
using Tecactus.Api.Sunat;
using Newtonsoft.Json.Linq;
using SRVTextToImage;   
using System.Drawing.Imaging;
using Common.ViewModel;
using Common;

namespace Asiri_ERP_Paciente.Controllers
{
    public class RHUt07_pacienteController : Controller
    {
        private AsiriContext db = new AsiriContext();
        Common.ViewModel.PacienteBE objPaciente = new Common.ViewModel.PacienteBE();

        // GET: RHUt07_paciente
        public ActionResult Index()
        {
            ViewBag.Inicio = TempData["inicios"];
            var rHUt07_paciente = db.RHUt07_paciente.Include(r => r.RHUt09_persona);
            return View(rHUt07_paciente.ToList());
        }

        // GET: RHUt07_paciente/Details/5
        public ActionResult Details(long? id)
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


        #region CREATE PACIENTE
        // GET: RHUt07_paciente/Create

        public ActionResult Create()
        {
            //PERSONA:
            ViewData["rHUt09_persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil");
            var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");
            //SELECT REGION
            ViewData["idRegion"] = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion");
            //SELECT PROVINCIA
            List<SelectListItem> provs = new List<SelectListItem>();
            ViewData["idProvincia"] = provs;
            //SELECT DISTRITO
            List<SelectListItem> list_dists = new List<SelectListItem>();
            ViewData["idDistrito"] = list_dists;
            ViewData["rHUt09_persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia");
            ViewData["rHUt09_persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona");
            //ViewBag.idPersona = new SelectList(db.RHUt09_persona, "idPersona", "tipoPersoneria");
            //SEXO
            var selectListSexo = new SelectList(
                new List<SelectListItem>
                {
                   new SelectListItem {Text = "Hombre", Value = "H"},
                   new SelectListItem {Text = "Mujer", Value = "M"},
                }, "Value", "Text");
            ViewBag.sexo = selectListSexo;
            RHUt09_persona rHUt09_persona = (RHUt09_persona)TempData["rHUt09_persona"];
            ViewBag.idUsuarioView = (rHUt09_persona != null) ? "1" : "1";
            ViewBag.ExisteIdTipDoc = (rHUt09_persona != null) ? rHUt09_persona.idTipoDocIdentidad + "" : "0";
            PacienteBE pacienteBE = new PacienteBE();
            pacienteBE.rHUt09_persona = rHUt09_persona;
            ViewBag.fotoPersona = "";
            ViewBag.pathFoto = "";
            return View(pacienteBE);
        }

        // POST: RHUt07_paciente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idPaciente,codPaciente,numHistoriaClinica,grupoSanguineo,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idPersona")] RHUt07_paciente rHUt07_paciente)
        public ActionResult Create(string pathFoto , string fotoPersona , PacienteBE pacienteBE, int idProvincia = 0, int idRegion = 0, int idDistrito = 0, string sexo = "")
        {
            try
            {
                pacienteBE.rHUt09_persona.idUsuario = "1";
                objPaciente = pacienteBE;
                if (ModelState.IsValid)
                {
                    //DATA FOR DEFAULT
                    pacienteBE.rHUt09_persona.activo = true;
                    pacienteBE.rHUt09_persona.fecRegistro = DateTime.Now;                   
                    if (fotoPersona != "" || fotoPersona !=null)
                    {
                        string [] foto = fotoPersona.Split(',');
                        string b = "";
                        foreach (string word in foto)
                        {
                            b = word;
                        }
                        var img = Convert.FromBase64String(b);
                    }
                    //
                    //var stringFoto = "data:image/jpg;base64,";
                    //var img2 = Convert.ToBase64String(img);
                    db.RHUt09_persona.Add(pacienteBE.rHUt09_persona);

                    //db.RHUt07_paciente.Add(pacienteBE.rHUt07_paciente);
                    db.SaveChanges();

                    //AGREGANDO AL PACIENTE
                    pacienteBE.rHUt07_paciente.fecRegistro = DateTime.Today;
                    pacienteBE.rHUt07_paciente.idUsuario = "1";
                    pacienteBE.rHUt07_paciente.activo = true;
                    int idPersona = (int)db.RHUt09_persona.Where(x => x.numDocIdentidad == pacienteBE.rHUt09_persona.numDocIdentidad && x.idTipoDocIdentidad == pacienteBE.rHUt09_persona.idTipoDocIdentidad).Select(y => y.idPersona).Single();
                    pacienteBE.rHUt07_paciente.codPaciente = genCodPaciente(idPersona);
                    pacienteBE.rHUt07_paciente.numHistoriaClinica = numHistoriaClinica();
                    db.RHUt07_paciente.Add(pacienteBE.rHUt07_paciente);
                    db.SaveChanges();

                    TempData["inicios"] = "Se registro con exito"; 
                    return RedirectToAction("Index");
                }

                ViewBag.Message = string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

                //ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();

                ViewData["rHUt09_persona.idEstadoCivil"] = new SelectList(db.RHUt05_estadoCivil, "idEstadoCivil", "descEstadoCivil", pacienteBE.rHUt09_persona.idEstadoCivil);
                var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad", pacienteBE.rHUt09_persona.idTipoDocIdentidad);
                ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");
                ViewData["rHUt09_persona.idVia"] = new SelectList(db.UBIt04_via, "idVia", "descVia", pacienteBE.rHUt09_persona.idVia);
                ViewData["rHUt09_persona.idZona"] = new SelectList(db.UBIt05_zona, "idZona", "descZona", pacienteBE.rHUt09_persona.idZona);

                var idRegionFind = idRegion;
                var idProvinciaFind = idProvincia;
                var idDistritoFind = idDistrito;
                if (idRegionFind > 0)
                {
                    ViewBag.idRegion = new SelectList(db.UBIt03_region, "idRegion", "nombreRegion", idRegionFind);
                    ViewBag.idProvincia = new SelectList(db.UBIt02_provincia.Where(p => p.idRegion == idRegionFind), "idProvincia", "nombreProvincia");
                    if (idProvinciaFind > 0)
                    {
                        ViewBag.idProvincia = new SelectList(db.UBIt02_provincia, "idProvincia", "nombreProvincia", idProvinciaFind);
                        ViewBag.idDistrito = new SelectList(db.UBIt01_distrito.Where(p => p.idProvincia == idProvinciaFind), "idDistrito", "nombreDistrito");
                        if (idDistritoFind > 0)
                        {
                            List<SelectListItem> list_dists = new List<SelectListItem>();
                            ViewBag.idDistrito = new SelectList(db.UBIt01_distrito, "idDistrito", "nombreDistrito", idDistritoFind); ;
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
                var selectListSexo = new SelectList(
                    new List<SelectListItem>
                    {
                   new SelectListItem {Text = "Hombre", Value = "H"},
                   new SelectListItem {Text = "Mujer", Value = "M"},
                    }, "Value", "Text");
                ViewBag.sexo = selectListSexo;
                if (sexo != "" || sexo != null)
                {
                    var indice = "H";
                    ViewBag.sexo = (sexo == indice) ? indice = "H" : indice = "M";
                    ViewBag.sexo = new SelectList(selectListSexo, "Value", "Text", "H");
                }
                else
                {
                    ViewBag.sexo = selectListSexo;
                }
                ViewBag.Existe = (pacienteBE.rHUt09_persona != null) ? pacienteBE.rHUt09_persona.idTipoDocIdentidad + "" : "0";

                //FOTO
                if(fotoPersona!="" || fotoPersona != "")
                {
                    ViewBag.fotoPersona = fotoPersona;
                    ViewBag.pathFoto = pathFoto;
                }

                return View(objPaciente);
            }
            catch (Exception e)
            {
                ViewBag.Error = e;
                return View(objPaciente);
                throw;
            }
            
        }

        #endregion

        // GET: RHUt07_paciente/Edit/5
        public ActionResult Edit(long? id)
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
            ViewBag.idPersona = new SelectList(db.RHUt09_persona, "idPersona", "tipoPersoneria", rHUt07_paciente.idPersona);
            return View(rHUt07_paciente);
        }

        // POST: RHUt07_paciente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,codPaciente,numHistoriaClinica,grupoSanguineo,activo,fecRegistro,fecModificacion,fecEliminacion,idUsuario,idUsuarioModificar,idUsuarioEliminar,idPersona")] RHUt07_paciente rHUt07_paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rHUt07_paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPersona = new SelectList(db.RHUt09_persona, "idPersona", "tipoPersoneria", rHUt07_paciente.idPersona);
            return View(rHUt07_paciente);
        }

        // GET: RHUt07_paciente/Delete/5
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

        // POST: RHUt07_paciente/Delete/5
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


        public ActionResult PreCreateSecond()
        {
            var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");
            ViewBag.Inicio = "0";

            var Date1 = DateTime.Today;
            var Date2 = DateTime.Today.AddDays(1);
            var resultado = from per in db.RHUt09_persona
                            where per.fecRegistro >= Date1 && per.fecRegistro < Date2
                            select new { per.idPersona, per.nombrePersona };
            ViewBag.NumPaciente = resultado.Count().ToString()+"";
             return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreCreateSecond(RHUt09_persona rHUt09_persona, string captcha = "")
        {
            try
            {
                var status = (this.Session["Seguridad"].ToString() == captcha);
                ViewBag.Message = status ? "Google reCaptcha validación exitosa. :D." : "Google reCaptcha validación fallida. :C";
                rHUt09_persona.idUsuario = "1";
                rHUt09_persona.tipoPersoneria = (rHUt09_persona.idTipoDocIdentidad == 3) ? "J" : "N";
                if (ModelState.IsValid && status == true)
                {
                    var Existe = ValidarExistenciaPersona(rHUt09_persona.numDocIdentidad, rHUt09_persona.idTipoDocIdentidad);
                    var Direccion = (Existe == true) ? "PreCreateSecond" : "Create";
                    if (Existe == true)
                    {
                        ViewBag.Existe = "El Documento: " + rHUt09_persona.numDocIdentidad + " ya existe en nuestro registro.";
                        return View(rHUt09_persona);
                    }
                    else
                    {                 
                        if (rHUt09_persona.difunto ==true)
                        {
                            TempData["rHUt09_persona"] = rHUt09_persona;
                            return RedirectToAction(Direccion);
                        }
                        else
                        {
                            int idTipoDocIdentidadValidar = rHUt09_persona.idTipoDocIdentidad;
                            switch (idTipoDocIdentidadValidar)
                            {

                                case 1:
                                    var ObjPersona = validarDni(rHUt09_persona.numDocIdentidad, 1);
                                    Tecactus.Api.Reniec.Person person = new Person();
                                    person = (Person)TempData["person"];
                                    if (person.dni != null)
                                    {
                                        return RedirectToAction(Direccion);
                                    }
                                    else
                                    {
                                        ViewBag.Error = "No se pudo realizar la consulta.";
                                        return View(rHUt09_persona);
                                    }
                                //break;
                                case 3:
                                    var ObjPersonaRuc = validarRuc(rHUt09_persona.numDocIdentidad, 3);
                                    Tecactus.Api.Sunat.Company company = new Tecactus.Api.Sunat.Company();
                                    company = (Company)TempData["company"];
                                    if (company.ruc != null)
                                    {
                                        return RedirectToAction(Direccion);
                                    }
                                    else
                                    {
                                        ViewBag.Error = "No se pudo realizar la consulta.";
                                        return View(rHUt09_persona);
                                    }
                                //break;
                                default:
                                    rHUt09_persona.tipoPersoneria = "N";
                                    TempData["rHUt09_persona"] = rHUt09_persona;
                                    //TempData["Doc"] = rHUt09_persona;
                                    return RedirectToAction(Direccion);
                                    //break;
                            }
                            //return RedirectToAction(Direccion);
                        }
                    }
                        
                }
                var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
                ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");
                //MOSTRAR EL MODAL (1)
                ViewBag.Inicio = "1";
                return View(rHUt09_persona);
            }
            catch (Exception e)
            {
                var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
                ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");
                //MOSTRAR EL MODAL (1)
                ViewBag.Inicio = "1";
                return View(rHUt09_persona);
            }
        }

        public ActionResult PreCreate()
        {
            var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreCreate(RHUt09_persona rHUt09_persona)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction("Create");

            }
            var idTipoDocIdentidad = new SelectList(db.RHUt12_tipoDocIdentidad, "idTipoDocIdentidad", "descTipoDocIdentidad");
            ViewData["idTipoDocIdentidad"] = new SelectList(idTipoDocIdentidad, "Value", "Text");
            return View(rHUt09_persona);
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
                    //RHUt09_persona rHUt09_persona = new RHUt09_persona();
                    //rHUt09_persona.numDocIdentidad = numDocIdentidad;
                    //rHUt09_persona.idTipoDocIdentidad = idTipoDocIdentidad;
                    ////TempData["rHUt09_persona"] = rHUt09_persona;
                    return Json("0");
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
                    TempData["person"] = person;
                    RHUt09_persona rHUt09_persona = new RHUt09_persona();
                    rHUt09_persona.numDocIdentidad = person.dni;
                    rHUt09_persona.nombrePersona = person.nombres;
                    rHUt09_persona.apellidoPaterno = person.apellido_paterno;
                    rHUt09_persona.apellidoMaterno = person.apellido_materno;
                    rHUt09_persona.idTipoDocIdentidad = idTipoDocIdentidad;
                    rHUt09_persona.tipoPersoneria = "N";
                    TempData["rHUt09_persona"] = rHUt09_persona;
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
                    TempData["company"] = company;
                    RHUt09_persona rHUt09_persona = new RHUt09_persona();
                    rHUt09_persona.numDocIdentidad = company.ruc;
                    rHUt09_persona.razonSocial = company.razon_social;
                    rHUt09_persona.direccion01 = company.direccion;
                    rHUt09_persona.idTipoDocIdentidad = idTipoDocIdentidad;
                    rHUt09_persona.tipoPersoneria = "J";
                    TempData["rHUt09_persona"] = rHUt09_persona;
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

        #region VALIDAR CAPTCHA

        [HttpGet]
        [OutputCache(NoStore =true, Duration =0,VaryByParam ="None")]
        public FileResult Captcha()
        {
            CaptchaRandomImage obj = new CaptchaRandomImage();
            this.Session["Seguridad"] = obj.GetRandomString(3);
            obj.GenerateImage(this.Session["Seguridad"].ToString(), 300, 75, System.Drawing.Color.WhiteSmoke,System.Drawing.Color.Blue);
            MemoryStream stream = new MemoryStream();
            obj.Image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");

        }

        #endregion

        #region CODPACIENTE_HISTORIA CLINICA
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

        #endregion
    }
}
