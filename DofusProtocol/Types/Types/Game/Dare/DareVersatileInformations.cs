namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareVersatileInformations
    {
        public const short Id  = 504;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double DareId { get; set; }
        public int CountEntrants { get; set; }
        public int CountWinners { get; set; }

        public DareVersatileInformations(double dareId, int countEntrants, int countWinners)
        {
            this.DareId = dareId;
            this.CountEntrants = countEntrants;
            this.CountWinners = countWinners;
        }

        public DareVersatileInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DareId);
            writer.WriteInt(CountEntrants);
            writer.WriteInt(CountWinners);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            DareId = reader.ReadDouble();
            CountEntrants = reader.ReadInt();
            CountWinners = reader.ReadInt();
        }

    }
}
