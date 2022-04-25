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
        DataContext _db;
        public UserRepository(DataContext db)
        {
            _db = db;
        }

        public bool IsMailExist(string email)
        {
            User user = _db.Users.FirstOrDefault(x => x.Email.Equals(email));
            return user != null;
        }

        public void Create(User user)
        {
            user.Created = DateTime.Now;
            user.RemainingQuota = 1024;
            
            _db.Users.Add(user);
            _db.SaveChanges();
            string path = "wwwroot/File_Storage/User_" + user.Id;
            DirectoryInfo userDirPath = Directory.CreateDirectory(path);
            user.FolderPath = path;
            _db.SaveChanges();



        }

        public void Delete(int id)
        {
            User user = GetUserById(id);
            _db.Users.Remove(user);
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Update(User user)
        {
            User up = GetUserById(user.Id);
            up.Name = user.Name;
            up.Email = user.Email;
            _db.SaveChanges();
        }
    }
}
