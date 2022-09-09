using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Mounts.Capture;
using System.Collections.Generic;
using System.Linq;
//By Saiki for Wolf

namespace Stump.Server.WorldServer.Game.Mounts
{
    class CaptureVulkManager : DataManager<CaptureVulkManager>
    {
        private Dictionary<int, VulkRecord> m_vulk;

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            m_vulk = Database.Query<VulkRecord>(VulkRelator.FetchQuery).ToDictionary(entry => entry.MonsterId);
        }

        public ItemTemplate GetDrop(int MonsterTemplate)
        {
            return m_vulk[MonsterTemplate].ItemTemplate;
        }

        public bool MountExist(int MonsterTemplate)
        {
            VulkRecord vulk_exist;
            m_vulk.TryGetValue(MonsterTemplate, out vulk_exist);

            if (vulk_exist != null)
                return true;
            else
                return false;
        }

        public int ItemId(int MonsterTemplate)
        {
            return m_vulk[MonsterTemplate].DropId;
        }

        public float TauxDrop(int MonsterTemplate)
        {
            return m_vulk[MonsterTemplate].TauxDrop;
        }
    }
}
