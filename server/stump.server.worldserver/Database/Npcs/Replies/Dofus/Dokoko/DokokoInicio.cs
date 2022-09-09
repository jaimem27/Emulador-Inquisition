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
    [Discriminator("DokokoInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class DokokoInicio : NpcReply
    {
        public DokokoInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(17077); //Tofu de peluche
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {

                //Dar objetos -- 
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2618); //kokopaja
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);

                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(1678); //Nuez de kokoko traumatizante
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                var itemTresQuest = Singleton<ItemManager>.Instance.TryGetTemplate(997); //Nuez de kokoko
                var itemTres = character.Inventory.TryGetItem(itemTresQuest);

                var itemCuatroQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2617); //Rodaja de Nozdekoko
                var itemCuatro = character.Inventory.TryGetItem(itemCuatroQuest);

                var itemCincoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2624); //Hoja de pekekoko
                var itemCinco = character.Inventory.TryGetItem(itemCincoQuest);

                var itemSeisQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2619); //Leche de tortuga
                var itemSeis = character.Inventory.TryGetItem(itemSeisQuest);

                var itemSieteQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2611); //Caparazon amarila
                var itemSiete = character.Inventory.TryGetItem(itemSieteQuest);

                var itemOchoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2613); //Caparazon azul
                var itemOcho = character.Inventory.TryGetItem(itemOchoQuest);

                var itemNueveQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2610); //Caparazon roja
                var itemNueve = character.Inventory.TryGetItem(itemNueveQuest);

                var itemDiezQuest = Singleton<ItemManager>.Instance.TryGetTemplate(2609); //Caparazon verde
                var itemDiez = character.Inventory.TryGetItem(itemDiezQuest);

                if (itemUno != null  && itemDos != null  && itemTres != null && itemCuatro != null && itemCinco != null && itemSeis != null && itemSiete != null && itemOcho != null && itemNueve != null && itemDiez != null &&
                    itemUno.Stack >= 15 && itemDos.Stack >= 5 && itemTres.Stack >= 10 && itemCuatro.Stack >= 10 && itemCinco.Stack >= 20 && itemSeis.Stack >= 15 && itemSiete.Stack >= 1 && itemOcho.Stack >= 1 && itemNueve.Stack >= 1 && itemDiez.Stack >= 1)
                {

                    character.Inventory.RemoveItem(itemUno, 15);
                    character.Inventory.RemoveItem(itemDos, 5);
                    character.Inventory.RemoveItem(itemTres, 10);
                    character.Inventory.RemoveItem(itemCuatro, 10);
                    character.Inventory.RemoveItem(itemCinco, 20);
                    character.Inventory.RemoveItem(itemSeis, 15);
                    character.Inventory.RemoveItem(itemSiete, 1);
                    character.Inventory.RemoveItem(itemOcho, 1);
                    character.Inventory.RemoveItem(itemNueve, 1);
                    character.Inventory.RemoveItem(itemDiez, 1);


                    character.SendServerMessage("Has terminado la Etapa 1 de 4.");
                    character.SendServerMessage("Has conseguido 1 023 750 puntos de experencia.");
                    character.AddExperience(1023750);
                    character.SendServerMessage("Has conseguido 11 980 Kamas.");
                    character.Inventory.AddKamas(11980);

                    character.SendServerMessage("Has conseguido el Nonoom.");
                    var pet = Singleton<ItemManager>.Instance.TryGetTemplate(6716); // Pergamino de Meriana
                    character.Inventory.AddItem(pet);


                    string annonce = character.Name + " ha empezado la búsqueda del Dofus Dokoko!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    var itemSiguienteEtapa = Singleton<ItemManager>.Instance.TryGetTemplate(17080); // Pergamino de Meriana
                    character.Inventory.AddItem(itemSiguienteEtapa);
                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes los materiales necesarios.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("Ya has obtenido el Dofus Dokoko.");
                return false;
            }

        }

    }
}