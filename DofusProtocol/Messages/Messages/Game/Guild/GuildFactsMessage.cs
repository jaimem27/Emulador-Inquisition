

// Generated on 04/19/2020 03:44:57
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GuildFactsMessage : Message
    {
        public const uint Id = 6415;
        public override uint MessageId
        {
            get { return Id; }
        }

        public Types.GuildFactSheetInformations infos;
        public int creationDate;
        public ushort nbTaxCollectors;
        public IEnumerable<Types.CharacterMinimalGuildPublicInformations> members;

        public GuildFactsMessage()
        {
        }

        public GuildFactsMessage(Types.GuildFactSheetInformations infos, int creationDate, ushort nbTaxCollectors, IEnumerable<Types.CharacterMinimalGuildPublicInformations> members)
        {
            this.infos = infos;
            this.creationDate = creationDate;
            this.nbTaxCollectors = nbTaxCollectors;
            this.members = members;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(infos.TypeId);
            infos.Serialize(writer);
            writer.WriteInt(creationDate);
            writer.WriteVarUShort(nbTaxCollectors);
            writer.WriteShort((short)members.Count());
            foreach (var objectToSend in members)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            infos = Types.ProtocolTypeManager.GetInstance<Types.GuildFactSheetInformations>(reader.ReadShort());
            infos.Deserialize(reader);
            creationDate = reader.ReadInt();
            nbTaxCollectors = reader.ReadVarUShort();
            var membersCount = reader.ReadUShort();
            var members_ = new Types.CharacterMinimalGuildPublicInformations[membersCount];
            for (var membersIndex = 0; membersIndex < membersCount; membersIndex++)
            {
                var objectToAdd = new Types.CharacterMinimalGuildPublicInformations();
                objectToAdd.Deserialize(reader);
                members_[membersIndex] = objectToAdd;
            }
            members = members_;
        }

    }

}