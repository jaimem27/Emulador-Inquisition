using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database.Items
{

    public class GuildBankItemRelator
    {
        public static string FetchQuery = "SELECT * FROM guilds_items_bank";

        /// <summary>
        /// Use string.Format
        /// </summary>
        public static string FetchByOwner = "SELECT * FROM guilds_items_bank WHERE OwnerGuildId={0}";
    }

    [TableName("guilds_items_bank")]
    public class GuildBankItemRecord : ItemRecord<GuildBankItemRecord>, IAutoGeneratedRecord
    {
        public GuildBankItemRecord()
        {

        }

        private int m_ownerGuildId;

        [Index]
        public int OwnerGuildId
        {
            get { return m_ownerGuildId; }
            set
            {
                m_ownerGuildId = value;
                IsDirty = true;
            }
        }

        [PrimaryKey("Id", false)]
        public override int Id
        {
            get;
            set;
        }
    }
}