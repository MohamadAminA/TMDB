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
        public Task<User> GetUserById(int id);
        public bool UpdateUser(User user);
        public bool DeleteUser(int id);
        public int AddUser(User user);
        public bool DeleteUser(User user);
        public User GetUserByName(string name);
        public bool IsUserExist(int id = 0, string name = "");
        public void SaveChanges();
        public Task<List<User>> GetUsersAsync();
    }
}
