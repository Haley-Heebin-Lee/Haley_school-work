using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A3HBL.Models
{
    public class TrackWithDetailViewModel: TrackBaseViewModel
    {
        public MediaTypeBaseViewModel MediaType { get; set; }

        [StringLength(160)]
        [Display(Name="Album Title")]
        public string AlbumTitle { get; set; }

        [StringLength(120)]
        [Display(Name = "Artist name")]
        public string AlbumArtistName { get; set; }
    }
}