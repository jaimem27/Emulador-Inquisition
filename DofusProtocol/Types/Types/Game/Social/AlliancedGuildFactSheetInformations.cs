namespace Stump.DofusProtocol.Types
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class AlliancedGuildFactSheetInformations : GuildInformations
    {
        public new const short Id = 422;
        public override short TypeId
        {
            get { return Id; }
        }
        public BasicNamedAllianceInformations allianceInfos;

        public AlliancedGuildFactSheetInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem, BasicNamedAllianceInformations allianceInfos)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.guildEmblem = guildEmblem;
            this.allianceInfos = allianceInfos;
        }

        public AlliancedGuildFactSheetInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInfos = new BasicNamedAllianceInformations();
            allianceInfos.Deserialize(reader);
        }

    }
}