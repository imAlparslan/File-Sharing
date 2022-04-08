using File_Sharing.Data;
using File_Sharing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.Repository
{

    public class UserRepository :IUser
    {
        DataContext db;
        public UserRepository(DataContext _db)
        {
            db = _db;
        }

        public void Create(User user)
        {
            user.Created = DateTime.Now;

            string path = "wwwroot/File_Storage/User_" + user.Email;
            DirectoryInfo userDirPath = Directory.CreateDirectory(path);
            user.FolderPath = userDirPath.FullName;
            
            db.Users.Add(user);
            db.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public User GetUserById(int id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}
