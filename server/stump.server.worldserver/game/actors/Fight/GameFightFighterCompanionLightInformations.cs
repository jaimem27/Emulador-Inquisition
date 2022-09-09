namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightFighterCompanionLightInformations : GameFightFighterLightInformations
    {
        public new const short Id = 454;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte companionId;
        public double masterId;

        public GameFightFighterCompanionLightInformations(bool sex, bool alive, double objectId, sbyte wave, ushort level, sbyte breed, sbyte companionId, double masterId)
        {
            this.Sex = sex;
            this.Alive = alive;
            this.ObjectId = objectId;
            this.Wave = wave;
            this.Level = level;
            this.Breed = breed;
            this.companionId = companionId;
            this.masterId = masterId;
        }

        public GameFightFighterCompanionLightInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(companionId);
            writer.WriteDouble(masterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            companionId = reader.ReadSByte();
            masterId = reader.ReadDouble();
        }

    }
}
