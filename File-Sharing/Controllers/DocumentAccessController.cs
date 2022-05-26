using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Services;
using Microsoft.AspNetCore.Mvc;

namespace File_Sharing.Controllers
{
    public class DocumentAccessController : Controller
    {
        IDocumentAccess _db;

        public DocumentAccessController(IDocumentAccess db)
        {
            _db = db;
        }
        
        [Route("share/{fileId:int}/{friendshipId:int}")]
        public IActionResult Share(int fileId, int friendshipId)
        {
            int id = fileId;
            int userId = GetAuthenticatedUserId();
            _db.Create(userId, fileId, friendshipId);
            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "DocumentAccess", action = "Index", fileId = id }
                ));
        }


        [HttpGet]
        public IActionResult Index(int fileId)
        {
            int userId = GetAuthenticatedUserId();
            List<DocumentAccess> accesses = _db.GetAccessorsList(userId, fileId); 
            List<Friendship> friendships = _db.GetFriendsList(userId);
            
            List<Friend> friends = new List<Friend>();
            List<Accessor> accessors = new List<Accessor>();

            foreach (DocumentAccess access in accesses)
            {
                accessors.Add(new Accessor()
                {
                    AccessId = access.Id,
                    AccessorName = _db.GetUserNameById(access.AccessorId),
                    AccessorId = access.AccessorId,
                    GivenTime = access.GivenTime,

                }) ; ;
            }
            foreach (Friendship friend in friendships)
            {
                int ownerId = GetAuthenticatedUserId() ;
                int friendId = _db.GetFriendId(ownerId, friend.Id);
                friends.Add(new Friend(){
                    FriendshipId = friend.Id,
                    Name = _db.GetUserNameById(friendId),
                    Email = "Email"
                });

            }
            ViewBag.fileId = fileId;
            ViewData["friends"] = friends;
            return View(accessors);
        }

        [HttpGet]
        public IActionResult EndSharing(int accessId)
        {
            int id = _db.GetFileId(accessId);
            _db.Delete(accessId);

            return RedirectToAction("Index", new RouteValueDictionary(
             new { controller = "DocumentAccess", action = "Index", fileId = id }
             ));
        }

        [HttpGet]
        [Route("shared")]
        public IActionResult Shared()
        {
            
            int userId = GetAuthenticatedUserId();
            List<DocumentAccess> myAccessebleFiles = _db.GetMySharings(userId); 
            List<MyFile> mySharingFiles = new List<MyFile>();

            foreach (DocumentAccess documentAccess in myAccessebleFiles)
            {
                Document document = _db.FindDocumentByAccessId(documentAccess.Id);
               
                mySharingFiles.Add(new MyFile() {
                DocumentId = document.Id,
                FileName = document.FileName,
                FilePath = document.FilePath,
                FileSize = document.FileSize,
                UploadDate = documentAccess.GivenTime,

            }); 
            }



            return View(mySharingFiles);
        }

        [HttpGet]
        [Route("inbox")]
        public IActionResult SharingWithMe()
        {
            int userId = GetAuthenticatedUserId();
            List<DocumentAccess> accesses = _db.GetSharingWithMe(userId);
            List<Inbox> myAccessibleFiles = new List<Inbox>();

            foreach (DocumentAccess documentAccess in accesses)
            {
                Document document = _db.FindDocumentByAccessId(documentAccess.Id);
                myAccessibleFiles.Add(new Inbox()
                {
                    
                    DocumentId = document.Id,
                    FileName = document.FileName,
                    FileOwnerName = _db.GetUserNameById(documentAccess.OwnerId),
                    FilePath = document.FilePath,
                    FileSize = document.FileSize,
                    SharingDate = documentAccess.GivenTime


                });

            }



            return View(myAccessibleFiles);
        }


        /*
        [HttpPost]
        public IActionResult test1(int fileId, int friendshipId)
        {
            int id = fileId;
            int userId = GetAuthenticatedUserId();
            _db.Create(userId, fileId, friendshipId);
            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "DocumentAccess", action = "Index", fileId = id }    
                ));
        }
        */

            private int GetAuthenticatedUserId()
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("ID")).Value);
            return userId;
        }
    }
}
