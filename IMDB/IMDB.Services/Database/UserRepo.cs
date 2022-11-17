using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.DataLayer;
using IMDB.DataLayer.Model;
using Infrastructure;

namespace IMDB.Services.Database
{
    public class UserRepo : IUser
    {
        private readonly ContextDB _context;
        public UserRepo(ContextDB context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            SaveChanges();
            return user.Id;
        }

        public bool DeleteUser(int id)
        {
            try
            {
                _context.Users.Remove(GetUserById(id));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                _context.Users.Remove(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(n => n.Name == name );
        }

        public bool IsUserExist(int id = 0, string name = "")
        {
            if(_context.Users.Any(n => n.Name.Equals(name) || n.Id == id))
            {
                return true;
            }
            return false;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
