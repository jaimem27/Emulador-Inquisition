namespace Stump.DofusProtocol.Types {
    using System;
    using Stump.Core.IO;

    [Serializable]
    public class PrismSubareaEmptyInfo {
        public const short Id = 438;
        public virtual short TypeId {
            get { return Id; }
        }
        public ushort subAreaId;
        public uint allianceId;

        public PrismSubareaEmptyInfo (ushort subAreaId, uint allianceId) {
            this.subAreaId = subAreaId;
            this.allianceId = allianceId;
        }

        public PrismSubareaEmptyInfo () { }

        public virtual void Serialize (IDataWriter writer) {
            writer.WriteVarUShort (subAreaId);
            writer.WriteVarUInt (allianceId);
        }

        public virtual void Deserialize (IDataReader reader) {
            subAreaId = reader.ReadVarUShort ();
            allianceId = reader.ReadVarUInt ();
        }

    }
}