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
        void Upload(IDocument document);
        void Download(IDocument document);
        Document GetDocumentById(int Id);


    }
}
