using Stump.Core.Mathematics;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.IPC.Messages;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights.Challenges;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightPvD : Fight<FightMonsterTeam, FightPlayerTeam>
    {
        private readonly Dictionary<byte, byte> _doplon = new Dictionary<byte, byte>
        {{20, 1}, {40, 2}, {60, 3}, {80, 5}, {100, 6}, {120, 8}, {140, 10}, {160, 12}, {180, 14}, {200, 17}, {220, 20}};

        private readonly Dictionary<byte, ushort> _doplonKamas = new Dictionary<byte, ushort>
        { {20, 50},{40, 200},{60, 450},{80, 800},{100, 1250},{120, 1800},{140, 2450},{160, 3200},{180, 4050},{200, 5000}, {220, 10000}};

        public FightPvD(int id, Map fightMap, FightMonsterTeam defendersTeam, FightPlayerTeam challengersTeam)
            : base(id, fightMap, defendersTeam, challengersTeam)
        {
        }
        public override bool IsPvP => false;
        public override void StartPlacement()
        {
            base.StartPlacement();

            m_placementTimer = Map.Area.CallDelayed(FightConfiguration.PlacementPhaseTime, StartFighting);
        }

        public override void StartFighting()
        {
            m_placementTimer.Dispose();

            base.StartFighting();
        }


        protected override void OnFighterAdded(FightTeam team, FightActor actor)
        {
            //if (BreedFighter != null && !(team is FightMonsterTeam)) return;
            base.OnFighterAdded(team, actor);
            if (!(team is FightMonsterTeam))
            {
                BreedFighter = actor;
            }
        }

        public FightActor BreedFighter { get; set; }

        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_PvM;
        protected override List<IFightResult> GetResults()
        {
            var list = new List<IFightResult>();
            list.AddRange(
                from entry in GetFightersAndLeavers()
                where !(entry is SummonedFighter)
                select entry.GetFightResult());
            if (Winners.Fighters.Contains(BreedFighter))
            {
                var actor = Losers.Fighters.FirstOrDefault();
                var dopple = actor as MonsterFighter;
                if (dopple != null)
                {
                    var doppleGrade = dopple.Monster.Grade;
                    foreach (
                        var fightPlayerResult in
                            list.OfType<FightPlayerResult>()
                                .Where(
                                    fightPlayerResult =>
                                        fightPlayerResult.Fighter.HasWin() &&
                                        !Leavers.Contains(fightPlayerResult.Fighter)))
                    {
                        Singleton<ItemManager>.Instance.CreateDopeul(((CharacterFighter)fightPlayerResult.Fighter).Character, dopple.Monster.Template.Id);
                        fightPlayerResult.AddEarnedExperience(doppleGrade.GradeXp);
                        fightPlayerResult.Loot.Kamas = _doplonKamas[(byte)doppleGrade.Level];
                        fightPlayerResult.Loot.AddItem(13052, (uint)(_doplon[(byte)doppleGrade.Level] * 2.5));
                        if (dopple.Monster.Template.Id == 166)//Eniripsa
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9083, 1));
                        }
                        if (dopple.Monster.Template.Id == 162)//Anutrof
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9079, 1));
                        }
                        if (dopple.Monster.Template.Id == 4290)//Hippermago
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(17441, 1));
                        }
                        if (dopple.Monster.Template.Id == 160)//Feka
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9077, 1));
                        }
                        if (dopple.Monster.Template.Id == 168)//Ocra
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9085, 1));
                        }
                        if (dopple.Monster.Template.Id == 161)//Osamoda
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9078, 1));
                        }
                        if (dopple.Monster.Template.Id == 2691)//Pandawa
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9088, 1));
                        }
                        if (dopple.Monster.Template.Id == 455)//Sacrogrito
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9087, 1));
                        }
                        if (dopple.Monster.Template.Id == 169)//Sadida
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9086, 1));
                        }
                        if (dopple.Monster.Template.Id == 3976)//Selatrop
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(16051, 1));
                        }
                        if (dopple.Monster.Template.Id == 163)//Sram
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9080, 1));
                        }
                        if (dopple.Monster.Template.Id == 3286)//Steamer
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(13274, 1));
                        }
                        if (dopple.Monster.Template.Id == 3111)//Tymador
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(12242, 1));
                        }
                        if (dopple.Monster.Template.Id == 4777)//Uginak
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(18618, 1));
                        }
                        if (dopple.Monster.Template.Id == 164)//Xelor
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9081, 1));
                        }
                        if (dopple.Monster.Template.Id == 167)//Yopuka
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9084, 1));
                        }
                        if (dopple.Monster.Template.Id == 3132)//Zobal
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(12243, 1));
                        }
                        if (dopple.Monster.Template.Id == 165)//Zurcarak
                        {
                            fightPlayerResult.Loot.AddItem(new DroppedItem(9082, 1));
                        }
                    }
                }



            }
            return list;
        }

        protected override void SendGameFightJoinMessage(CharacterFighter fighter)
        {

            ContextHandler.SendGameFightJoinMessage(fighter.Character.Client, true, true, IsStarted, IsStarted ? 0 : (int)GetPlacementTimeLeft().TotalMilliseconds / 10000, FightType);

        }

        protected override bool CanCancelFight() => false;

        public override TimeSpan GetPlacementTimeLeft()
        {
            var timeleft = FightConfiguration.PlacementPhaseTime - (DateTime.Now - CreationTime).TotalMilliseconds;

            if (timeleft < 0)
                timeleft = 0;

            return TimeSpan.FromMilliseconds(timeleft);
        }

        protected override void OnDisposed()
        {
            if (m_placementTimer != null)
                m_placementTimer.Dispose();

            base.OnDisposed();
        }
    }
}