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
    [Discriminator("DokokoDos", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class DokokoDos : NpcReply
    {
        public DokokoDos(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(17080); // Pergamino de Meriana
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {



                if (character.Achievement.AchievementIsCompleted(56)) { 

                    character.Inventory.RemoveItem(faseCompletada, (int)faseCompletada.Stack);

                    character.SendServerMessage("Has terminado la Etapa 2 de 4.");
                    character.SendServerMessage("Has conseguido 1 023 750 puntos de experencia.");
                    character.AddExperience(1023750);
                    character.SendServerMessage("Has conseguido 11 980 Kamas.");
                    character.Inventory.AddKamas(11980);


                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(17081); // Pergamino de Mami Ayuto
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
                character.SendServerMessage("No has completado la primera fase.");
                return false;
            }

        }

    }
}