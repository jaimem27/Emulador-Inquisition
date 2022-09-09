using Stump.Server.BaseServer.Database;

using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Almanax;
using Database.Almanax;

namespace Stump.Server.WorldServer.Game.Almanax
{
    public class AlmanaxItemManager : DataManager<AlmanaxItemManager>
    {
        private List<AlmanaxItemRecord> _records = new List<AlmanaxItemRecord>();

        [Initialization(InitializationPass.Seventh)]
        public override void Initialize()
        {
            _records = Database.Fetch<AlmanaxItemRecord>("select * from almanax_item");
        }

        public List<AlmanaxItemRecord> GetRecords()
        {
            return _records.ToList();
        }


    }
}