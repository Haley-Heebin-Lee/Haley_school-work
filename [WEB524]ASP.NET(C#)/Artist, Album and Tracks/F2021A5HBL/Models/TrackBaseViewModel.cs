using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5HBL.Models
{
    public class TrackBaseViewModel
    {
        public int Id { get; set; }

        [StringLength(40)]
        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }
        //holds the username (e.g. amanda@example.com) of the authenticated user who is in the process of adding a new Track object.

        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [StringLength(30)]
        [Display(Name = "Track genre")]
        public string Genre { get; set; }
        //item selection

        [Required]
        [StringLength(60)]
        [Display(Name = "Track name")]
        public string Name { get; set; }
    }
}