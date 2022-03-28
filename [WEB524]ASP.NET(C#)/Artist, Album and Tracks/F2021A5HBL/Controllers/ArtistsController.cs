using F2021A5HBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Controllers
{
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();


        // GET: Artists
        [Authorize(Roles = "Executive, Coordinator")]
        public ActionResult Index()
        {
            var artists = m.ArtistGetAll();
            return View(artists);
        }

        // GET: Artists/Details/5
        [Authorize(Roles = "Executive, Coordinator")]
        public ActionResult Details(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());
            if(artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(artist);
            }
            
        }

        // GET: Artists/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var artist = new ArtistAddFormViewModel();

            artist.Executive = User.Identity.Name;
            artist.ArtistGenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            //1. genre name-> from genre
            //2. genre name from artist
            return View(artist);
        }

        // POST: Artists/Create
        [Authorize(Roles = "Executive")]
        [HttpPost]
        public ActionResult Create(ArtistAddFormViewModel newItem)
        {
            newItem.Executive = User.Identity.Name;

            if (!ModelState.IsValid)
            {
               newItem.ArtistGenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                return View(newItem);
            }
            try
            {
                var addedArtist = m.ArtistAdd(newItem);

                if (addedArtist == null)
                {
                    return View(newItem);
                }
                else
                {
                    return RedirectToAction("details", new { id = addedArtist.Id });
                }
            }
            catch
            {
                return View(newItem);
            }
        }

        //GET
        [Authorize(Roles = "Executive, Coordinator")]
        [Route("artists/{id}/addalbum")]
        public ActionResult AddAlbum(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                var newAlbum = new AlbumAddFormViewModel();

                newAlbum.ArtistId = artist.Id;
                newAlbum.ArtistName = artist.Name;
                newAlbum.AlbumGenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

                var selectedArtistsIds = new List<int> { artist.Id };
                newAlbum.ArtistList = new MultiSelectList(m.ArtistGetAll(), "Id", "Name", selectedArtistsIds);
                newAlbum.TrackList = new MultiSelectList(m.TrackGetAllByArtistId(artist.Id), "Id", "Name");

                return View(newAlbum);
            }
        }

        // POST: Artists/Create
        //Once user input on the page, this function will be triggered.
        [Authorize(Roles = "Executive, Coordinator"), Route("artists/{id}/addalbum")]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddFormViewModel album)
        {
            try
            {
                // TODO: Add insert logic here

                if (!ModelState.IsValid)
                {
                    return View(album);
                }

                var addedAlbum = m.AlbumAdd(album);

                if (addedAlbum == null)
                {
                    return View(album);
                }
                else
                {
                    return RedirectToAction("Details", "Albums", new { id = addedAlbum.Id });
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
