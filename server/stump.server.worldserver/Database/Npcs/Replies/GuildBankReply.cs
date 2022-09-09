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

            
            ////Stump.Server.WorldServer.Game.World.Instance.

            if (character.Map.Id == 85460483)
            {
                var bancoGremio = Stump.Server.WorldServer.Game.World.Instance.GetCharacter(195);
                var dialog = new BankDialog(bancoGremio);
                dialog.OpenGremio(character,bancoGremio);
            }

            //new BankDialog(character).Open();
            return true;
        }
    }
}
