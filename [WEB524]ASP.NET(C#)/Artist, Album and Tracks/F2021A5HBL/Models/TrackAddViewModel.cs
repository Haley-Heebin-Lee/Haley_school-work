using F2021A5HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5HBL.Models
{
    public class TrackAddViewModel
    {
        public TrackAddViewModel()
        {
            Albums = new List<Album>();
        }
        public string Clerk { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Composer name (comma-separated)")]
        public string Composers { get; set; }

        public string Genre { get; set; } //holds a genre string value

        [Required]
        [StringLength(40)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public IEnumerable<Album> Albums { get; set; }
    }
}