

//// Generated on 02/17/2017 01:58:24
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Stump.Core.IO;
//using Stump.DofusProtocol.Types;

//namespace Stump.DofusProtocol.Messages
//{
//    public class InventoryPresetItemUpdateMessage : Message
//    {
//        public const uint Id = 6168;
//        public override uint MessageId
//        {
//            get { return Id; }
//        }
        
//        public sbyte presetId;
//        public Types.ItemForPreset presetItem;
        
//        public InventoryPresetItemUpdateMessage()
//        {
//        }
        
//        public InventoryPresetItemUpdateMessage(sbyte presetId, Types.ItemForPreset presetItem)
//        {
//            this.presetId = presetId;
//            this.presetItem = presetItem;
//        }
        
//        public override void Serialize(IDataWriter writer)
//        {
//            writer.WriteSByte(presetId);
//            presetItem.Serialize(writer);
//        }
        
//        public override void Deserialize(IDataReader reader)
//        {
//            presetId = reader.ReadSByte();
//            presetItem = new Types.ItemForPreset();
//            presetItem.Deserialize(reader);
//        }
        
//    }
    
//}