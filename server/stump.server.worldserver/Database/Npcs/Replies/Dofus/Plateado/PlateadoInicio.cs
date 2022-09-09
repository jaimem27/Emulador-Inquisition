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
    [Discriminator("PlateadoInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class PlateadoInicio : NpcReply
    {
        public PlateadoInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(16889);
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {
                
                var itemRequisitoUno = Singleton<ItemManager>.Instance.TryGetTemplate(1782);
                var itemUno = character.Inventory.TryGetItem(itemRequisitoUno);
                var itemRequisitoDos = Singleton<ItemManager>.Instance.TryGetTemplate(303);
                var itemDos = character.Inventory.TryGetItem(itemRequisitoDos);
                var itemRequisitoTres = Singleton<ItemManager>.Instance.TryGetTemplate(17123);
                var itemTres = character.Inventory.TryGetItem(itemRequisitoTres);
                var itemRequisitoCuatro = Singleton<ItemManager>.Instance.TryGetTemplate(289);
                var itemCuatro = character.Inventory.TryGetItem(itemRequisitoCuatro);

                if (itemUno != null && itemDos != null && itemTres != null && itemCuatro != null && itemUno.Stack >= 20 && itemDos.Stack >= 15 && itemTres.Stack >= 20 && itemCuatro.Stack >= 30 )
                {

                    character.Inventory.RemoveItem(itemUno, 20);
                    character.Inventory.RemoveItem(itemDos, 15);
                    character.Inventory.RemoveItem(itemTres, 20);
                    character.Inventory.RemoveItem(itemCuatro, 30);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15605);
                    character.Inventory.AddItem(itemDosQuest);

                    character.SendServerMessage("Has terminado la etapa 1 de 3.");
                    character.SendServerMessage("Has conseguido 410 265 puntos de experencia.");
                    character.AddExperience(410265);
                    character.SendServerMessage("Has conseguido 1480 Kamas.");
                    character.Inventory.AddKamas(1480);
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
                character.SendServerMessage("Ya has obtenido el Dofus Plateado.");
                return false;
            }

        }

    }
}