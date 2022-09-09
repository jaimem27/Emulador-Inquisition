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
    [Discriminator("VigilantesInicio", typeof(NpcReply), new System.Type[] { typeof(NpcReplyRecord) })]
    class VigilantesInicio : NpcReply
    {
        public VigilantesInicio(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            var misionTerminada = Singleton<ItemManager>.Instance.TryGetTemplate(17089); //Bendición goblin
            var dofusObtenido = character.Inventory.TryGetItem(misionTerminada);

            if (dofusObtenido == null)
            {

                if (character.Achievement.AchievementIsCompleted(1093))
                {


                    character.SendServerMessage("Has terminado la Etapa 1 de 5.");
                    character.SendServerMessage("Has conseguido 4 318 347 puntos de experencia.");
                    character.AddExperience(4318347);
                    character.SendServerMessage("Has conseguido 39 481 Kamas.");
                    character.Inventory.AddKamas(39481);
                    character.AddTitle(155);

                    string annonce = character.Name + " ha empezado la búsqueda del Dofus de los Vigilantes!";
                    Singleton<World>.Instance.SendAnnounce(annonce, Color.Azure);

                    var itemSiguienteEtapa = Singleton<ItemManager>.Instance.TryGetTemplate(17466); // Tofu soñoliento
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
                character.SendServerMessage("Ya has obtenido el Dofus de los Vigilantes.");
                return false;
            }

        }

    }
}