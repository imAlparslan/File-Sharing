using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Repository;
using File_Sharing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace File_Sharing.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocument _db;

        public DocumentController(IDocument db)
        {
            _db = db;
        }

        public IActionResult Index(int page = 1)
        {
            int senderId = GetAuthenticatedUserId();

            var myDocuments = _db.GetMyDocuments(senderId);
            List<MyFile> myFiles = new List<MyFile>();
            
          
            foreach (Document document in myDocuments)
            {
                myFiles.Add(new MyFile()
                {
                    FileName = document.FileName,
                    FileSize = document.FileSize,
                    UploadDate = document.UploadDate,
                    FilePath = document.FilePath,
                    DocumentId = document.Id,
                });
            }
         
            var pagedList = (PagedList<MyFile>)myFiles.ToPagedList(page, 4);    

            return View(pagedList);
        }

        [HttpGet]
        public IActionResult Delete(int fileId)
        {
            //delete from folder.

            User user = _db.GetUserById(GetAuthenticatedUserId());
            _db.Delete(fileId);
            return RedirectToAction("Index", "Document");
        }

        [HttpGet]
        public IActionResult Upload()
        {
            if (User != null && User.Identity.IsAuthenticated)
            { 
                User user = _db.GetUserById(GetAuthenticatedUserId());
                ViewBag.UserQuota = user.RemainingQuota;
                return View();
            }
            else
            {
                return RedirectToAction("login", "Account");
            }

        }



        [HttpPost]
        public IActionResult Upload(UploadFile doc)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
            if(doc !=null)
            {
            
            User fileOwner = _db.GetUserById(Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("ID")).Value));

            if(fileOwner.RemainingQuota < doc.File.Length / 1024.00 / 1024.00)
             {

             ViewBag.msg = "You not have enough quota";
             ViewBag.UserQuota = fileOwner.RemainingQuota;
                        return View("Upload");
             }
            var location = Path.Combine(Directory.GetCurrentDirectory(), fileOwner.FolderPath + doc.File.FileName);
            var stream = new FileStream(location, FileMode.Create);
            doc.File.CopyTo(stream);
            Document document = new Document();
            document.OwnerId = fileOwner.Id;
            document.FileName = doc.File.FileName;
            document.FilePath = location;
            document.FileSize = Math.Round(stream.Length/1024.00/1024.00,2);
            

             stream.Close();
            _db.Upload(document);
            
            }

            return RedirectToAction("Index","Document");

            }
            return RedirectToAction("login","Account");
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Download(int documentId)
        {
            //check permission.
            Document document = _db.GetDocumentById(documentId);
            string filePath = document.FilePath;
            string fileName = document.FileName;
            
            

            var response = File(System.IO.File.ReadAllBytes(filePath), "application/octet-stream", fileName);
            return response;
        }


        private int GetAuthenticatedUserId()
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("ID")).Value);
            return userId;
        }
    }
}
