using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Mounts.Capture;
using System.Collections.Generic;
using System.Linq;
//By Allan for Avalone

namespace Stump.Server.WorldServer.Game.Mounts
{
    class CaptureMuldoManager : DataManager<CaptureMuldoManager>
    {
        private Dictionary<int, MuldoRecord> m_muldo;

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            m_muldo = Database.Query<MuldoRecord>(MuldoRelator.FetchQuery).ToDictionary(entry => entry.MonsterId);
        }

        public ItemTemplate GetDrop(int MonsterTemplate)
        {
            return m_muldo[MonsterTemplate].ItemTemplate;
        }

        public bool MountExist(int MonsterTemplate)
        {
            MuldoRecord muldo_exist;
            m_muldo.TryGetValue(MonsterTemplate, out muldo_exist);

            if (muldo_exist != null)
                return true;
            else
                return false;
        }

        public int ItemId(int MonsterTemplate)
        {
            return m_muldo[MonsterTemplate].DropId;
        }

        public float TauxDrop(int MonsterTemplate)
        {
            return m_muldo[MonsterTemplate].TauxDrop;
        }
    }
}
