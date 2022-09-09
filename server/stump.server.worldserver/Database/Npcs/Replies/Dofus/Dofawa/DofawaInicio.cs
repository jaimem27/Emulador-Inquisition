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
    [Discriminator("DofawaInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class DofawaInicio : NpcReply
    {
        public DofawaInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(15604);
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {
                //Inicio de mision
                var itemInicioQuest1 = Singleton<ItemManager>.Instance.TryGetTemplate(15602);
                character.Inventory.AddItem(itemInicioQuest1);

                var itemUnoQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15603);
                var itemUno = character.Inventory.TryGetItem(itemUnoQuest);

                if (itemUno != null )
                {
                    var itemInicioQuest = Singleton<ItemManager>.Instance.TryGetTemplate(15602);
                    var itemInicio = character.Inventory.TryGetItem(itemInicioQuest);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);
                    character.Inventory.RemoveItem(itemInicio, (int)itemInicio.Stack);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(7113);
                    character.Inventory.AddItem(itemDosQuest);

                    character.SendServerMessage("Has obtenido el Dofawa.");
                    character.SendServerMessage("Has conseguido 12 454 puntos de experencia.");
                    character.AddExperience(12454);
                    character.SendServerMessage("Has conseguido 303 Kamas.");
                    character.Inventory.AddKamas(303);
                    character.AddTitle(197);

                    string annonce = character.Name + " ha terminado la misión del Dofawa!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(15604);
                    character.Inventory.AddItem(itemFinal);
                    character.RefreshActor();
                }
                else
                {
                    character.SendServerMessage("No tienes el alma necesaria para forjar el Dofus.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("Ya has obtenido el Dofawa.");
                return false;
            }

        }

    }
}