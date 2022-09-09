using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Commands.Commands.CommandPlayer
{
    public class StopTreasureCommand : SubCommandContainer
    {
        public StopTreasureCommand()
        {
            Aliases = new[] { "parar" };
            RequiredRole = RoleEnum.Player;
            Description = "Abandonas la busqueda del tesoro.";
        }

        public override void Execute(TriggerBase trigger)
        {
            var gameTrigger = trigger as GameTrigger;

            if (gameTrigger.Character.Record.TreasureSearch != 0 && gameTrigger.Character.Record.TreasureTimeStart < DateTime.Now)
            {
                gameTrigger.Character.Record.TreasureSearch = 0;
                gameTrigger.Character.Record.TreasureMapCoffre = null;
                gameTrigger.Character.Record.TreasureTimeStart = DateTime.Now;
                gameTrigger.Character.Record.TreasureIndice = null;

                DataManager.DefaultDatabase.Update(gameTrigger.Character.Record);

                if (gameTrigger.Character.HasEmote(EmotesEnum.COMPLETE_A_QUEST))
                {
                    gameTrigger.Character.RemoveEmote(EmotesEnum.COMPLETE_A_QUEST);
                }

                gameTrigger.Character.SendServerMessage("Has abandonado la busqueda del Tesoro.", System.Drawing.Color.Orange);
                gameTrigger.Character.DisplayNotification("Dejaste de buscar tu <b> Tesoro </b>.", NotificationEnum.INFORMATION);

                DataManager.DefaultDatabase.Update(gameTrigger.Character.Record);
            }
            else if (gameTrigger.Character.Record.TreasureSearch != 0 && gameTrigger.Character.Record.TreasureTimeStart > DateTime.Now)
            {
                if (gameTrigger.Character.Record.TreasureTimeStart.Minute - DateTime.Now.Minute == 1)
                {
                    gameTrigger.Character.SendServerMessage("Debes esperar <b>" + (gameTrigger.Character.Record.TreasureTimeStart.Minute - DateTime.Now.Minute) + " minutos</b> antes de que puedas renunciar a tu busqueda.", System.Drawing.Color.Red);
                }
                else
                {
                    gameTrigger.Character.SendServerMessage("Debes esperar <b>" + (gameTrigger.Character.Record.TreasureTimeStart.Minute - DateTime.Now.Minute) + " minutos</b> antes de que puedas renunciar a tu busqueda.", System.Drawing.Color.Red);
                }
            }
            else
            {
                gameTrigger.Character.SendServerMessage("Debes tener una busqueda en curso para poder abandonarla.", System.Drawing.Color.Red);
            }
        }
    }
}