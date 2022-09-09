using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database
{
    public class AlmanaxRelator
    {
        public static string FetchByOwner = "SELECT * FROM characters_almanax WHERE OwnerId={0}";
    }
}
