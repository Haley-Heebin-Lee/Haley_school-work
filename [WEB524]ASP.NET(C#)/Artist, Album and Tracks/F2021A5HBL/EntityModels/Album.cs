using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5HBL.EntityModels
{
    public class Album
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
            ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Coordinator { get; set; }
        // holds the username (e.g. amanda@example.com) of the authenticated user who is in the process of adding a new Album object.
        public string Genre { get; set; }
        //item selection

        [Required, StringLength(120)]
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }

        [StringLength(500)]
        public string UrlAlbum { get; set; }
        // hold a URL to an image of the album.
        public ICollection<Artist> Artists { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}