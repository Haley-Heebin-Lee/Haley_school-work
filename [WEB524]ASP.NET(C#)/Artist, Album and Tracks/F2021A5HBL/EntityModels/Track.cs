using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2021A5HBL.EntityModels
{
    public class Track
    {
        public Track()
        {
            Albums = new HashSet<Album>();
        }
        public int Id { get; set; }
        public string Clerk { get; set; }
        //y holds the username (e.g. amanda@example.com) of the authenticated user who is in the process of adding a new Track object.
        public string Composers { get; set; }
        public string Genre { get; set; }
        //item selection
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}