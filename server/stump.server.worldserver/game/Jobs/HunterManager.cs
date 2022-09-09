using Stump.Server.BaseServer;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Jobs;
using System.Collections.Generic;
using System.Linq;
//By Allan for Avalone and adapted per Saiki
namespace Stump.Server.WorldServer.Game.Jobs
{
    class HunterManager : DataManager<HunterManager>
    {
        private Dictionary<int, HunterRecord> m_hunterInfos;

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            m_hunterInfos = Database.Query<HunterRecord>(HunterRelator.FetchQuery).ToDictionary(entry => entry.MonsterId);
        }

        public bool DropExist(int MonsterTemplate)
        {
            HunterRecord drop_exist;
            m_hunterInfos.TryGetValue(MonsterTemplate, out drop_exist);

            if (drop_exist != null)
                return true;
            else
                return false;
        }

        public int ItemId(int MonsterTemplate)
        {
            return m_hunterInfos[MonsterTemplate].DropId;
        }

        public int Level(int MonsterTemplate)
        {
            return m_hunterInfos[MonsterTemplate].Level;
        }
    }
}
