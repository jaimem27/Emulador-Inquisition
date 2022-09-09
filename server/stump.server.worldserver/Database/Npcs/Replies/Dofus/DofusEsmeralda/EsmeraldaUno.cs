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
    [Discriminator("EsmeraldaUno", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class Esmeralda1 : NpcReply
    {
        public Esmeralda1(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {

            var muestraUno = Singleton<ItemManager>.Instance.TryGetTemplate(15443);
            var item = character.Inventory.TryGetItem(muestraUno);
            var muestraDos = Singleton<ItemManager>.Instance.TryGetTemplate(15578);
            var item2 = character.Inventory.TryGetItem(muestraDos);

            if (item != null && item2 != null)
            {
                var itemDosQuest1 = Singleton<ItemManager>.Instance.TryGetTemplate(15426);
                var itemQuestAnterrior = character.Inventory.TryGetItem(itemDosQuest1);
                character.Inventory.RemoveItem(itemQuestAnterrior, (int)itemQuestAnterrior.Stack);

                character.Inventory.RemoveItem(item, (int)item.Stack);
                character.Inventory.RemoveItem(item2, (int)item2.Stack);

                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15593);
                character.Inventory.AddItem(itemDosQuest);

                character.SendServerMessage("Has completado la fase 2 de 5 de la misión.");
                character.SendServerMessage("Has conseguido 903 231 puntos de experencia.");
                character.AddExperience(903231);
                character.SendServerMessage("Has conseguido 11 856 Kamas.");
                character.Inventory.AddKamas(11856);
                character.RefreshActor();
            }
            else
            {
                character.SendServerMessage("No tienes las muestras suficientes.");
            }


            character.RefreshActor();


            return true;
        }

    }
}