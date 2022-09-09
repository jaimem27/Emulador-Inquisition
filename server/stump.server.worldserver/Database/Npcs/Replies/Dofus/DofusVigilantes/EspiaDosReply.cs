using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Context;

namespace Database.Npcs.Replies
{
    [Discriminator("EspiaDosReply", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class EspiaDosReply : NpcReply
    {
        public EspiaDosReply(NpcReplyRecord record)
            : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {


            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(3955, 5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(3952, 5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(3951, 5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();
            character.RefreshActor();

            return true;
        }

    }
}