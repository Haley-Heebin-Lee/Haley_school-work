using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Controllers
{
   
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData

      
        public ActionResult Index()
        {
            if (m.LoadData())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

        public ActionResult RemoveArtist()
        {
            if (m.RemoveDataArtists())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }
        public ActionResult RemoveTrack()
        {
            if (m.RemoveDataTracks())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }
        public ActionResult RemoveAlbum()
        {
            if (m.RemoveDataAlbums())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult LoadData()
        {
            if (m.LoadData())
            {

                return Content("role data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        
        public ActionResult Artist()
        {
            if (m.LoadDataArtist())
            {
                return Content("artist data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        
        public ActionResult Album()
        {
            if (m.LoadDataAlbum())
            {
                return Content("album data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        //[Authorize(Roles = "Clerk")]
        public ActionResult Track()
        {
            if (m.LoadDataTrack())
            {
                return Content("track data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }

        public ActionResult Genre()
        {
            if (m.LoadDataGenre())
            {
                return Content("genre data has been added!");
            }
            else
            {
                return Content("data exists already");
            }
        }
    }
}