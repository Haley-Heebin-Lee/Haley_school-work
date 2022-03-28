using F2021A5HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2021A5HBL.Models
{
    public class AlbumWithDetailViewModel : AlbumBaseViewModel
    {
        public AlbumWithDetailViewModel()
        {
            Artists = new List<Artist>();
            Tracks = new List<Track>();
            ArtistNames = new List<string>();
        }
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<Track> Tracks { get; set; }
        public IEnumerable<string> ArtistNames { get; set; }
    }
}