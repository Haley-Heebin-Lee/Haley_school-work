using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A3HBL.Models
{
    public class TrackAddFormViewModel:TrackAddViewModel
    {
        [Display(Name = "Album")]
        public SelectList AlbumList { get; set; }
        public string AlbumTitle;

        [Display(Name = "Media Type")]
        public SelectList MediaTypeList { get; set; }
        public string MdeiaTypeName;
    }
}