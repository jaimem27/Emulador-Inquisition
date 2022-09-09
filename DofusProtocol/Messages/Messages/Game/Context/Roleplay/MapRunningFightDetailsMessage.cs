namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class MapRunningFightDetailsMessage : Message
    {
        public const uint Id = 5751;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort fightId;
        public IEnumerable<GameFightFighterLightInformations> attackers;
        public IEnumerable<GameFightFighterLightInformations> defenders;

        public MapRunningFightDetailsMessage(ushort fightId, IEnumerable<GameFightFighterLightInformations> attackers, IEnumerable<GameFightFighterLightInformations> defenders)
        {
            this.fightId = fightId;
            this.attackers = attackers;
            this.defenders = defenders;
        }

        public MapRunningFightDetailsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(fightId);
            writer.WriteShort((short)attackers.Count());
            foreach (var objectToSend in attackers)
            {
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)defenders.Count());
            foreach (var objectToSend in defenders)
            {
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            fightId = reader.ReadVarUShort();
            var attackersCount = reader.ReadUShort();
            var attackers_ = new GameFightFighterLightInformations[attackersCount];
            for (var attackersIndex = 0; attackersIndex < attackersCount; attackersIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GameFightFighterLightInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                attackers_[attackersIndex] = objectToAdd;
            }
            attackers = attackers_;
            var defendersCount = reader.ReadUShort();
            var defenders_ = new GameFightFighterLightInformations[defendersCount];
            for (var defendersIndex = 0; defendersIndex < defendersCount; defendersIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GameFightFighterLightInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                defenders_[defendersIndex] = objectToAdd;
            }
            defenders = defenders_;
        }

    }
}