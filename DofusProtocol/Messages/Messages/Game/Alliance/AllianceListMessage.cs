namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class AllianceListMessage : Message
    {
        public const uint Id = 6408;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IEnumerable<AllianceFactSheetInformations> alliances;

        public AllianceListMessage(IEnumerable<AllianceFactSheetInformations> alliances)
        {
            this.alliances = alliances;
        }

        public AllianceListMessage() { }

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
            var alliances_ = new AllianceFactSheetInformations[alliancesCount];
            for (var alliancesIndex = 0; alliancesIndex < alliancesCount; alliancesIndex++)
            {
                var objectToAdd = new AllianceFactSheetInformations();
                objectToAdd.Deserialize(reader);
                alliances_[alliancesIndex] = objectToAdd;
            }
            alliances = alliances_;
        }

    }
}