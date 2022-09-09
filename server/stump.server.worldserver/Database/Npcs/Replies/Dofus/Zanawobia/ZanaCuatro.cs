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
    [Discriminator("ZanaCuatro", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class ZanaCuatro : NpcReply
    {
        public ZanaCuatro(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(16900); //Bola Mistica
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {

               //Comprobar requisitos
                var itemDosQuest = Singleton<ItemManager>.Instance.TryGetTemplate(97); //Dagas maderucha
                var itemDos = character.Inventory.TryGetItem(itemDosQuest);

                var itemTresQuest = Singleton<ItemManager>.Instance.TryGetTemplate(44); //Espada maderucha
                var itemTres = character.Inventory.TryGetItem(itemTresQuest);

                if (itemDos != null && itemTres != null && itemDos.Stack >= 20 && itemTres.Stack >= 20)
                {

                    character.Inventory.RemoveItem(itemDos, 25);
                    character.Inventory.RemoveItem(itemTres, 25);

                    character.SendServerMessage("Has terminado la Etapa 4 de 7.");
                    character.SendServerMessage("Has conseguido 610 092 puntos de experencia.");
                    character.AddExperience(610092);
                    character.SendServerMessage("Has conseguido 4 780 Kamas.");
                    character.Inventory.AddKamas(4780);

                    character.Inventory.RemoveItem(faseCompletada, (int)faseCompletada.Stack);
                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(16901); // Bola de fucus
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
                character.SendServerMessage("No has completado la tercera fase.");
                return false;
            }

        }

    }
}