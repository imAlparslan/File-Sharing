using File_Sharing.Data;
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

        public IActionResult Download(int documentId)
        {
            //check permission.
            Document document = _db.GetDocumentById(documentId);
            string filePath = document.FilePath;
            string fileName = document.FileName;
            
            
            //var response = File(System.IO.File.rea

            var response = File(System.IO.File.ReadAllBytes(filePath), "application/octet-stream", fileName);
            return response;
        }
    }
}
