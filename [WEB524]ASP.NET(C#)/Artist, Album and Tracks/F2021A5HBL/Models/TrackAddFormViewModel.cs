using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Models
{
    public class TrackAddFormViewModel : TrackAddViewModel
    {
       
        [Display(Name = "Track genre")]
        public SelectList TrackGenreList { get; set; }
    }
}