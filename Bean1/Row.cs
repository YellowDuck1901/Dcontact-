using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bean
{
    public class Row
    {
        public string ID;
        public string text;
        public string font;
        public string link;
        public string bullet;
        public string color;
        public string click;
        public string code;
        public string birth;

        public Row(string id, string text, string font, string link, string bullet, string color, string click, string code, string birth)
        {
            this.ID = id;
            this.text = text;
            this.font = font;
            this.link = link;
            this.bullet = bullet;
            this.color = color;
            this.click = click;
            this.code = code;
            this.birth = birth;
        }

        public Row(string id)
        {
            this.ID = id;
            this.text = "Text";
            this.font = "Cursive";
            this.link = "";
            this.bullet = "fa fa-cube";
            this.color = "#273c75";
            this.click = "0";
            this.code = "0";
            this.birth = "";
        }

        public Row()
        {
        }
    }


}
