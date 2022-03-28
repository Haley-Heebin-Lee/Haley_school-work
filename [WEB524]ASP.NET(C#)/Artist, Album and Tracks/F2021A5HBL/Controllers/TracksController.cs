using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = m.TrackGetAll();
            return View(tracks);
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var track = m.TrackGetById(id.GetValueOrDefault());
            if(track == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(track);
            }
            
        }

        
    }
}
