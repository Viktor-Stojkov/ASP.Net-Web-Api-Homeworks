using FavoriteMovies.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteMovies.Models
{
    public class Movies : BaseEntity
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public List<Director> Director { get; set; }
    }
}
