using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Ascension", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class AscensionReply : NpcReply
    {
        public AscensionReply(NpcReplyRecord record)
          : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            if (character.GetAscensionStair() < 99)
            {
                var actualStairMaAscensionReplyp = AscensionEnum.GetAscensionFloorMap(character.AscensionStair)[0];
                Console.WriteLine(actualStairMaAscensionReplyp);
                Map map = Game.World.Instance.GetMap(actualStairMaAscensionReplyp);

                var actualStairCell = AscensionEnum.GetAscensionFloorMap(character.AscensionStair)[1];
                Console.WriteLine(actualStairCell);

                int[] actualStairMonsters = AscensionEnum.GetAscensionFloorMonsters(character.AscensionStair);
                Console.WriteLine(actualStairMonsters);
                StartFight(character, map, actualStairCell, actualStairMonsters);
                return true;
            }
            else
            {
                character.OpenPopup("Ya has completado todas los pisos de la torre, ¡espera hasta la semana que viene!");
                return false;
            }

        }


        public void StartFight(Character character, Map map, int cell, int[] monsters)
        {
            if (!Game.World.Instance.GetMap(map.Id).Area.IsRunning) Game.World.Instance.GetMap(map.Id).Area.Start();
            character.Teleport(map, map.GetCell(cell));
            Task.Delay(1000).ContinueWith(t => {

                character.DisplayNotification($"Acabas de ser teletransportado al " + (character.AscensionStair + 1) + " piso de la torre.", NotificationEnum.INFORMATION);

                var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
                fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));

                foreach (int m in monsters)
                {
                    var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade((int)m, 5);
                    var position = new ObjectPosition(map, map.GetCell(cell), (DirectionsEnum)5);
                    var monster = new Monster(grade, new MonsterGroup(0, position));

                    fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
                }
                fight.StartPlacement();

                ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
                new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
                character.SaveLater();
                return true;
            });
        }
    }

}




