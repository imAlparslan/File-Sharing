using File_Sharing.Data;
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

        [HttpGet]
        public IActionResult Create()
        {
            //register form.
            return View();
        }


        [HttpPost]
        public IActionResult Create(User user)
        {
            //check mail
            db.Create(user); 


            //Show message
            return Ok(user);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return Ok("User Deleted.");
        }

    }
}
