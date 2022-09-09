namespace Stump.DofusProtocol.Messages
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class GuildInformationsGeneralMessage : Message
    {
        public const uint Id = 5557;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool abandonnedPaddock;
        public byte level;
        public ulong expLevelFloor;
        public ulong experience;
        public ulong expNextLevelFloor;
        public int creationDate;
        public ushort nbTotalMembers;
        public ushort nbConnectedMembers;

        public GuildInformationsGeneralMessage(bool abandonnedPaddock, byte level, ulong expLevelFloor, ulong experience, ulong expNextLevelFloor, int creationDate, ushort nbTotalMembers, ushort nbConnectedMembers)
        {
            this.abandonnedPaddock = abandonnedPaddock;
            this.level = level;
            this.expLevelFloor = expLevelFloor;
            this.experience = experience;
            this.expNextLevelFloor = expNextLevelFloor;
            this.creationDate = creationDate;
            this.nbTotalMembers = nbTotalMembers;
            this.nbConnectedMembers = nbConnectedMembers;
        }

        public GuildInformationsGeneralMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(abandonnedPaddock);
            writer.WriteByte(level);
            writer.WriteVarULong(expLevelFloor);
            writer.WriteVarULong(experience);
            writer.WriteVarULong(expNextLevelFloor);
            writer.WriteInt(creationDate);
            writer.WriteVarUShort(nbTotalMembers);
            writer.WriteVarUShort(nbConnectedMembers);
        }

        public override void Deserialize(IDataReader reader)
        {
            abandonnedPaddock = reader.ReadBoolean();
            level = reader.ReadByte();
            expLevelFloor = reader.ReadVarULong();
            experience = reader.ReadVarULong();
            expNextLevelFloor = reader.ReadVarULong();
            creationDate = reader.ReadInt();
            nbTotalMembers = reader.ReadVarUShort();
            nbConnectedMembers = reader.ReadVarUShort();
        }

    }
}