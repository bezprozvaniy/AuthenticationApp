using System.Collections.Generic;

namespace AuthenticationApp.Model
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }

        public List<Software> Software { get; set; }

        public User()
        {

        }

        public User(string userName) : this()
        {
            this.UserName = userName;
        }

        public User(string userName, string[] software) : this(userName)
        {
            this.Software = new List<Software>();

            foreach (var s in software)
            {
                this.Software.Add(new Software(s));
            }
        }

        public User(string userName, string lastName, string[] software) : this(userName, software)
        {
            this.LastName = lastName;
        }

        public User(string userName, string lastName, string email, string[] software) : this(userName, lastName, software)
        {
            this.Email = email;
        }
    }
}
