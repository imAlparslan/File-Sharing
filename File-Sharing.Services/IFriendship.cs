using File_Sharing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Services
{
    public interface IFriendship
    {
        public void CreateRequest(int senderId, string reciverEmail);
        public void AcceptRequest(int FriendshipdId);
        public void RejectRequest(int FriendshipdId);
        public List<Friendship> GetFriends(int userId);
        public string GetUserNameById(int senderId);
        public string GetUserMailById(int userId);
        public void Delete(int friendshipId);
        public List<Friendship> GetReceivedRequests(int userId);
        public User GetUserByEmail(string email);


    }
}
