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
    [Discriminator("EsmeraldaDos", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class Esmeralda2 : NpcReply
    {
        public Esmeralda2(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {

            var escamaUno = Singleton<ItemManager>.Instance.TryGetTemplate(14993);
            var item = character.Inventory.TryGetItem(escamaUno);
            var escamaDos = Singleton<ItemManager>.Instance.TryGetTemplate(14994);
            var item1 = character.Inventory.TryGetItem(escamaDos);
            var escamaTres = Singleton<ItemManager>.Instance.TryGetTemplate(14995);
            var item2 = character.Inventory.TryGetItem(escamaTres);

            if (item != null && item1 != null && item2 != null)
            {
                var misionAnterior  = Singleton<ItemManager>.Instance.TryGetTemplate(15593);
                var Anterior = character.Inventory.TryGetItem(misionAnterior);
                character.Inventory.RemoveItem(Anterior, (int)Anterior.Stack);
                
                character.Inventory.RemoveItem(item, (int)item.Stack);
                character.Inventory.RemoveItem(item1, (int)item1.Stack);
                character.Inventory.RemoveItem(item2, (int)item2.Stack);


                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15594);
                character.Inventory.AddItem(itemDosQuest);

                character.SendServerMessage("Has completado la fase 3 de 5 de la misión.");
                character.SendServerMessage("Has conseguido 2 340 000 puntos de experencia.");
                character.AddExperience(2340000);
                character.SendServerMessage("Has conseguido 28 752 Kamas.");
                character.Inventory.AddKamas(28752);
                character.RefreshActor();
            }
            else
            {
                character.SendServerMessage("No tienes las escamas.");
            }


            character.RefreshActor();


            return true;
        }

    }
}