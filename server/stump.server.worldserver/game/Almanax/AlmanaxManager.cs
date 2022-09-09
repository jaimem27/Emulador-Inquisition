using Stump.Server.BaseServer.Database;

using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Almanax;

namespace Stump.Server.WorldServer.Game.Almanax
{
    public class AlmanaxManager : DataManager<AlmanaxManager>
    {
        private List<AlmanaxRecord> _records = new List<AlmanaxRecord>();

        [Initialization(InitializationPass.Seventh)]
        public override void Initialize()
        {
            _records = Database.Fetch<AlmanaxRecord>("select * from characters_almanax");
        }

        public List<AlmanaxRecord> GetCharacterRecords(int characterId)
        {
            return _records.Where(x => x.CharacterId == characterId).ToList();
        }

        public List<AlmanaxRecord> GetCharacterRecords(string ip)
        {
            return _records.Where(x => x.Ip == ip).ToList();
        }

        public void AddRecord(AlmanaxRecord record)
        {
            _records.Add(record);
        }

        public void DeleteRecord(AlmanaxRecord record)
        {
            _records.Remove(record);
        }
    }
}