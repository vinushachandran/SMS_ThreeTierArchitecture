/// <summary>
/// This class represents the home controller
/// </summary>
/// <author>Vinusha</author>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       
    }
}