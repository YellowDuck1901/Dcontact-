using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean
{
    public class User
    {
        public string id { get; set; }
        public string email { get; set; }
        public bool isban { get; set; }
        public string username { get; set; }

        public bool isAdmin { get; set; }
        public Dcontact dcontact { get; set; }

        public User(string id, string email, bool isban, string username, Dcontact dcontact)
        {
            this.id = id;
            this.email = email;
            this.isban = isban;
            this.username = username;
            this.dcontact = dcontact;
        }

        public User(string id, string email, string username, bool isAdmin)
        {
            this.id = id;
            this.email = email;
            this.username = username;
            this.isAdmin = isAdmin;
        }

        public User()
        {
        }
    }
}
