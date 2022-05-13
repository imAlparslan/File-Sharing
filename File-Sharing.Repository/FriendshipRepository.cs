using File_Sharing.Data;
using File_Sharing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Repository
{
    public class FriendshipRepository : IFriendship
    {
        DataContext _db;

        public FriendshipRepository(DataContext db)
        {
            _db = db;
        }

        public void AcceptRequest(int FriendshipdId)
        {
            Friendship friendship = GetFriendshipById(FriendshipdId);
            friendship.Status = Status.Friend;
            _db.Update(friendship);
            _db.SaveChanges();
        }

        public void CreateRequest(int senderId, string reciverEmail)
        {
            Friendship p = new Friendship() {
                SenderId = senderId,
                ReciverId = _db.Users.FirstOrDefault(x => x.Email.Equals(reciverEmail)).Id,
                Status = Status.Request
            };
           _db.Friendships.Add(p);
           _db.SaveChanges();
            
        }

        public List<Friendship> GetFriends(int userId)
        {
            List<Friendship> friends = _db.Friendships.Where(x => x.Status == Status.Friend && 
            (x.ReciverId == userId || x.SenderId == userId)).ToList();
            return friends;
        }

        public Friendship GetFriendshipById(int friendshipId)
        {
            return _db.Friendships.FirstOrDefault(x => x.Id == friendshipId);
        }

        public List<Friendship> GetReceivedRequests(int userId)
        {
            List<Friendship> receivedRequests = _db.Friendships.Where(x => x.ReciverId == userId && x.Status == Status.Request).ToList();
            return receivedRequests;
        }

        public string GetUserNameById(int senderId)
        {
            return _db.Users.FirstOrDefault(x => x.Id == senderId).Name;
        }

        public string GetUserMailById(int userId)
        {
            return _db.Users.FirstOrDefault(x => x.Id == userId).Email;
        }

        public void RejectRequest(int FriendshipdId)
        {
            Friendship friendship = GetFriendshipById(FriendshipdId);
            _db.Friendships.Remove(friendship);
            _db.SaveChanges();

        }

        public void Delete(int friendshipId)
        {
            Friendship friendship = _db.Friendships.FirstOrDefault(x => x.Id == friendshipId);
            _db.Friendships.Remove(friendship);
            _db.SaveChanges();
        }
    }
}
