using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A3HBL.Controllers
{
    public class MediaTypesController : Controller
    {
        private Manager m = new Manager();
        // GET: MediaTypes
        public ActionResult Index()
        {
            var media = m.MediaTypeGetAll();
            return View(media);
        }

        // GET: MediaTypes/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/

        
    }
}
