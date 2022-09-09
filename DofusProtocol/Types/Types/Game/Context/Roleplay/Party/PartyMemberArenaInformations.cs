namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberArenaInformations : PartyMemberInformations
    {
        public new const short Id = 391;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort rank;

        public PartyMemberArenaInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex, int lifePoints, int maxLifePoints, ushort prospecting, byte regenRate, short initiative, sbyte alignmentSide, short worldX, short worldY, double mapId, ushort subAreaId, PlayerStatus status, IEnumerable<PartyEntityBaseInformation> companions, ushort rank)
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
            this.rank = rank;
        }

        public PartyMemberArenaInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(rank);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            rank = reader.ReadVarUShort();
        }

    }
}
