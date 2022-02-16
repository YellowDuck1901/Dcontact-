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
        public  bool  isban { get; set; }
        public string  username { get; set; }

        public User(string id, string email, bool isban, string username)
        {
            this.id = id;
            email = email;
            this.isban = isban;
            this.username = username;
        }

        public User()
        {
        }
    }
}
