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

        public void CreateRequest(int senderId, int reciverId)
        {
            Friendship p = new Friendship {
                SenderId = senderId,
                ReciverId = reciverId,
                Status = Status.Request
            };
           _db.Friendships.Add(p);
           _db.SaveChanges();
            
        }

        public Friendship GetFriendshipById(int friendshipId)
        {
            return _db.Friendships.FirstOrDefault(x => x.Id == friendshipId);
        }


    }
}
