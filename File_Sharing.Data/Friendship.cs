using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Data
{
    public enum Status{
        Friend,
        Request
    };


    public class Friendship
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public Status Status { get; set; }

    }
}
