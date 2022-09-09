namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GuildListMessage : Message
    {
        public const uint Id = 6413;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IEnumerable<GuildInformations> guilds;

        public GuildListMessage(IEnumerable<GuildInformations> guilds)
        {
            this.guilds = guilds;
        }

        public GuildListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)guilds.Count());
            foreach (var objectToSend in guilds)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var guildsCount = reader.ReadUShort();
            var guilds_ = new GuildInformations[guildsCount];
            for (var guildsIndex = 0; guildsIndex < guildsCount; guildsIndex++)
            {
                var objectToAdd = new GuildInformations();
                objectToAdd.Deserialize(reader);
                guilds_[guildsIndex] = objectToAdd;
            }
            guilds = guilds_;
        }

    }
}