using File_Sharing.Models;
using File_Sharing.Services;
using Microsoft.AspNetCore.Mvc;

namespace File_Sharing.Controllers
{
    public class FriendshipController : Controller
    {
        IFriendship _db;

        public FriendshipController(IFriendship db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("sendrequest")]
        public IActionResult SendRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("sendrequest")]
        public IActionResult SendRequest(FriendshipRequest request)
        {
            //check request valid
            int senderId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("ID")).Value);
            _db.CreateRequest(senderId, request.ReciverEmail);
            return RedirectToAction("Index","Home");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
