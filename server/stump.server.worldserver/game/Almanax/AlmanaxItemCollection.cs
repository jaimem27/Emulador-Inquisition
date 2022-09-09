using Database.Dopeul;
using Stump.Server.BaseServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Database.Almanax;
using Database.Almanax;

namespace Stump.Server.WorldServer.Game.Almanax
{
    public class AlmanaxItemCollection : DataManager<AlmanaxItemCollection>
    {
        public List<AlmanaxItemRecord> Almanax { get; set; }


        public void Load()
        {
            Almanax = Find();
        }

        public List<AlmanaxItemRecord> Find()
        {
            return AlmanaxItemManager.Instance.GetRecords();
        }

    }
}
