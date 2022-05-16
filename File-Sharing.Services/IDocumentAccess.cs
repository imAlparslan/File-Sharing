using File_Sharing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Services
{
    public interface IDocumentAccess
    {
        public void Create(int userId, int documentId, int friendshipId);
        public void Delete(int accessId);
        public int GetFileId(int fileAccessId);
        public List<DocumentAccess> GetAccessorsList(int ownerId, int fileId);
        public List<Friendship> GetFriendsList(int userId);
        public string GetUserNameById(int userId);
        public string GetFriendName(Friendship friendship);
        public int GetFriendId(int userId, int friendshipId);




    }
}
