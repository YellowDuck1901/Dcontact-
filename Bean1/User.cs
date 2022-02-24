using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean
{
    public class User
    {
        public string username { get; set; } 
        public string id { get; set; }
        public string email { get; set; }
        public  bool  isban { get; set; }

        public User()
        {
        }

        public User( string id, string email, bool isban, string username)
        {
            this.id = id;
            this.email = email;
            this.username = username;
            this.isban = isban;
        }
    }
}
