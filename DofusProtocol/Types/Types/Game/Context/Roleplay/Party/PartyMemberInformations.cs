namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberInformations : CharacterBaseInformations
    {
        public new const short Id = 90;
        public override short TypeId
        {
            get { return Id; }
        }
        public int lifePoints;
        public int maxLifePoints;
        public ushort prospecting;
        public byte regenRate;
        public short initiative;
        public sbyte alignmentSide;
        public short worldX;
        public short worldY;
        public double mapId;
        public ushort subAreaId;
        public PlayerStatus status;
        public IEnumerable<PartyEntityBaseInformation> companions;

        public PartyMemberInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex, int lifePoints, int maxLifePoints, ushort prospecting, byte regenRate, short initiative, sbyte alignmentSide, short worldX, short worldY, double mapId, ushort subAreaId, PlayerStatus status, IEnumerable<PartyEntityBaseInformation> companions)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Sex = sex;
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.prospecting = prospecting;
            this.regenRate = regenRate;
            this.initiative = initiative;
            this.alignmentSide = alignmentSide;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.status = status;
            this.companions = companions;
        }

        public PartyMemberInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(lifePoints);
            writer.WriteVarInt(maxLifePoints);
            writer.WriteVarUShort(prospecting);
            writer.WriteByte(regenRate);
            writer.WriteVarShort(initiative);
            writer.WriteSByte(alignmentSide);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteDouble(mapId);
            writer.WriteVarUShort(subAreaId);
            writer.WriteShort(status.TypeId);
            status.Serialize(writer);
            writer.WriteShort((short)companions.Count());
            foreach (var objectToSend in companions)
            {
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            lifePoints = reader.ReadVarInt();
            maxLifePoints = reader.ReadVarInt();
            prospecting = reader.ReadVarUShort();
            regenRate = reader.ReadByte();
            initiative = reader.ReadVarShort();
            alignmentSide = reader.ReadSByte();
            worldX = reader.ReadShort();
            worldY = reader.ReadShort();
            mapId = reader.ReadDouble();
            subAreaId = reader.ReadVarUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            status.Deserialize(reader);
            var companionsCount = reader.ReadUShort();
            var companions_ = new PartyEntityBaseInformation[companionsCount];
            for (var companionsIndex = 0; companionsIndex < companionsCount; companionsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PartyEntityBaseInformation>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                companions_[companionsIndex] = objectToAdd;
            }
            companions = companions_;
        }

    }
}
