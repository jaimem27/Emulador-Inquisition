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
    [Discriminator("PlateadoTres", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class PlateadoTres : NpcReply
    {
        public PlateadoTres(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(16138);
            var item = character.Inventory.TryGetItem(faseTerminada);

            if (item != null)
            {

                var itemRequisitoUno = Singleton<ItemManager>.Instance.TryGetTemplate(16888);
                var itemUno = character.Inventory.TryGetItem(itemRequisitoUno);

                if (itemUno != null )
                {
                    var itemAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(16138);
                    var itemAnteriorQuest = character.Inventory.TryGetItem(itemAnterior);
                    character.Inventory.RemoveItem(itemAnteriorQuest, 1);
                    character.Inventory.RemoveItem(itemUno, (int)itemUno.Stack);

                    var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(19629);
                    character.Inventory.AddItem(itemDosQuest);

                    var itemDosQuest1 = Singleton<ItemManager>.Instance.TryGetTemplate(16889);
                    character.Inventory.AddItem(itemDosQuest1);

                    character.SendServerMessage("Has terminado la etapa 3 de 3.");
                    character.SendServerMessage("Has Obtenido el Dofus Plateado.");
                    character.SendServerMessage("Has ganado el titulo de Marcapáginas.");
                    character.SendServerMessage("Has ganado el Ornamento : Aventurero Satisfactorio.");
                    character.SendServerMessage("Has conseguido 1 089 720 puntos de experencia.");
                    character.AddExperience(1089720);
                    character.SendServerMessage("Has conseguido 4 760 Kamas.");
                    character.Inventory.AddKamas(4760);
                    character.AddTitle(80);
                    character.AddOrnament(39);

                    string annonce = character.Name + " ha terminado la misión del Dofus Plateado!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);
                    character.RefreshActor();

                }
                else
                {
                    character.SendServerMessage("No tienes la carne de oso.");
                    return false;
                }

                return true;
            }
            else
            {
                character.SendServerMessage("Ya tienes el Dofus Plateado.");
                return false;
            }

        }

    }
}