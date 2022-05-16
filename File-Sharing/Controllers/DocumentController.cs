using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Repository;
using File_Sharing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace File_Sharing.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocument _db;

        public DocumentController(IDocument db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            int senderId = GetAuthenticatedUserId();

            List<Document> myDocuments = _db.GetMyDocuments(senderId);
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
                

            return View(myFiles);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            if (User != null && User.Identity.IsAuthenticated)
            { 
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
            var location = Path.Combine(Directory.GetCurrentDirectory(), fileOwner.FolderPath + doc.File.FileName);
            var stream = new FileStream(location, FileMode.Create);
            doc.File.CopyTo(stream);
            Document document = new Document();
            document.OwnerId = fileOwner.Id;
            document.FileName = doc.File.FileName;
            document.FilePath = location;
            document.FileSize = Math.Round(stream.Length/1024.00/1024.00,2);
            

             
            _db.Upload(document);
            
            }

            return RedirectToAction("Index","Home");

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
            Document document = _db.GetDocumentById(1);
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
