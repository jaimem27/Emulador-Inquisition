namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTeamMemberCompanionInformations : FightTeamMemberInformations
    {
        public new const short Id = 451;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte companionId;
        public ushort level;
        public double masterId;

        public FightTeamMemberCompanionInformations(double objectId, sbyte companionId, ushort level, double masterId)
        {
            this.ObjectId = objectId;
            this.companionId = companionId;
            this.level = level;
            this.masterId = masterId;
        }

        public FightTeamMemberCompanionInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(companionId);
            writer.WriteVarUShort(level);
            writer.WriteDouble(masterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            companionId = reader.ReadSByte();
            level = reader.ReadVarUShort();
            masterId = reader.ReadDouble();
        }

    }
}
