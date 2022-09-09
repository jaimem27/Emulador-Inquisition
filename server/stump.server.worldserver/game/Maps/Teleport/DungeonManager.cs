using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Dungeon;
using Stump.Server.WorldServer.Database.Dungeons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Game.Maps.Teleport
{
    public class DungeonManager : DataManager<DungeonManager>
    {
        private Dictionary<int, DungeonRecord> m_dungeons = new Dictionary<int, DungeonRecord>();

        [Initialization(InitializationPass.Last)]
        public override void Initialize()
        {
            m_dungeons = Database.Query<DungeonRecord>(DungeonRelator.FetchQuery).ToDictionary(x => x.Id);
        }

        public DungeonRecord GetDungeonById(int dungeonId)
        {
            return m_dungeons.Values.FirstOrDefault(x => x.Id == dungeonId);
        }

        public DungeonRecord GetDungeonByMapsIds(int mapId)
        {
            return m_dungeons.Values.Where(x => x.MapsIdsCSV.Contains(mapId.ToString())).FirstOrDefault();
        }
    }
}
