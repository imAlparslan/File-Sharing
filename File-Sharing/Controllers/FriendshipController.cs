using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
            string senderMail = _db.GetUserMailById(senderId);
            User r = _db.GetUserByEmail(request.ReciverEmail);
            
            if (senderMail.Equals(request.ReciverEmail))
            {
                ViewBag.msg = "You cannot sent request by yourself";
                return View("SendRequest");
            }else if (r == null)
            {
                ViewBag.msg = "User not found!";
                return View("SendRequest");
            }

            _db.CreateRequest(senderId, request.ReciverEmail);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Route("friends")]
        public IActionResult Index(int page = 1)
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("ID")).Value);
            List<Friendship> friendships = _db.GetFriends(userId);
            List<Friend> friends = new List<Friend>();

            foreach(Friendship friend in friendships)
            {
                friends.Add(new Friend
                {
                    Name = friend.SenderId == userId ? _db.GetUserNameById(friend.ReciverId) : _db.GetUserNameById(friend.SenderId),
                    Email = friend.SenderId == userId ? _db.GetUserMailById(friend.ReciverId) : _db.GetUserMailById(friend.SenderId),
                    FriendshipId = friend.Id
                });
            }
            PagedList<Friend> pagedList = (PagedList<Friend>)friends.ToPagedList(page, 4);

            return View(pagedList);
        }

        [HttpGet]
        [Route("requests")]
        public IActionResult GetReceivedRequests(int page = 1)
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("ID")).Value);
            List<Friendship> list = _db.GetReceivedRequests(userId);
            List<Request> requests = new List<Request>();
            foreach (Friendship friendship in list)
            {
                requests.Add(new Request
                {
                    FriendshipId = friendship.Id,
                    SenderEmail = _db.GetUserMailById(friendship.SenderId),
                    SenderName = _db.GetUserNameById(friendship.SenderId),
                });
            }
            PagedList<Request> pagedList = (PagedList<Request>)requests.ToPagedList();

            return View(pagedList);
        }

        //[HttpPost]
        [Route("requests/{FriendshipId:int}")]
        public IActionResult AcceptRequest(int FriendshipId)
        {
            _db.AcceptRequest(FriendshipId);
            return RedirectToAction("GetReceivedRequests", "Friendship");

        }
        [Route("reject/{FriendshipId:int}")]
        public IActionResult RejectRequest(int FriendshipId)
        {
            _db.RejectRequest(FriendshipId);
            return RedirectToAction("GetReceivedRequests", "Friendship");

        }


        [Route("delete/{friendshipId:int}")]
        public IActionResult DeleteFriend(int friendshipId)
        {
            // check sharing files.

            _db.Delete(friendshipId);
            return RedirectToAction("Index","Friendship");
            
        }




    }
}
