using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class UUID
    {
        public static string getUUID()
        {
            Guid id = Guid.NewGuid();
            return id.ToString().Replace("-", "");
        }
    }
}
