

// Generated on 02/17/2017 01:58:24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class InventoryPresetSaveCustomMessage : Message
    {
        public const uint Id = 6329;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte presetId;
        public sbyte symbolId;
        public IEnumerable<sbyte> itemsPositions;
        public IEnumerable<int> itemsUids;
        
        public InventoryPresetSaveCustomMessage()
        {
        }
        
        public InventoryPresetSaveCustomMessage(sbyte presetId, sbyte symbolId, IEnumerable<sbyte> itemsPositions, IEnumerable<int> itemsUids)
        {
            this.presetId = presetId;
            this.symbolId = symbolId;
            this.itemsPositions = itemsPositions;
            this.itemsUids = itemsUids;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(presetId);
            writer.WriteSByte(symbolId);
            var itemsPositions_before = writer.Position;
            var itemsPositions_count = 0;
            writer.WriteShort(0);
            foreach (var entry in itemsPositions)
            {
                 writer.WriteSByte(entry);
                 itemsPositions_count++;
            }
            var itemsPositions_after = writer.Position;
            writer.Seek((int)itemsPositions_before);
            writer.WriteShort((short)itemsPositions_count);
            writer.Seek((int)itemsPositions_after);

            var itemsUids_before = writer.Position;
            var itemsUids_count = 0;
            writer.WriteShort(0);
            foreach (var entry in itemsUids)
            {
                 writer.WriteVarInt(entry);
                 itemsUids_count++;
            }
            var itemsUids_after = writer.Position;
            writer.Seek((int)itemsUids_before);
            writer.WriteShort((short)itemsUids_count);
            writer.Seek((int)itemsUids_after);

        }
        
        public override void Deserialize(IDataReader reader)
        {
            presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            symbolId = reader.ReadSByte();
            if (symbolId < 0)
                throw new Exception("Forbidden value on symbolId = " + symbolId + ", it doesn't respect the following condition : symbolId < 0");
            var limit = reader.ReadShort();
            var itemsPositions_ = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 itemsPositions_[i] = reader.ReadSByte();
            }
            itemsPositions = itemsPositions_;
            limit = reader.ReadShort();
            var itemsUids_ = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 itemsUids_[i] = reader.ReadVarInt();
                 if (itemsUids_[i] < 0)
                     throw new Exception("Forbidden value on itemsUids_[i] = " + itemsUids_[i] + ", it doesn't respect the following condition : itemsUids_[i] < 0");
            }
            itemsUids = itemsUids_;
        }
        
    }
    
}