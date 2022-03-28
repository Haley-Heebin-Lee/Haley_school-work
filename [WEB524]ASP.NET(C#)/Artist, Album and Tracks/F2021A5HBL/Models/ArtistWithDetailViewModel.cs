using F2021A5HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2021A5HBL.Models
{
    public class ArtistWithDetailViewModel : ArtistBaseViewModel
    {
        public ArtistWithDetailViewModel()
        {
            Albums = new List<Album>();
            AlbumNames = new List<string>();
        }

        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<string> AlbumNames { get; set; }

    }
}