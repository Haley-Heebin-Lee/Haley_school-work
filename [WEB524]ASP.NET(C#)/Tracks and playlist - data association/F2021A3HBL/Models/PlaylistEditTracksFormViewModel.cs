using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A3HBL.Models
{
    public class PlaylistEditTracksFormViewModel
    {
        public PlaylistEditTracksFormViewModel()
        {
            Tracks = new List<TrackBaseViewModel>();
        }

        [Key]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        [Display(Name = "Playlist Name")]
        public string Name { get; set; }
        public MultiSelectList TrackList { get; set; }
        
        public IEnumerable<TrackBaseViewModel> TracksOnPlaylist { get; set; }
        public int TracksCount { get; set; }
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}