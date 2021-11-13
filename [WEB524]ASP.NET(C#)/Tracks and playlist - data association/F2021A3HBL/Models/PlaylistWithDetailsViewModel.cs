using F2021A3HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A3HBL.Models
{
    public class PlaylistWithDetailsViewModel:PlaylistBaseViewModel
    {
        
        [Display(Name = "Tracks on Playlist")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}