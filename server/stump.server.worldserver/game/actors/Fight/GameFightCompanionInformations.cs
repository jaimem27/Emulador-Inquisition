namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightCompanionInformations : GameFightFighterInformations
    {
        public new const short Id = 450;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte companionGenericId;
        public ushort level;
        public double masterId;

        public GameFightCompanionInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, sbyte companionGenericId, ushort level, double masterId)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.TeamId = teamId;
            this.Wave = wave;
            this.Alive = alive;
            this.Stats = stats;
            this.PreviousPositions = previousPositions;
            this.companionGenericId = companionGenericId;
            this.level = level;
            this.masterId = masterId;
        }

        public GameFightCompanionInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(companionGenericId);
            writer.WriteVarUShort(level);
            writer.WriteDouble(masterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            companionGenericId = reader.ReadSByte();
            level = reader.ReadVarUShort();
            masterId = reader.ReadDouble();
        }

    }
}
