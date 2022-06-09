using File_Sharing.Data;
using File_Sharing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Repository
{
    public class DocumentAccessRepository : IDocumentAccess
    {
        DataContext _db;

        public DocumentAccessRepository(DataContext db)
        {
            _db = db;
        }

        public void Create(int userId, int documentId, int friendshipId)
        {
            DocumentAccess documentAccess = new DocumentAccess()
            {
                OwnerId = userId,
                DocumentId = documentId,
                AccessorId = GetFriendId(userId, friendshipId),
                Friendship = _db.Friendships.FirstOrDefault(x => x.Id == friendshipId),
                GivenTime = DateTime.Now,
                
           };

            _db.Add(documentAccess);
            _db.SaveChanges();
        }

        public void Delete(int accessId)
        {
            DocumentAccess access = _db.DocumentAccesses.FirstOrDefault(x=> x.Id == accessId);
            _db.DocumentAccesses.Remove( access );
            _db.SaveChanges(true);
        }

        public List<DocumentAccess> GetAccessorsList(int ownerId, int fileId)
        {
            List<DocumentAccess> accessors = _db.DocumentAccesses.Where(x=> x.OwnerId == ownerId && x.DocumentId == fileId).ToList();
            return accessors;
        }

        public List<Friendship> GetFriendsList(int userId)
        {
            List<Friendship> friends = _db.Friendships.Where(x => x.Status == Status.Friend &&
            (x.ReciverId == userId || x.SenderId == userId) ).ToList();
            //&& !_db.DocumentAccesses.Any(y => (y.AccessorId == x.SenderId) || y.AccessorId == x.ReciverId)
            return friends;
        }

        public int GetFileId(int fileAccessId)
        {
            return _db.DocumentAccesses.FirstOrDefault(x => x.Id == fileAccessId).DocumentId;
        }

        public string GetUserNameById(int userId)
        {
            return _db.Users.FirstOrDefault(x => x.Id == userId).Name;
        }

        public int GetFriendId(int userId, int friendshipId)
        {
            Friendship friendship = _db.Friendships.FirstOrDefault(x=>x.Id == friendshipId);
            return friendship.SenderId == userId ? friendship.ReciverId : friendship.SenderId;
        }

        public string GetFriendName(Friendship friendship)
        {


            throw new NotImplementedException();
        }

        public List<DocumentAccess> GetMySharings(int userId)
        {
            return _db.DocumentAccesses.Where(x => x.OwnerId == userId).ToList();
        }

        public Document FindDocumentByAccessId(int accessId)
        {
            DocumentAccess access = _db.DocumentAccesses.FirstOrDefault(x => x.Id == accessId);
            return _db.Documents.FirstOrDefault(x => x.Id == access.DocumentId);
                
        }

        public List<DocumentAccess> GetSharingWithMe(int userId)
        {
            return _db.DocumentAccesses.Where(x => x.AccessorId == userId).ToList();
        }

        public string GetUserEmailWithId(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id).Email;
           
        }
    }
}
