using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Data.Core.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string Released { get; set; }

        public string Director { get; set; }

        public string Writer { get; set; }

        public string Actors { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }

        public string Poster { get; set; }

        public string ImdbRating { get; set; }

        public string ImgVotes { get; set; }

        public string ImgId { get; set; }

        public string Type { get; set; }

        public string Production { get; set; }
    }
}
