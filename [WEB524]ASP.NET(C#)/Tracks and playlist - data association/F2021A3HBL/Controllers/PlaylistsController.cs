using F2021A3HBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A3HBL.Controllers
{
    public class PlaylistsController : Controller
    {
        private Manager m = new Manager();
        // GET: Playlists
        public ActionResult Index()
        {
            var allPlaylist = m.PlaylistGetAll();
            return View(allPlaylist);
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            var playlist = m.PlaylistGetById(id.GetValueOrDefault());
            if(playlist == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(playlist);
            }
            
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.PlaylistGetById(id.GetValueOrDefault());
            if(o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = m.mapper.Map<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>(o);

                var selectedValues = o.Tracks.Select(t => t.TrackId);

                form.TrackList = new MultiSelectList(items: m.TrackGetAll(),
                    dataValueField: "TrackId",
                    dataTextField: "NameFull",
                    selectedValues: selectedValues);

                form.TracksOnPlaylist = o.Tracks.OrderBy(n => n.Name);
                return View(form);
            }
            
        }

        // POST: Playlists/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, PlaylistEditTracksViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = newItem.Id });
            }

            if(id.GetValueOrDefault() != newItem.Id)
            {
                return RedirectToAction("Index");
            }
            var editedItem = m.PlaylistEditTracks(newItem);

            if (editedItem == null)
            {
                return RedirectToAction("Edit", new { id = newItem.Id });
            }
            else
            {
                return RedirectToAction("Details", new { id = newItem.Id });
            }
        }

        
    }
}
