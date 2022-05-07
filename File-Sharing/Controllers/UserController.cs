using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Services;
using Microsoft.AspNetCore.Mvc;

namespace File_Sharing.Controllers
{
    public class UserController : Controller
    {
        IUser db;

        public UserController(IUser _db)
        {
            db = _db;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return Ok("User Deleted.");
        }

      
    }
}
