﻿using F2021A5HBL.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2021A5HBL.Models
{
    public class ArtistAddFormViewModel : ArtistAddViewModel
    {

        [Display(Name = "Artist's primary genre")]
        public SelectList ArtistGenreList { get; set; }
    }
}