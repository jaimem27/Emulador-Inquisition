using Stump.Server.BaseServer.Database;

using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Llavero;

namespace Stump.Server.WorldServer.Game.Llavero
{
    public class LlaveroManager : DataManager<LlaveroManager>
    {
        private List<LlaveroRecord> _records = new List<LlaveroRecord>();

        [Initialization(InitializationPass.Seventh)]
        public override void Initialize()
        {
            _records = Database.Fetch<LlaveroRecord>("select * from characters_llavero");
        }

        public List<LlaveroRecord> GetCharacterRecords(int characterId)
        {
            return _records.Where(x => x.CharacterId == characterId).ToList();
        }

        public void AddRecord(LlaveroRecord record)
        {
            _records.Add(record);
        }

        public void DeleteRecord(LlaveroRecord record)
        {
            _records.Remove(record);
        }
    }
}