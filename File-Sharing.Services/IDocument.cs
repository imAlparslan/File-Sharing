using File_Sharing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Services
{
    public interface IDocument
    {
        void Upload(Document document);
        void Download(Document document);
        List<Document> GetMyDocuments(int userId);
        Document GetDocumentById(int Id);
        List<Document>
        public User GetUserById(int id);


    }
}
