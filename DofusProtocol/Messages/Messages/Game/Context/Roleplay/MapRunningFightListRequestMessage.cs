﻿namespace Stump.DofusProtocol.Messages
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class MapRunningFightListRequestMessage : Message
    {
        public const uint Id = 5742;
        public override uint MessageId
        {
            get { return Id; }
        }

        public MapRunningFightListRequestMessage() { }

        public override void Serialize(IDataWriter writer) { }

        public override void Deserialize(IDataReader reader) { }

    }
}