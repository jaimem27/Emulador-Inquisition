namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartedTaxCollectorShopMessage : Message
    {
        public List<ObjectItem> objects = new List<ObjectItem>();
        public const uint Id = 6664;
        public double kamas;

        public override uint MessageId
        {
            get
            {
                return 6664;
            }
        }

        public ExchangeStartedTaxCollectorShopMessage()
        {
        }

        public ExchangeStartedTaxCollectorShopMessage(List<ObjectItem> objects, double kamas)
        {
            this.objects = objects;
            this.kamas = kamas;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)this.objects.Count);
            for (int index = 0; index < this.objects.Count; ++index)
                this.objects[index].Serialize(writer);
            if (this.kamas < 0.0 || this.kamas > 9.00719925474099E+15)
                throw new Exception("Forbidden value (" + (object)this.kamas + ") on element kamas.");
            writer.WriteVarLong((long)this.kamas);
        }

        public override void Deserialize(IDataReader reader)
        {
            uint num = (uint)reader.ReadUShort();
            for (int index = 0; (long)index < (long)num; ++index)
            {
                ObjectItem objectItem = new ObjectItem();
                objectItem.Deserialize(reader);
                this.objects.Add(objectItem);
            }
            this.kamas = (double)reader.ReadVarULong();
            if (this.kamas < 0.0 || this.kamas > 9.00719925474099E+15)
                throw new Exception("Forbidden value (" + (object)this.kamas + ") on element of ExchangeStartedTaxCollectorShopMessage.kamas.");
        }
    }
}