

// Generated on 04/19/2020 03:44:57
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GuildFactsRequestMessage : Message
    {
        public const uint Id = 6404;
        public override uint MessageId
        {
            get { return Id; }
        }

        public uint guildId;

        public GuildFactsRequestMessage()
        {
        }

        public GuildFactsRequestMessage(uint guildId)
        {
            this.guildId = guildId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(guildId);
        }

        public override void Deserialize(IDataReader reader)
        {
            guildId = reader.ReadVarUInt();
        }

    }

}