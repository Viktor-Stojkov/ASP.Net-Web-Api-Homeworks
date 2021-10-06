using Homework02API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Homework02API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        [HttpGet]  // http://localhost:[controller]/api/books
        public ActionResult<List<string>> PrintStaticBooks()
        {
            try
            {
                return Ok(StaticBooksDb.Books);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("queryString")] // http://localhost:[controller]/api/books/querystring?index=2
        public ActionResult<Book> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, negative value");
                }
                if (index >= StaticBooksDb.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Resource with the {index} does not exist");
                }
                return Ok(StaticBooksDb.Books[index]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("{filteredByAuthor}/{filteredByTitle}")]  // http://localhost:[controller]/api/books/Francesco/La%20Banca%20centrale%20europea
        public ActionResult<List<Book>> FilteredByBook(string filteredByAuthor, string filteredByTitle)
        {
            try
            {
                List<Book> filteredBook = StaticBooksDb.Books.Where(x => x.Author.ToLower().Contains(filteredByAuthor.ToLower())
                                                                        && x.Title == filteredByTitle).ToList();
                return Ok(filteredBook);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("postBookWithOrWithoutDescriptionInPostman")]  // http://localhost:[controller]/api/books/postBookWithOrWithoutDescriptionInPostman
                                                                 // {"author": "Ginna Parle", "title": "Touch me"}
                                                                 //                     ||
                                                                 // {"author": "Ginna Parle", "title": "Touch me", "Description":[{"regionofproduct": "Europa", "country": "France", "year": 2020}]}
        public IActionResult postBookWithOrWithoutDescriptionInPostman([FromBody] Book book)
        {
            try
            {
                StaticBooksDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "We have created new Book");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("listOfTitles")]
        public ActionResult<List<string>> PostBooks([FromBody] List<Book> books)
        {
            try
            {
                List<string> title = books.Select(x => x.Title).ToList();
                return StatusCode(StatusCodes.Status200OK, title);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
