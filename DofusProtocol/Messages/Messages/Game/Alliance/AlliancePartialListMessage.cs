namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class AlliancePartialListMessage : AllianceListMessage
    {
        public new const uint Id = 6427;
        public override uint MessageId
        {
            get { return Id; }
        }

        public AlliancePartialListMessage(IEnumerable<AllianceFactSheetInformations> alliances)
        {
            this.alliances = alliances;
        }

        public AlliancePartialListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}