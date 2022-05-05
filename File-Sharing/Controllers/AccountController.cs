using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Repository;
using File_Sharing.Services;
using Microsoft.AspNetCore.Mvc;

namespace File_Sharing.Controllers
{
    public class AccountController : Controller
    {
        IUser _db;
        public AccountController(IUser db)
        {
            _db = db;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            //register form.
            return View();
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserCreate userRequest)
        {

            if (_db.IsMailExist(userRequest.Email))
            {
                ModelState.AddModelError("Email", "This Mail Already Using.");
            }

            if (!ModelState.IsValid)
            {

                return View(userRequest);
            }

            User user = new User()
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                Password = userRequest.Password,
            };

            _db.Create(user);


            //Show message
            return RedirectToAction("Index", "Home");
        }

    }
}
