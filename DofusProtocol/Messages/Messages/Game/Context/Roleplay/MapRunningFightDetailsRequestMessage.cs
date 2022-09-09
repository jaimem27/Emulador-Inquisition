namespace Stump.DofusProtocol.Messages
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class MapRunningFightDetailsRequestMessage : Message
    {
        public const uint Id = 5750;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort fightId;

        public MapRunningFightDetailsRequestMessage(ushort fightId)
        {
            this.fightId = fightId;
        }

        public MapRunningFightDetailsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(fightId);
        }

        public override void Deserialize(IDataReader reader)
        {
            fightId = reader.ReadVarUShort();
        }

    }
}