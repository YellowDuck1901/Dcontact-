using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bean
{
    public class Row
    {
        public string text;
        public string font;
        public string link;
        public string bullet;
        public string click;
        public string code;
        public string birth;

        public Row(string text, string font, string link, string bullet, string click, string code, string birth)
        {
            this.text = text;
            this.font = font;
            this.link = link;
            this.bullet = bullet;
            this.click = click;
            this.code = code;
            this.birth = birth;
        }

        public Row()
        {
        }
    }


}
