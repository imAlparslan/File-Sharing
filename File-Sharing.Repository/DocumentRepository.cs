﻿using File_Sharing.Data;
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

        public void Download(IDocument document)
        {
            throw new NotImplementedException();
        }

        public Document GetDocumentById(int Id)
        {
            return _db.Documents.FirstOrDefault(d => d.Id == Id);
        }

        public void Upload(IDocument document)
        {
            throw new NotImplementedException();
        }
    }
}