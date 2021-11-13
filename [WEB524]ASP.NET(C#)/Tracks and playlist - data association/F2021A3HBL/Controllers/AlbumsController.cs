using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A3HBL.Controllers
{
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();
        // GET: Albums
        public ActionResult Index()
        {
            var albums = m.AlbumGetAll();
            return View(albums);
        }

        // GET: Albums/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/

    }
}
