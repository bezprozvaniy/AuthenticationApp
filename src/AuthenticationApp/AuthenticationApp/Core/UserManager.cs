using System.Collections.Generic;
using System.Linq;
using AuthenticationApp.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApp.Core
{
    public static class UserManager
    {
        private static UserContext _db;
        private static readonly object Locker = new object();

        private static UserContext GetDb()
        {
            lock (Locker)
            {
                if (_db == null)
                {
                    _db = new UserContext();
                    _db.Users.Load();
                    _db.Software.Load();
                }
            }

            return _db;
        }

        public static IEnumerable<User> GetUsers()
        {
            return GetDb().Users;
        }

        public static bool HasUser(string userName)
        {
            return GetDb().Users.Any(_ => _.UserName == userName);
        }

        public static User GetUser(long id)
        {
            return GetDb().Users.FirstOrDefault(_ => _.Id == id);
        }

        public static void AddUser(User user)
        {
            GetDb().Users.Add(user);
            GetDb().SaveChanges();
        }

        public static void EditUser(long id, User user)
        {
            var currentUser = GetUser(id);
            if (currentUser != null)
            {
                currentUser.UserName = user.UserName;
                currentUser.Email = user.Email;
                currentUser.LastName = user.LastName;

                var userSoftwareToAdd = currentUser.Software.Where(currentUserSoftware => user.Software.All(_ => _.Name != currentUserSoftware.Name)).ToList();
                var userSoftwareToRemove = currentUser.Software.Where(currentUserSoftware => user.Software.All(software => software.Name != currentUserSoftware.Name)).ToList();

                foreach (var software in userSoftwareToAdd)
                {
                    currentUser.Software.Add(software);
                }

                foreach (var software in userSoftwareToRemove)
                {
                    currentUser.Software.Remove(software);
                }

                GetDb().SaveChanges();
            }
        }

        public static void DeleteUser(long id)
        {
            var user = GetUser(id);
            GetDb().Users.Remove(user);
            GetDb().SaveChanges();
        }
    }
}
