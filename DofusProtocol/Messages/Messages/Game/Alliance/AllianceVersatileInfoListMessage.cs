namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class AllianceVersatileInfoListMessage : Message
    {
        public const uint Id = 6436;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IEnumerable<AllianceVersatileInformations> alliances;

        public AllianceVersatileInfoListMessage(IEnumerable<AllianceVersatileInformations> alliances)
        {
            this.alliances = alliances;
        }

        public AllianceVersatileInfoListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)alliances.Count());
            foreach (var objectToSend in alliances)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var alliancesCount = reader.ReadUShort();
            var alliances_ = new AllianceVersatileInformations[alliancesCount];
            for (var alliancesIndex = 0; alliancesIndex < alliancesCount; alliancesIndex++)
            {
                var objectToAdd = new AllianceVersatileInformations();
                objectToAdd.Deserialize(reader);
                alliances_[alliancesIndex] = objectToAdd;
            }
            alliances = alliances_;
        }

    }
}