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
    [Discriminator("DokokoCuatro", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class DokokoCuatro : NpcReply
    {
        public DokokoCuatro(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var faseAnterior = Singleton<ItemManager>.Instance.TryGetTemplate(17085); // Fuego interno brillante
            var faseCompletada = character.Inventory.TryGetItem(faseAnterior);

            if (faseCompletada != null)
            {


                if (character.Achievement.AchievementIsCompleted(485))
                {
                    character.Inventory.RemoveItem(faseCompletada, (int)faseCompletada.Stack);

                    character.SendServerMessage("Has terminado la Etapa 4 de 4.");
                    character.SendServerMessage("Has conseguido 5 047 500 puntos de experencia.");
                    character.AddExperience(5047500);
                    character.SendServerMessage("Has conseguido 23 960 Kamas.");
                    character.SendServerMessage("Has obtenido el Dokoko.");
                    var dofus = Singleton<ItemManager>.Instance.TryGetTemplate(17078);
                    character.Inventory.AddItem(dofus);
                    string annonce = character.Name + " ha terminado completar el Dofus Dokoko!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    character.Inventory.AddKamas(23960);
                    character.AddOrnament(99);
                    character.AddTitle(75);

                    var itemFinal = Singleton<ItemManager>.Instance.TryGetTemplate(17077); // Tofu de peluche
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