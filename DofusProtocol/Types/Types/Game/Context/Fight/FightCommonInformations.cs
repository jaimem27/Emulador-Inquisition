namespace Stump.DofusProtocol.Types
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class FightCommonInformations
    {
        public const short Id = 43;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort fightId;
        public sbyte fightType;
        public IEnumerable<FightTeamInformations> fightTeams;
        public IEnumerable<ushort> fightTeamsPositions;
        public IEnumerable<FightOptionsInformations> fightTeamsOptions;

        public FightCommonInformations(ushort fightId, sbyte fightType, IEnumerable<FightTeamInformations> fightTeams, IEnumerable<ushort> fightTeamsPositions, IEnumerable<FightOptionsInformations> fightTeamsOptions)
        {
            this.fightId = fightId;
            this.fightType = fightType;
            this.fightTeams = fightTeams;
            this.fightTeamsPositions = fightTeamsPositions;
            this.fightTeamsOptions = fightTeamsOptions;
        }

        public FightCommonInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(fightId);
            writer.WriteSByte(fightType);
            writer.WriteShort((short)fightTeams.Count());
            foreach (var objectToSend in fightTeams)
            {
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)fightTeamsPositions.Count());
            foreach (var objectToSend in fightTeamsPositions)
            {
                writer.WriteVarUShort(objectToSend);
            }
            writer.WriteShort((short)fightTeamsOptions.Count());
            foreach (var objectToSend in fightTeamsOptions)
            {
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            fightId = reader.ReadVarUShort();
            fightType = reader.ReadSByte();
            var fightTeamsCount = reader.ReadUShort();
            var fightTeams_ = new FightTeamInformations[fightTeamsCount];
            for (var fightTeamsIndex = 0; fightTeamsIndex < fightTeamsCount; fightTeamsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<FightTeamInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                fightTeams_[fightTeamsIndex] = objectToAdd;
            }
            fightTeams = fightTeams_;
            var fightTeamsPositionsCount = reader.ReadUShort();
            var fightTeamsPositions_ = new ushort[fightTeamsPositionsCount];
            for (var fightTeamsPositionsIndex = 0; fightTeamsPositionsIndex < fightTeamsPositionsCount; fightTeamsPositionsIndex++)
            {
                fightTeamsPositions_[fightTeamsPositionsIndex] = reader.ReadVarUShort();
            }
            fightTeamsPositions = fightTeamsPositions_;
            var fightTeamsOptionsCount = reader.ReadUShort();
            var fightTeamsOptions_ = new FightOptionsInformations[fightTeamsOptionsCount];
            for (var fightTeamsOptionsIndex = 0; fightTeamsOptionsIndex < fightTeamsOptionsCount; fightTeamsOptionsIndex++)
            {
                var objectToAdd = new FightOptionsInformations();
                objectToAdd.Deserialize(reader);
                fightTeamsOptions_[fightTeamsOptionsIndex] = objectToAdd;
            }
            fightTeamsOptions = fightTeamsOptions_;
        }

    }
}