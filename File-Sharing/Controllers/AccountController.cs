using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Repository;
using File_Sharing.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login request)
        {
            var user = _db.GetUserByEmail(request.Email);
            if(user == null)
            {
                ModelState.AddModelError("Email", "Email Does Not Match.");
                return View(request);

            }else if (!user.Password.Equals(request.Password)){
                ModelState.AddModelError("Password", "Invalid Password");
                return View(request);
            }

            
            var claims = new List<Claim>
                {
                    new Claim("type", "User"),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("ID", user.Id.ToString()),
                    
                };
            var userIdentity = new ClaimsIdentity(claims, "CookieAuth");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("index","Home");
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

        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
     
            return View();
        }

    }
}
