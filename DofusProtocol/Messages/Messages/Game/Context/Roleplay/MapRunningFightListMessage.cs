

// Generated on 04/19/2020 03:44:47
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class MapRunningFightListMessage : Message
    {
        public const uint Id = 5743;
        public override uint MessageId
        {
            get { return Id; }
        }

        public IEnumerable<Types.FightExternalInformations> fights;

        public MapRunningFightListMessage()
        {
        }

        public MapRunningFightListMessage(IEnumerable<Types.FightExternalInformations> fights)
        {
            this.fights = fights;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)fights.Count());
            foreach (var objectToSend in fights)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var fightsCount = reader.ReadUShort();
            var fights_ = new Types.FightExternalInformations[fightsCount];
            for (var fightsIndex = 0; fightsIndex < fightsCount; fightsIndex++)
            {
                var objectToAdd = new Types.FightExternalInformations();
                objectToAdd.Deserialize(reader);
                fights_[fightsIndex] = objectToAdd;
            }
            fights = fights_;
        }

    }

}