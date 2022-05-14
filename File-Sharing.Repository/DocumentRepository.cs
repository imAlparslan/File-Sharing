using File_Sharing.Data;
using File_Sharing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Repository
{
    public class DocumentRepository : IDocument
    {
        DataContext _db;
        public DocumentRepository(DataContext db)
        {
            _db = db;
        }

        public void Download(Document document)
        {
            throw new NotImplementedException();
        }

        public Document GetDocumentById(int Id)
        {
            return _db.Documents.FirstOrDefault(d => d.Id == Id);
        }

        public List<Document> GetMyDocuments(int userId)
        {
            List<Document> documents = _db.Documents.Where(x => x.OwnerId == userId).ToList();

            return documents;
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Upload(Document document)
        {

            document.UploadDate = DateTime.Now;
            _db.Documents.Add(document);
             User fileOwner = GetUserById(document.OwnerId);
            fileOwner.RemainingQuota -= document.FileSize;
            _db.Update(fileOwner);
            _db.SaveChanges();
            
        }
    }
}
