//using System;
//using System.Drawing;
//using System.Linq;
//using Stump.Core.Reflection;
//using Stump.DofusProtocol.Enums;
//using Stump.DofusProtocol.Messages;
//using Stump.Server.BaseServer.Database;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
//using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
//using Stump.Server.WorldServer.Game.Fights;
//using Stump.Server.WorldServer.Game.Maps.Cells;
//using Stump.Server.WorldServer.Handlers.Context;

//namespace Stump.Server.WorldServer.Database.Npcs.Replies
//{
//    [Discriminator("B1", typeof(NpcReply), typeof(NpcReplyRecord))]
//    internal class Boss1 : NpcReply
//    {
//        public Boss1(NpcReplyRecord record) : base(record)
//        {
//        }

//        public int MonsterId
//        {
//            get { return Record.GetParameter<int>(0u); }
//            set { Record.SetParameter(0u, value); }
//        }

//        public override bool Execute(Npc npc, Character character)
//        {
//            var DeleteDopeul = new DoppleRecord();
//            var compareTime = DateTime.Now;
//            foreach (var dopeul in character.DoppleCollection.Dopeul.Where(dopeul => dopeul.DopeulId == MonsterId))
//            {
//                DeleteDopeul = dopeul;
//                compareTime = dopeul.Time;
//                break;
//            }
//            if (!(compareTime <= DateTime.Now))
//            {
//                switch (character.Account.Lang)
//                {
//                    default:
//                        character.SendServerMessage(
//                    $"No puedes reiniciar una pelea contra este jefe, debes esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
//                    Color.PaleVioletRed);
//                        break;
//                }
//                character.LeaveDialog();
//                return false;
//            }
//            else if (compareTime <= DateTime.Now)
//            {
//                if (DeleteDopeul != null)
//                {
//                    character.DoppleCollection.DeleteDopeul.Add(DeleteDopeul);
//                }
//                var monsterGradeId = 1;
//                while (monsterGradeId < 12)
//                {
//                    if (character.Level > monsterGradeId * 20 + 10)
//                        monsterGradeId++;
//                    else
//                        break;
//                }
//                var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(MonsterId, monsterGradeId);
//                var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
//                var monster = new Monster(grade, new MonsterGroup(0, position));

//                var fight = Singleton<FightManager>.Instance.CreatePvBFight(character.Map);
//                fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
//                fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
//                fight.StartPlacement();
//                ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
//                    new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
//                character.SaveLater();
//                return true;
//            }
//            switch (character.Account.Lang)
//            {
//                default:
//                    character.SendServerMessage(
//                $"No puedes reiniciar una pelea contra este jefe, debes esperar <b>{compareTime.Subtract(DateTime.Now).Hours} horas, {compareTime.Subtract(DateTime.Now).Minutes} minutos</b>",
//                Color.PaleVioletRed);
//                    break;
//            }
//            character.LeaveDialog();
//            return false;
//        }
//    }
//}