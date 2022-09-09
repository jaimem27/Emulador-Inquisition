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
    [Discriminator("DotruzaInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class DotruzaInicio : NpcReply
    {
        public DotruzaInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(18265);
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {
                //Inicio de mision
                var itemInicioQuest1 = Singleton<ItemManager>.Instance.TryGetTemplate(18264);
                character.Inventory.AddItem(itemInicioQuest1);

                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15317);
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);

                if (itemUno != null)
                {
                    var itemInicioQuest = Singleton<ItemManager>.Instance.TryGetTemplate(18264);
                    var itemInicio = character.Inventory.TryGetItem(itemInicioQuest);
                    character.Inventory.RemoveItem(itemUno, 1);
                    character.Inventory.RemoveItem(itemInicio, (int)itemInicio.Stack);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15235);
                    character.Inventory.AddItem(itemDosQuest);

                    character.SendServerMessage("Has obtenido el Dotruz.");
                    character.SendServerMessage("Has conseguido 3 782 805 puntos de experencia.");
                    character.AddExperience(3782805);
                    character.SendServerMessage("Has conseguido 38 960 Kamas.");
                    character.Inventory.AddKamas(38960);
                    character.AddTitle(197);

                    string annonce = character.Name + " ha terminado la misión del Dotruz!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(18265);
                    character.Inventory.AddItem(itemFinal);
                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes el huevo fecundado.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("Ya has obtenido el Dotruz.");
                return false;
            }

        }

    }
}