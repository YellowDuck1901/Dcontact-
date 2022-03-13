using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean
{
    public class Admin
    {
        public string ID { get; set; }

        public string username { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        
        public string address { get; set; }

        public bool isAdmin { get; set; }

        public Admin(string ID, string username, string fullName, string email, string phoneNumber, string address, bool isAdmin)
        {
            this.ID = ID;
            this.fullName = fullName;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.isAdmin = isAdmin;
        }

        public Admin() { }

    }
}
