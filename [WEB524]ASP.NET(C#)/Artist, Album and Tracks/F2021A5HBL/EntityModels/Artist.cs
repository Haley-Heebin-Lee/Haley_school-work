using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2021A5HBL.EntityModels
{
    public class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
            BirthOrStartDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string BirthName { get; set; }
        public DateTime BirthOrStartDate { get; set; }
        // For an individual person, it is their birth date
        // For all others, it is the date that the artist started working in the music business

        public string Executive { get; set; }
        //y holds the username (e.g. amanda@example.com) of the authenticated user who completed the process of adding a new Artist object.
        public string Genre { get; set; }
        //item selection

        [Required, StringLength(120)]
        public string Name { get; set; }

        [StringLength(500)]
        public string UrlArtist { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}