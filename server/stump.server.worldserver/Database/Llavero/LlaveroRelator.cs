using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database
{
    public class LlaveroRelator
    {
        public static string FetchByOwner = "SELECT * FROM characters_llavero WHERE OwnerId={0}";
    }
}
