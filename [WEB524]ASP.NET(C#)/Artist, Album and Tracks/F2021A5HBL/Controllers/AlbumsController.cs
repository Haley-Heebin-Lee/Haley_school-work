using F2021A5HBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Controllers
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
        public ActionResult Details(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());
            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(album);
            }
        }

        //GET
        [Route("albums/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                var trackAddForm = new TrackAddFormViewModel();
                trackAddForm.AlbumId = album.Id;
                trackAddForm.AlbumName = album.Name;
                trackAddForm.TrackGenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                return View(trackAddForm);
            }
        }

        //POST
        [Route("albums/{id}/addtrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel track)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(track);
                }

                var addedTrack = m.TrackAdd(track);

                if (addedTrack == null)
                {
                    return View(track);
                }
                else
                {
                    return RedirectToAction("details", "tracks", new { id = addedTrack.Id });
                }
            }
            catch
            {
                return View();
            }
        }



    }
}
