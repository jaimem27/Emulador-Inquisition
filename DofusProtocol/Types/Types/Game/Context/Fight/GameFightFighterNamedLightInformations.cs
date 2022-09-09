namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightFighterNamedLightInformations : GameFightFighterLightInformations
    {
        public new const short Id = 456;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Name { get; set; }

        public GameFightFighterNamedLightInformations(bool sex, bool alive, double objectId, sbyte wave, ushort level, sbyte breed, string name)
        {
            this.Sex = sex;
            this.Alive = alive;
            this.ObjectId = objectId;
            this.Wave = wave;
            this.Level = level;
            this.Breed = breed;
            this.Name = name;
        }

        public GameFightFighterNamedLightInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
        }

    }
}
