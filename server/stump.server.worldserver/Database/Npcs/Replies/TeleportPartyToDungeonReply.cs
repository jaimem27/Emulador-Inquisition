using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Game.Parties;
using Stump.Server.WorldServer.Game.Maps.Teleport;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("TeleportParty", typeof(NpcReply), typeof(NpcReplyRecord))]
    public class TeleportPartyToDungeonReply : NpcReply
    {
        public TeleportPartyToDungeonReply(NpcReplyRecord record) : base(record)
        {
        }

        /// <summary>
        /// Parameter 0
        /// </summary>
        public ushort DungId
        {
            get
            {
                return Record.GetParameter<ushort>(0);
            }
            set
            {
                Record.SetParameter(0, value);
            }
        }


        public override bool Execute(Npc npc, Character character)
        {
            if (!base.Execute(npc, character))
                return false;

            var party = character.Party;

            if (party == null)
            {//Impossível – você não tem um grupo!
                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 312);
                return false;
            }

            foreach (var popup in party.Members.ToList().Where(x => character != x).Select(member => new Game.Dungs.DungInvitation(character.Position, character, member, DungId)))
                popup.Display();

            //party.Members.Select(x => new DungInvitation(character.Position, character, x, DungId));
            //party.ForEach(x => { if (character != x) new DungInvitation(character.Position, character, x, 10).Display(); } );

            return true;
        }
    }
}
