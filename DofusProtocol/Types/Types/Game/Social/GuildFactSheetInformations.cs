namespace Stump.DofusProtocol.Types
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GuildFactSheetInformations : GuildInformations
    {
        public new const short Id = 424;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong leaderId;
        public ushort nbMembers;

        public GuildFactSheetInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem, ulong leaderId, ushort nbMembers)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.guildEmblem = guildEmblem;
            this.leaderId = leaderId;
            this.nbMembers = nbMembers;
        }

        public GuildFactSheetInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(leaderId);
            writer.WriteVarUShort(nbMembers);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            leaderId = reader.ReadVarULong();
            nbMembers = reader.ReadVarUShort();
        }

    }
}