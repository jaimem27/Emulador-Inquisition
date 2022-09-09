using System;
using Stump.Core.Reflection;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Bank;
using Stump.Server.WorldServer.Game;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("GuildBank", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class GuildBankReply : NpcReply
    {
        public GuildBankReply(NpcReplyRecord record)
          : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            if (!character.HasEmote(DofusProtocol.Enums.EmotesEnum.EMOTE_GUILD)){
                character.SendServerMessage("No te has unido a ningun gremio");
                return false;
            }

            if (character.Guild.BankInUse == 0)
            {
                var banco = new GuildBankDialog(character);
                banco.Open();
                return true;
            }
            else
            {
                character.SendServerMessage("<b>[ERROR]</b> : El banco gremial de <b>" + character.Guild.Name + "</b> está siendo utilizado por <b>" + character.Guild.BankInUseBy + "</b>.");
                return false;
            }

        }
    }
}
