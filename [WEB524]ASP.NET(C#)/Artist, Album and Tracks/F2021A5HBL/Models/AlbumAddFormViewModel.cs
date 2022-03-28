using F2021A5HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Models
{
    public class AlbumAddFormViewModel : AlbumAddViewModel
    {
        

        [Display(Name = "Gernres List")]
        public SelectList AlbumGenreList { get; set; }

        [Display(Name = "Artists List")]
        public MultiSelectList ArtistList { get; set; }

        [Display(Name = "Tracks List")]
        public MultiSelectList TrackList { get; set; }
    }
}