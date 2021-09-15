using FavoriteMovies.Enums;
using FavoriteMovies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMovies : Controller
    {
        [HttpGet]  //api/favoritemovies
        public ActionResult<List<Movies>> Get()  // ја печати статичната листа од StaticDb
        {
            try
            {
                return Ok(StaticDb.Movies);
            }
            catch (Exception)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("{index}")] //api/favoritemovies/2
        public ActionResult<Movies> Get(int index)  // печати id од class од StaticDb
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the index can not be negative!");
                }
                if (index >= StaticDb.Movies.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Resource with index {index} does not exist!");
                }
                return Ok(StaticDb.Movies[index]);
            }
            catch (Exception)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }


        [HttpGet("filter/{year}")]  // api/favoritemovies/filter/2000
        public ActionResult<List<Movies>> filterYearFromQuery(int? year = null)
        {
            try
            {
                if (year == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }
                else
                {
                    List<Movies> moviesDb = StaticDb.Movies.Where(x => x.Year == year).ToList();
                    return Ok(moviesDb);
                }
            }
            catch (Exception)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("filterGenre/{genreId}")] // api/favoritemovies/filterGenre/2
        public ActionResult<List<Movies>> filterGenreFromQuery(int? genreId = null)
        {
            try
            {
                if (genreId == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }
                else
                {
                    List<Movies> moviesDb = StaticDb.Movies.Where(x => x.Genre == (Genre)genreId).ToList();
                    return Ok(moviesDb);
                }
            }
            catch (Exception)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        //[Route("Genre/{findGenre}")]  //api/favoritemovies/Genre/
        //[HttpPost]  //api/
        //public Genre? Genre(string findGenre)
        //{
        //    Genre parsedGenre;
        //    if (Enum.TryParse<Genre>(findGenre, true, out parsedGenre))
        //    {
        //        return parsedGenre;
        //    }
        //    return null;
        //}

        [HttpPost]
        public IActionResult AddNewMovies([FromBody] Movies movies)
        {
            try
            {
                StaticDb.Movies.Add(movies);
                return StatusCode(StatusCodes.Status201Created, "Movie was added");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occurred");
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(Request.Body))
                {
                    string requestBody = streamReader.ReadToEnd();
                    int index = Int32.Parse(requestBody);
                    if (index < 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value!");
                    }
                    if (index >= StaticDb.Movies.Count)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, $"There is no note with index {index}");
                    }
                    StaticDb.Movies.RemoveAt(index);
                    return StatusCode(StatusCodes.Status204NoContent, "The note was deleted");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }

        }

        [HttpGet("director/{country}")]
        public ActionResult<List<Director>> filteredByDirector(string country)
        {
            try
            {
                if (string.IsNullOrEmpty(country))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }
                Director directorDb = StaticDb.Director.FirstOrDefault(x => x.Country == country);
                if (directorDb == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"User with country {country} was not found!");
                }
                return Ok(StaticDb.Movies.Where(x => x.Director.));
            }
            catch (Exception)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
