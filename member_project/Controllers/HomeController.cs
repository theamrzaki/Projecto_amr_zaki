using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace member_project.Controllers
{
    public class HomeController : Controller
    {
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

        public JsonResult function_mvc(int value, int value2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }
            int _double = value + value2;
            return new JsonResult { Data = new { _double = _double } };
        }
    }
}