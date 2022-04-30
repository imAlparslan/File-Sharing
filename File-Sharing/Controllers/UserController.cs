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

        [HttpGet]
        [Route("Register")]
        public IActionResult Create()
        {
            //register form.
            return View();
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Create(UserCreate userRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(userRequest);
            }
            //check mail
            User user = new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                Password = userRequest.Password,

            };
            db.Create(user); 


            //Show message
            return RedirectToAction("Index","Home");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return Ok("User Deleted.");
        }

    }
}
