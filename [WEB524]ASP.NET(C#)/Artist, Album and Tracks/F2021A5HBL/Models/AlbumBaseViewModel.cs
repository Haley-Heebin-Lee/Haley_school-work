using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5HBL.Models
{
    public class AlbumBaseViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Coordinator who looks after the album")]
        public string Coordinator { get; set; }
        // holds the username (e.g. amanda@example.com) of the authenticated user who is in the process of adding a new Album object.
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }
        //item selection

        [Required, StringLength(120)]
        [Display(Name = "Album name")]
        public string Name { get; set; }


        [Display(Name = "Release date")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { get; set; }

        [StringLength(500)]
        [Display(Name = "Album image(cover art)")]
        public string UrlAlbum { get; set; }
        // hold a URL to an image of the album.
    }
}