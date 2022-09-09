namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationMemberInformations : CharacterBaseInformations
    {
        public new const short Id = 376;
        public override short TypeId
        {
            get { return Id; }
        }
        public short worldX;
        public short worldY;
        public double mapId;
        public ushort subAreaId;
        public IEnumerable<PartyEntityBaseInformation> companions;

        public PartyInvitationMemberInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex, short worldX, short worldY, double mapId, ushort subAreaId, IEnumerable<PartyEntityBaseInformation> companions)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Sex = sex;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.companions = companions;
        }

        public PartyInvitationMemberInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteDouble(mapId);
            writer.WriteVarUShort(subAreaId);
            writer.WriteShort((short)companions.Count());
            foreach (var objectToSend in companions)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            worldX = reader.ReadShort();
            worldY = reader.ReadShort();
            mapId = reader.ReadDouble();
            subAreaId = reader.ReadVarUShort();
            var companionsCount = reader.ReadUShort();
            var companions_ = new PartyEntityBaseInformation[companionsCount];
            for (var companionsIndex = 0; companionsIndex < companionsCount; companionsIndex++)
            {
                var objectToAdd = new PartyEntityBaseInformation();
                objectToAdd.Deserialize(reader);
                companions_[companionsIndex] = objectToAdd;
            }
            companions = companions_;
        }

    }
}
