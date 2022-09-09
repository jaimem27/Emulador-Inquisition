using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Shortcuts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.WorldServer.Handlers.Mounts;
using Stump.Server.WorldServer.Handlers.Shortcuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("CambiarArtefacto", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class CambioArtefacto : NpcReply
    {
        public CambioArtefacto(NpcReplyRecord record)
          : base(record)
        {
        }

        public short ArtefactoId
        {
            get
            {
                return base.Record.GetParameter<short>(0u, false);
            }
            set
            {
                base.Record.SetParameter<short>(0u, value);
            }
        }

        public override bool Execute(Npc npc, Character character)
        {
            var item = character.GetPrestigeItem();

            if (item == null)
                item = character.CreatePrestigeItem();
            else
            {
                switch (ArtefactoId)
                {
                    case 1:
                        item.UpdateEffectsViajero();
                        character.OpenPopup($"Acabas de elegir el Artefacto del Viajero ! \r\n Recuerda que puedes cambiar de artefacto cuando quieras.");
                        break;
                    case 2:
                        item.UpdateEffectsValeroso();
                        character.OpenPopup($"Acabas de elegir el Artefacto del Valeroso ! \r\n Recuerda que puedes cambiar de artefacto cuando quieras.");
                        break;
                    case 3:
                        item.UpdateEffectsCorazon();
                        character.OpenPopup($"Acabas de elegir el Artefacto del Corazón ! \r\n Recuerda que puedes cambiar de artefacto cuando quieras.");
                        break;
                    case 4:
                        item.UpdateEffectsMajestuoso();
                        character.OpenPopup($"Acabas de elegir el Artefacto del Majestuoso ! \r\n Recuerda que puedes cambiar de artefacto cuando quieras.");
                        break;
                    case 5:
                        item.UpdateEffectsRocoso();
                        character.OpenPopup($"Acabas de elegir el Artefacto del Rocoso ! \r\n Recuerda que puedes cambiar de artefacto cuando quieras.");
                        break;
                    case 6:
                        item.UpdateEffectsTramposo();
                        character.OpenPopup($"Acabas de elegir el Artefacto del Tramposo ! \r\n Recuerda que puedes cambiar de artefacto cuando quieras.");
                        break;
                    default:
                        character.SendServerMessage("No cumples con los requisitos.");
                        break;
                }
                
                character.Inventory.RefreshItem(item);
            }
            character.RefreshActor();

            //character.SendServerMessage("No cumples con los requisitos para evolucionar el Artefacto.");

            return true;
        }
    }
}




