using F2021A3HBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A3HBL.Controllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            var tracks = m.TrackGetAllWithDetail();
            return View(tracks);
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var track = m.TrackGetByIdWithDetail(id.GetValueOrDefault());
            if(track == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(track);
            }
           
        }

        // GET: Tracks/Create
        public ActionResult Create()
        {
            var track = new TrackAddFormViewModel();
            track.AlbumList = new SelectList(m.AlbumGetAll(), "AlbumId", "Title");
            track.MediaTypeList = new SelectList(m.MediaTypeGetAll(), "MediaTypeId", "Name");
            return View(track);
        }

        // POST: Tracks/Create
        [HttpPost]
        public ActionResult Create(TrackAddFormViewModel newItem)
        {
            
            if(!ModelState.IsValid)
            {
                return View(newItem);
            }
            try
            {
                var addedItem = m.TrackAdd(newItem);
                if(addedItem == null)
                    return View(newItem);
                else
                    return RedirectToAction("Details", new { id = addedItem.TrackId});
            }
            catch
            {
                return View(newItem);
            }
        }

    
    }
}
