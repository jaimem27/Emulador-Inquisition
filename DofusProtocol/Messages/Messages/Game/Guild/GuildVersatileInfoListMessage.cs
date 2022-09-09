namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GuildVersatileInfoListMessage : Message
    {
        public const uint Id = 6435;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IEnumerable<GuildVersatileInformations> guilds;

        public GuildVersatileInfoListMessage(IEnumerable<GuildVersatileInformations> guilds)
        {
            this.guilds = guilds;
        }

        public GuildVersatileInfoListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)guilds.Count());
            foreach (var objectToSend in guilds)
            {
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var guildsCount = reader.ReadUShort();
            var guilds_ = new GuildVersatileInformations[guildsCount];
            for (var guildsIndex = 0; guildsIndex < guildsCount; guildsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GuildVersatileInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                guilds_[guildsIndex] = objectToAdd;
            }
            guilds = guilds_;
        }

    }
}