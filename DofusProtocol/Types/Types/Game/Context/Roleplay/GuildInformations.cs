namespace Stump.DofusProtocol.Types
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GuildInformations : BasicGuildInformations
    {
        public new const short Id = 127;
        public override short TypeId
        {
            get { return Id; }
        }
        public GuildEmblem guildEmblem;

        public GuildInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.guildEmblem = guildEmblem;
        }

        public GuildInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guildEmblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildEmblem = new GuildEmblem();
            guildEmblem.Deserialize(reader);
        }

    }
}