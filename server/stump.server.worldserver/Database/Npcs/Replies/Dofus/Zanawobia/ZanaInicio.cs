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
    [Discriminator("ZanaInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class ZanaInicio : NpcReply
    {
        public ZanaInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(16890); //Marca de Meriana
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {

                //Dar objetos -- 
                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(16492); //Tablas de Surf
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);

                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(428); //Salvia
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                var itemTresQuest = Singleton<ItemManager>.Instance.TryGetTemplate(361); //Zanawobias
                var itemTres = character.Inventory.TryGetItem(itemTresQuest);

                if (itemUno != null && itemUno.Stack >= 20 && itemDos != null && itemDos.Stack > 49 && itemTres != null && itemTres.Stack > 14)
                {

                    character.Inventory.RemoveItem(itemUno, 20);
                    character.Inventory.RemoveItem(itemDos, 50);
                    character.Inventory.RemoveItem(itemTres, 15);


                    character.SendServerMessage("Has terminado la Etapa 1 de 7.");
                    character.SendServerMessage("Has conseguido 320 817 puntos de experencia.");
                    character.AddExperience(320817);
                    character.SendServerMessage("Has conseguido 2 905 Kamas.");
                    character.Inventory.AddKamas(2905);

                    string annonce = character.Name + " ha empezado la búsqueda del Dofus Zanahowia!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(16891); // Marca de badmorva item para 2 etapa
                    character.Inventory.AddItem(itemFinal);
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
                character.SendServerMessage("Ya has obtenido el Dofus Zanahowia.");
                return false;
            }

        }

    }
}