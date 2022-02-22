using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean
{
    public class Dcontact
    {
        public string numerView;
        public string avt;
        public string background;
        public List<Row> rows;

        public Dcontact(string numerView, string avt, string background, List<Row> rows)
        {
            this.numerView = numerView;
            this.avt = avt;
            this.background = background;
            this.rows = rows;
        }

        public Dcontact()
        {
        }
    }
}
