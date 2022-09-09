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
    [Discriminator("ZanaTres", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class ZanaTres : NpcReply
    {
        public ZanaTres(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(16894); //Totem del Sur 
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {

                //Inicio Quest (item para poder dropear requisitos)
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16895); //Totem del este
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);
                character.Inventory.AddItem(itemUnoQuest);


                //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16896); //Totem del norte
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                var itemTresQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16897); //Tótem del Oeste
                var itemTres = character.Inventory.TryGetItem(itemTresQuest);

                var itemCuatroQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16898); //Llave zarposa
                var itemCuatro = character.Inventory.TryGetItem(itemCuatroQuest);

                var itemCincoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16899); //Llave dentada
                var itemCinco = character.Inventory.TryGetItem(itemCincoQuest);

                if (itemDos != null && itemTres != null && itemCuatro != null && itemCinco != null)
                {

                    character.Inventory.RemoveItem(itemDos, (int)itemDos.Stack);
                    character.Inventory.RemoveItem(itemTres, (int)itemTres.Stack);
                    character.Inventory.RemoveItem(itemCuatro, (int)itemCuatro.Stack);
                    character.Inventory.RemoveItem(itemCinco, (int)itemCinco.Stack);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    character.SendServerMessage("Has terminado la Etapa 3 de 7.");
                    character.SendServerMessage("Has conseguido 610 092 puntos de experencia.");
                    character.AddExperience(610092);
                    character.SendServerMessage("Has conseguido 4 780 Kamas.");
                    character.Inventory.AddKamas(4780);

                    character.Inventory.RemoveItem(faseCompletada, (int)faseCompletada.Stack);
                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(16900); // Bola mística para 4 etapa
                    character.Inventory.AddItem(itemFinal);
                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No cumples con los requisitos para seguir.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("No has completado la segunda fase.");
                return false;
            }

        }

    }
}