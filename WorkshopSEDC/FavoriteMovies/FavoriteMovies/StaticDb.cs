using FavoriteMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteMovies
{
    public static class StaticDb
    {
        public static List<Movies> Movies = new List<Movies>()
        {
            new Movies(){ Id = 0, Description= "Welcome to Action Movies", Genre = Enums.Genre.Action,
                Title = "Rambo", Year = 2000 },
            new Movies(){ Id = 0, Description= "Welcome to Fantasy Movies", Genre = Enums.Genre.Fantasy,
                Title = "Rambo", Year = 2000 },
            new Movies(){ Id = 0, Description= "Welcome to Science Movies", Genre = Enums.Genre.Science,
                Title = "Rambo", Year = 2000 },
        };

        public static List<Director> Director = new List<Director>()
        {
            new Director()
            {
                Id = 0,
                FirstName = "Viktor",
                LastName = "Stojkov",
                Country = "MK"
            },

            new Director()
            {
                Id = 0,
                FirstName = "Mathias",
                LastName = "Schneider",
                Country = "DE"
            },

            new Director()
            {
                Id = 0,
                FirstName = "Viktorija",
                LastName = "Lapevska",
                Country = "EN"
            },

        };
    }
}

