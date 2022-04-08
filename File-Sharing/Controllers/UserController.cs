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


        [HttpPost]
        public IActionResult Create(User user)
        {
            db.Create(user);
            
            string path = "wwwroot/File_Storage/User_"+ user.Id;
            DirectoryInfo drinf = Directory.CreateDirectory(path);
            return Ok(user);
        }

    }
}
