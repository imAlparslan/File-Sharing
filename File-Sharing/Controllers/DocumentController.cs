using File_Sharing.Data;
using File_Sharing.Models;
using File_Sharing.Repository;
using File_Sharing.Services;
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
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(UploadFile doc)
        {
            if(doc !=null)
            {
            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/File_Storage/User_1/" + doc.File.FileName);
            var stream = new FileStream(location, FileMode.Create);
            doc.File.CopyTo(stream);
            
            Document document = new Document();
            document.OwnerId = 1;
            document.FileName = doc.File.FileName;
            document.FilePath = location;
            _db.Upload(document);
            }

            return Ok();
        }


        public IActionResult Download(int documentId)
        {
            //check permission.
            Document document = _db.GetDocumentById(documentId);
            string filePath = document.FilePath;
            string fileName = document.FileName;
            
            

            var response = File(System.IO.File.ReadAllBytes(filePath), "application/octet-stream", fileName);
            return response;
        }
    }
}
