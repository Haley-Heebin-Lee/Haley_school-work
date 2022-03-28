using F2021A5HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5HBL.Models
{
    public class AlbumAddViewModel
    {
        public AlbumAddViewModel()
        {
            TrackIds = new List<int>();
            ArtistIds = new List<int>();
            Artists = new List<Artist>();
            Tracks = new List<Track>();
            ReleaseDate = DateTime.Now;
        }

        [Required, StringLength(120)]
        [Display(Name = "Album name")]
        public string Name { get; set; }


        [Display(Name = "Release Date"), DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Coordinator { get; set; }
        [StringLength(500)]
        [Display(Name = "URL to album image(cover art)")]
        public string UrlAlbum { get; set; }

        [Required]
        public string Genre { get; set; }
        //item selection


        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public IEnumerable<int> ArtistIds { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
        //=================how about tracks?
        public int TrackId { get; set; }
        public string TrackName { get; set; }

        public IEnumerable<int> TrackIds { get; set; }
        public IEnumerable<Track> Tracks { get; set; }
    }
}