using F2021A5HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5HBL.Models
{
    public class ArtistAddViewModel
    {
        public ArtistAddViewModel()
        {
            Albums = new List<Album>();
            BirthOrStartDate = DateTime.Now;
        }

        [Required, StringLength(120)]
        [Display(Name = "Artist name or stage Name")]
        public string Name { get; set; }

        [StringLength(40), Display(Name = "if applicable, artist's birth name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }

        [Required]
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }
        //item selection

        [StringLength(40)]
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }


        [StringLength(500)]
        [Display(Name = "URL to artist photo")]
        public string UrlArtist { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}