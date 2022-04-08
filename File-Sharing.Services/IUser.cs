using File_Sharing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Services
{
    public interface IUser
    {
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public bool IsMailExist(string email);
        public void Create(User user);
        public void Update(User user);
        public void Delete(int id);
        
    }
}
