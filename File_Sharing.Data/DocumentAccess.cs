using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Data
{
    public class DocumentAccess
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int OwnerId { get; set; }
        public int AccessorId { get; set; }
        public DateTime GivenTime { get; set; }


    }
}
