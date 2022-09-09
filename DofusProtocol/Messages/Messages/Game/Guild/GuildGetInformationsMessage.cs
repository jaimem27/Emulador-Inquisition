namespace Stump.DofusProtocol.Messages
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GuildGetInformationsMessage : Message
    {
        public const uint Id = 5550;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte infoType;

        public GuildGetInformationsMessage(sbyte infoType)
        {
            this.infoType = infoType;
        }

        public GuildGetInformationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(infoType);
        }

        public override void Deserialize(IDataReader reader)
        {
            infoType = reader.ReadSByte();
        }

    }
}