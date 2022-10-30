using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.DataLayer.Model;

namespace Infrastructure
{
    public interface IUser
    {
        public int AddUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(int id);
        public bool DeleteUser(User user);
        public User GetUserById(int id);
        public User GetUserByName(string name);
        public bool IsUserExist(int id = 0, string name = "");
        public void SaveChanges();
    }
}
