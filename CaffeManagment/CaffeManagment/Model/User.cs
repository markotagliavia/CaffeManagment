using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeManagment.Model
{
    public enum Role : int
    {
        Admin = 1,
        Employee = 2
    }

    public class User
    {
        private string username;
        private string name;
        private string surname;
        private string hashPassword;
        private Role role;
        private DateTime registerDateTime;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public string HashPassword
        {
            get { return hashPassword; }
            set { hashPassword = value; }
        }

        public Role Role
        {
            get { return role; }
            set { role = value; }
        }
        public DateTime RegisterDateTime
        {
            get { return registerDateTime; }
            set { registerDateTime = value; }
        }
    }
}
