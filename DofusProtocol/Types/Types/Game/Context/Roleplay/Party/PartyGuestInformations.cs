namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyGuestInformations
    {
        public const short Id = 374;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ulong guestId;
        public ulong hostId;
        public string name;
        public EntityLook guestLook;
        public sbyte breed;
        public bool sex;
        public PlayerStatus status;
        public IEnumerable<PartyEntityBaseInformation> companions;

        public PartyGuestInformations(ulong guestId, ulong hostId, string name, EntityLook guestLook, sbyte breed, bool sex, PlayerStatus status, IEnumerable<PartyEntityBaseInformation> companions)
        {
            this.guestId = guestId;
            this.hostId = hostId;
            this.name = name;
            this.guestLook = guestLook;
            this.breed = breed;
            this.sex = sex;
            this.status = status;
            this.companions = companions;
        }

        public PartyGuestInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(guestId);
            writer.WriteVarULong(hostId);
            writer.WriteUTF(name);
            guestLook.Serialize(writer);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            writer.WriteShort(status.TypeId);
            status.Serialize(writer);
            writer.WriteShort((short)companions.Count());
            foreach (var objectToSend in companions)
            {
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            guestId = reader.ReadVarULong();
            hostId = reader.ReadVarULong();
            name = reader.ReadUTF();
            guestLook = new EntityLook();
            guestLook.Deserialize(reader);
            breed = reader.ReadSByte();
            sex = reader.ReadBoolean();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            status.Deserialize(reader);
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
