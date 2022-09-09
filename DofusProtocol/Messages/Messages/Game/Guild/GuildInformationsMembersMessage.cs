namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GuildInformationsMembersMessage : Message
    {
        public const uint Id = 5558;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IEnumerable<GuildMember> members;

        public GuildInformationsMembersMessage(IEnumerable<GuildMember> members)
        {
            this.members = members;
        }

        public GuildInformationsMembersMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)members.Count());
            foreach (var objectToSend in members)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var membersCount = reader.ReadUShort();
            var members_ = new GuildMember[membersCount];
            for (var membersIndex = 0; membersIndex < membersCount; membersIndex++)
            {
                var objectToAdd = new GuildMember();
                objectToAdd.Deserialize(reader);
                members_[membersIndex] = objectToAdd;
            }
            members = members_;
        }

    }
}