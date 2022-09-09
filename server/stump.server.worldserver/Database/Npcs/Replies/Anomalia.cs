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
    [Discriminator("Anomalia", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    class AnomaliaReply : NpcReply
    {
        public AnomaliaReply(NpcReplyRecord record)
            : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {

            string annonce = character.Name + " ha entrado en un bucle temporal para hacer frente a una anomalía!";

            Random random = new Random();
            int r = random.Next(1, 10);

            switch (r)
            {
                case 1:
                    PrimeraAnomalia(character);
                    break;
                case 2:
                    SegundaAnomalia(character);
                    break;
                case 3:
                    TerceraAnomalia(character);
                    break;
                case 4:
                    CuartaAnomalia(character);
                    break;
                case 5:
                    QuintaAnomalia(character);
                    break;
                case 6:
                    SextaAnomalia(character);
                    break;
                case 7:
                    SeptimaAnomalia(character);
                    break;
                case 8:
                    OctabaAnomalia(character);
                    break;
                case 9:
                    NovenaAnomalia(character);
                    break;
                default:
                    DecimaAnomalia(character);
                    break;
            }


            npc.Delete();
            npc.Refresh();
            character.RefreshActor();
            Singleton<World>.Instance.SendAnnounce(annonce, Color.Red);
            character.Record.NumberOfPnjFound++;


            return true;
        }

        //Noximiliano
        private void DecimaAnomalia(Character character)
        {

            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5781, 5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5709, 5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5712, 5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5714, 5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5719, 5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Fab'
        private void NovenaAnomalia(Character character)
        {
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5755,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5720,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5710,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5710,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5718,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Rey Mohino
        private void OctabaAnomalia(Character character)
        {
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5688,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5722,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5716,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5713,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5711,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Perceblando
        private void SeptimaAnomalia(Character character)
        {

            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5687,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5712,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5723,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5710,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5717,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Agonia
        private void SextaAnomalia(Character character)
        {
          
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5684,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5721,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5715,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5711,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5709,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Larva Rushu
        private void QuintaAnomalia(Character character)
        {
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5683,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5709,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5709,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5718,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5721,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Cuervo Negro
        private void CuartaAnomalia(Character character)
        {
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5682,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5713,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5711,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5720,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5715,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Dathura
        private void TerceraAnomalia(Character character)
        {
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5673,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5710,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5712,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5718,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5722,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Julith
        private void SegundaAnomalia(Character character)
        {
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5663,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5711,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5713,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5714,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5720,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

        //Campeon de la aurora purpura
        private void PrimeraAnomalia(Character character)
        {
          
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade(5662,5);
            var position = new ObjectPosition(character.Map, character.Cell, (DirectionsEnum)5);
            var monster = new Monster(grade, new MonsterGroup(0, position));

            var grade1 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5709,5);
            var monster1 = new Monster(grade1, new MonsterGroup(0, position));

            var grade2 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5712,5);
            var monster2 = new Monster(grade2, new MonsterGroup(0, position));

            var grade3 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5721,5);
            var monster3 = new Monster(grade3, new MonsterGroup(0, position));

            var grade4 = Singleton<MonsterManager>.Instance.GetMonsterGrade(5717,5);
            var monster4 = new Monster(grade4, new MonsterGroup(0, position));


            var fight = Singleton<FightManager>.Instance.CreatePvMFight(character.Map);
            fight.ChallengersTeam.AddFighter(character.CreateFighter(fight.ChallengersTeam));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster1));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster2));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster3));
            fight.DefendersTeam.AddFighter(new MonsterFighter(fight.DefendersTeam, monster4));
            fight.StartPlacement();

            //fight.HideBlades();
            //fight.StartFighting();

            ContextHandler.HandleGameFightJoinRequestMessage(character.Client,
            new GameFightJoinRequestMessage(character.Fighter.Id, (ushort)fight.Id));
            character.SaveLater();

            character.DisplayNotification("¡Acabas de entrar a una anomalía!", NotificationEnum.INFORMATION);
        }

    }
}