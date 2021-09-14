using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        [HttpGet] //http://localhost:[port]/api/users
        public ActionResult<List<string>> GetAllNameFromStaticDbClass()
        {
            return StatusCode(StatusCodes.Status200OK, HomeworkStaticDb.UserNames);
        }

        [HttpGet("{userId}")] //http://localhost:[port]/api/users/0
        public ActionResult<string> GetNameById(int userId)
        {
            try
            {
                if (userId < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value");
                }
                if (userId >= HomeworkStaticDb.UserNames.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no note with userId {userId} !");
                }
                return StatusCode(StatusCodes.Status200OK, HomeworkStaticDb.UserNames[userId]);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost] //http://localhost:[port]/homework.html
        public IActionResult AddNewUserPostMethod()
        {
            try
            {
                using(StreamReader addUserStreamReader = new StreamReader(Request.Body))
                {
                    string addUser = addUserStreamReader.ReadToEnd();
                    HomeworkStaticDb.UserNames.Add(addUser);
                    return StatusCode(StatusCodes.Status201Created, "The note was created");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpDelete]
        public IActionResult DeleteExistingUser()
        {
            try
            {
                using (StreamReader DeleteUserStreamReader = new StreamReader(Request.Body))
                {
                    string deleteUser = DeleteUserStreamReader.ReadToEnd();
                    int accessById = Int32.Parse(deleteUser);
                    if (accessById < 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value");
                    }
                    if (accessById >= HomeworkStaticDb.UserNames.Count)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, $"There is no note with userId {accessById}");
                    }
                    HomeworkStaticDb.UserNames.RemoveAt(accessById);
                    return StatusCode(StatusCodes.Status204NoContent, "The note was deleted");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
