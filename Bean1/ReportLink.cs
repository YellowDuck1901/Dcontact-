using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean
{
    public class ReportLink
    {
        public string id_row { get; set; }
        public string username { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public bool status { get; set; }

        public ReportLink() { }
        public ReportLink(string username, string link, string description, string email, bool status)
        {
            this.username = username;
            this.link = link;
            this.description = description;
            this.email = email;
            this.status = status;
        }

        public ReportLink(string id_row, string username, string link, string description, string email, bool status)
        {
            this.id_row = id_row;
            this.username = username;
            this.link = link;
            this.description = description;
            this.email = email;
            this.status = status;
        }
    }
}
