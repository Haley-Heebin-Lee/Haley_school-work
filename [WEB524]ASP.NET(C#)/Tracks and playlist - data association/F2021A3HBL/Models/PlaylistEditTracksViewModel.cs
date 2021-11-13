using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2021A3HBL.Models
{
    public class PlaylistEditTracksViewModel
    {
        public PlaylistEditTracksViewModel()
        {
            TrackId = new List<int>();
        }

        public int Id { get; set; }

        public IEnumerable<int> TrackId { get; set; }
    }
}