using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Commands.Commands.Teleport
{
    namespace Stump.Server.WorldServer.Game.Dialogs.Interactives
    {

        public class AscensionZaap : CommandBase
        {
            public AscensionZaap()
            {
                Aliases = new[] { "torre" };
                RequiredRole = RoleEnum.Moderator;
                Description = "Teletransporte a la torre.";
            }

            public override void Execute(TriggerBase trigger)
            {
                var gameTrigger = trigger as GameTrigger;
                if (gameTrigger != null)
                {


                    if (gameTrigger.Character.GetAscensionStair() < 99)
                    {
                        var actualStairMap = AscensionEnum.GetAscensionFloorMap(gameTrigger.Character.AscensionStair)[0];
                        Map map = World.Instance.GetMap(actualStairMap);

                        var actualStairCell = AscensionEnum.GetAscensionFloorMap(gameTrigger.Character.AscensionStair)[1];

                        int[] actualStairMonsters = AscensionEnum.GetAscensionFloorMonsters(gameTrigger.Character.AscensionStair);
                        StartFight(gameTrigger.Character, map, actualStairCell, actualStairMonsters);
                    }
                    else
                        gameTrigger.Character.OpenPopup("Ya has completado todas los pisos de la torre, ¡espera hasta la semana que viene!");
                }
            }



            public void StartFight(Character character, Map map, int cell, int[] monsters)
            {
                if (!World.Instance.GetMap(map.Id).Area.IsRunning) World.Instance.GetMap(map.Id).Area.Start();
                character.Teleport(map, map.GetCell(cell));
                Task.Delay(1000).ContinueWith(t => {

                    character.DisplayNotification($"Has sido teletransportado al piso " + (character.AscensionStair + 1) + " de la torre.", NotificationEnum.INFORMATION);

                    var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
                    
                    
                    fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
                    
                    foreach (int m in monsters)
                    {
                        var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade((int)m, 5);
                        var position = new ObjectPosition(map, map.GetCell(cell), (DirectionsEnum)5);
                        var monster = new Monster(grade, new MonsterGroup(0, position));

                        fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
                    }
                    fight.HideBlades();
                    fight.StartPlacement();
                    

                    ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
                    new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
                    character.SaveLater();

                });
            }
        }
    }
}

