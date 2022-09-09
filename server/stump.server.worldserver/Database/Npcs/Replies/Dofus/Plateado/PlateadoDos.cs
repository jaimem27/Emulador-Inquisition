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
    [Discriminator("PlateadoDos", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class PlateadoDos : NpcReply
    {
        public PlateadoDos(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(15605);
            var item = character.Inventory.TryGetItem(faseTerminada);

            if (item != null)
            {

                var itemRequisitoUno = Singleton<ItemManager>.Instance.TryGetTemplate(17125);
                var itemUno = character.Inventory.TryGetItem(itemRequisitoUno);
                var itemRequisitoDos = Singleton<ItemManager>.Instance.TryGetTemplate(17127);
                var itemDos = character.Inventory.TryGetItem(itemRequisitoDos);

                if (itemUno != null && itemDos != null && itemUno.Stack >= 1 && itemDos.Stack >= 1 )
                {

                    character.Inventory.RemoveItem(itemUno, 1);
                    character.Inventory.RemoveItem(itemDos, 1);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16138);
                    character.Inventory.AddItem(itemDosQuest);
                    var itemAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(15605);
                    var itemAnteriorQuest = character.Inventory.TryGetItem(itemAnterior);
                    character.Inventory.RemoveItem(itemAnteriorQuest,1);

                    character.SendServerMessage("Has terminado la etapa 2 de 3.");
                    character.SendServerMessage("Has conseguido 726 480 puntos de experencia.");
                    character.AddExperience(726480);
                    character.SendServerMessage("Has conseguido 2 380 Kamas.");
                    character.Inventory.AddKamas(2380);
                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes los recursos de la lista.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("No has terminado la anterior fase.");
                return false;
            }

        }

    }
}