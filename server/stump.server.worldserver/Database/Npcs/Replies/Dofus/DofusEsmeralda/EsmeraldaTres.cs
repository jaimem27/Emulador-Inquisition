using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.Server.WorldServer.Database.Monsters;
using System.Drawing;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.DofusProtocol.Messages;

namespace Database.Npcs.Replies
{
    [Discriminator("EsmeraldaTres", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class Esmeralda3 : NpcReply
    {
        public Esmeralda3(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {

            var muestra = Singleton<ItemManager>.Instance.TryGetTemplate(15595);
            var item = character.Inventory.TryGetItem(muestra);

            if (item != null )
            {
                var misionAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(15594);
                var Anterior = character.Inventory.TryGetItem(misionAnterior);
                character.Inventory.RemoveItem(Anterior, (int)Anterior.Stack);

                character.Inventory.RemoveItem(item, (int)item.Stack);

                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15596);
                character.Inventory.AddItem(itemDosQuest);

                character.SendServerMessage("Has completado la fase 4 de 5 de la misión.");
                character.SendServerMessage("Has conseguido 2 340 000 puntos de experencia.");
                character.AddExperience(2340000);
                character.SendServerMessage("Has conseguido 28752 Kamas.");
                character.Inventory.AddKamas(28752);
                character.RefreshActor();
            }
            else
            {
                character.SendServerMessage("No tienes la muestra del Maxilubu.");
            }


            character.RefreshActor();


            return true;
        }

    }
}