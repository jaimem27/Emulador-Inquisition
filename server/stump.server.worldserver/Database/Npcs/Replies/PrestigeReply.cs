using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Shortcuts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.WorldServer.Handlers.Mounts;
using Stump.Server.WorldServer.Handlers.Shortcuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("AumentarArtefacto", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class PrestigeReply : NpcReply
    {
        public PrestigeReply(NpcReplyRecord record)
          : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            var seBuscaItemArtefacto = Singleton<ItemManager>.Instance.TryGetTemplate(21014);
            var item = character.Inventory.TryGetItem(seBuscaItemArtefacto);
            bool correcto;

            if (item != null)
            {
                correcto = character.IncrementPrestige();
                character.SendServerMessage("El Artefacto ha evolucionado gracias al Dios Xelor.");
                character.Inventory.RemoveItem(item, 1);
            }
            else
            {
                character.SendServerMessage("No cumples con los requisitos para evolucionar el Artefacto.");
                correcto = false;
            }


            return correcto;
        }
    }
}




