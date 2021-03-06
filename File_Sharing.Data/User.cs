using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Data
{

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        
        public double RemainingQuota { get; set; }
        
        public string Password { get; set; }
        
        public string? FolderPath { get; set; }
        
        public DateTime Created { get; set; }
    
    
    }
}
