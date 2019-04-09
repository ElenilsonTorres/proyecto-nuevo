using SistemWalter.Context;
using SistemWalter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemWalter.Controllers
{
    public class HomeController : Controller
    {
        private SistemadeAguaEntities db = new SistemadeAguaEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        [HttpPost]
        public ActionResult lecmensu(int filtro)
        {
            var d= (from p in db.Pagos  where p.Fecha_Registro.Value.Month == filtro select p ).ToList();

            foreach(var i in d)
            {

            }

            

          

            return View();
  
        }
    }
    }
