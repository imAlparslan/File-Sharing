﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Services
{
    public interface IFriendship
    {
        public void CreateRequest(int senderId, int reciverId);
        public void AcceptRequest(int FriendshipdId);
        public void AcceptRequest(int FriendshipdId);
        

    }
}
