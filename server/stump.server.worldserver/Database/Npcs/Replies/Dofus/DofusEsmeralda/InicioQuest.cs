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
    [Discriminator("EsmeraldaInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class EsmeraldaInicio : NpcReply
    {
        public EsmeraldaInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(15599);
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {
                //Inicio de mision
                var itemInicioQuest1 = Singleton<ItemManager>.Instance.TryGetTemplate(15425);
                character.Inventory.AddItem(itemInicioQuest1);

                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15427);
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);

                if (itemUno != null && itemUno.Stack >= 15)
                {
                    var itemInicioQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15425);
                    var itemInicio = character.Inventory.TryGetItem(itemInicioQuest);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);
                    character.Inventory.RemoveItem(itemInicio, (int)itemInicio.Stack);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15426);
                    character.Inventory.AddItem(itemDosQuest);

                    character.SendServerMessage("Has completado la fase 1 de 5 de la misión.");
                    character.SendServerMessage("Has conseguido 992 754 puntos de experencia.");
                    character.AddExperience(992754);
                    character.SendServerMessage("Has conseguido 15 072 Kamas.");
                    character.Inventory.AddKamas(15072);
                    string annonce = character.Name + " ha empezado la búsqueda del Dofus Esmeralda!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);
                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes las muestras suficientes.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("Ya has obtenido el Dofus Esmeralda.");
                return false;
            }

        }

    }
}