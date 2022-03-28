using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Controllers
{
    public class GenresController : Controller
    {
        private Manager m = new Manager();
        // GET: Genres
        public ActionResult Index()
        {
            var genre = m.GenreGetAll();
            return View(genre);
        }

        
    }
}
