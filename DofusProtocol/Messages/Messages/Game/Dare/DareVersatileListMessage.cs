namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DareVersatileListMessage : Message
    {
        public const uint Id = 6657;
        public override uint MessageId
        {
            get { return Id; }
        }
        public DareVersatileInformations[] Dares { get; set; }

        public DareVersatileListMessage(DareVersatileInformations[] dares)
        {
            this.Dares = dares;
        }

        public DareVersatileListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Dares.Count());
            for (var daresIndex = 0; daresIndex < Dares.Count(); daresIndex++)
            {
                var objectToSend = Dares[daresIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var daresCount = reader.ReadUShort();
            Dares = new DareVersatileInformations[daresCount];
            for (var daresIndex = 0; daresIndex < daresCount; daresIndex++)
            {
                var objectToAdd = new DareVersatileInformations();
                objectToAdd.Deserialize(reader);
                Dares[daresIndex] = objectToAdd;
            }
        }

    }
}
