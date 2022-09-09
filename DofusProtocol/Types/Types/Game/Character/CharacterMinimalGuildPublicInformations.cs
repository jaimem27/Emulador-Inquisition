using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stump.Core.IO;
namespace Stump.DofusProtocol.Types
{
    public class CharacterMinimalGuildPublicInformations : CharacterMinimalInformations
    {
        public new const short Id = 556;
        public uint rank;
        public override short TypeId
        {
            get { return Id; }
        }
        public CharacterMinimalGuildPublicInformations() { }
        public CharacterMinimalGuildPublicInformations(ulong id, string name, ushort level, uint rank) : base(id, name, level)
        {
            this.rank = rank;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt((int)this.rank);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            rank = reader.ReadUInt();
        }
    }
}