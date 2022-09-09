using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Fights.Teams.Prism;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Prisms;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.WorldServer.Handlers.Prism;

namespace Stump.Server.WorldServer.Game.Fights {
    public class FightPvMr : Fight<FightPrismDefenderTeam, FightPrismAttackersTeam> {
#pragma warning disable CS0108 // 'FightPvMr.logger' oculta el miembro heredado 'Fight<FightPrismDefenderTeam, FightPrismAttackersTeam>.logger'. Use la palabra clave new si su intenci�n era ocultarlo.
        private static readonly Logger logger = LogManager.GetCurrentClassLogger ();
#pragma warning restore CS0108 // 'FightPvMr.logger' oculta el miembro heredado 'Fight<FightPrismDefenderTeam, FightPrismAttackersTeam>.logger'. Use la palabra clave new si su intenci�n era ocultarlo.

        [Variable]
        public static int PvTAttackersPlacementPhaseTime = 150000;

        [Variable]
        public static int PvTDefendersPlacementPhaseTime = 20000;

        private readonly Dictionary<FightActor, Map> m_defendersMaps = new Dictionary<FightActor, Map> ();
        private readonly List<Character> m_defendersQueue = new List<Character> ();
        private bool m_isAttackersPlacementPhase;
        public FightPvMr (int id, Map fightMap, FightPrismDefenderTeam blueTeam, FightPrismAttackersTeam redTeam) : base (id, fightMap, blueTeam, redTeam) { }
        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_PvPr;
        public override bool IsPvP => true;
        public FightPrismAttackersTeam AttackersTeam => (FightPrismAttackersTeam) ChallengersTeam;

        public FightPrismDefenderTeam DefendersTeamm => (FightPrismDefenderTeam) DefendersTeam;
        public ReadOnlyCollection<Character> DefendersQueue => m_defendersQueue.AsReadOnly ();

        public bool IsAttackersPlacementPhase {
            get {
                return m_isAttackersPlacementPhase && (State == FightState.Placement || State == FightState.NotStarted);
            }
            set { m_isAttackersPlacementPhase = value; }
        }

        public bool IsDefendersPlacementPhase {
            get {
                return !m_isAttackersPlacementPhase && (State == FightState.Placement || State == FightState.NotStarted);
            }
            set { m_isAttackersPlacementPhase = !value; }
        }
        protected override List<IFightResult> GetResults () {
            var results = new List<IFightResult> ();
            return results;
        }

        protected override void SendGameFightJoinMessage (CharacterFighter fighter) {
            var timeMaxBeforeFightStart = (int) GetPlacementTimeLeft (fighter).TotalMilliseconds / 100;
            ContextHandler.SendGameFightJoinMessage (fighter.Character.Client, CanCancelFight (),
                (fighter.Team == AttackersTeam && IsAttackersPlacementPhase) ||
                (fighter.Team == DefendersTeamm && IsDefendersPlacementPhase), IsStarted, timeMaxBeforeFightStart,
                FightType);
        }

        protected override void SendGameFightSpectatorJoinMessage (FightSpectator spectator) {
            ContextHandler.SendGameFightSpectatorJoinMessage (spectator.Character.Client, this);
        }
        protected override bool CanCancelFight () {
            return false;
        }

        public override void StartFighting () {
            m_placementTimer?.Dispose ();
            PrismFighter.PrismNpc.Alliance.Clients.Send (
                new PrismFightRemovedMessage ((ushort) PrismFighter.PrismNpc.SubArea.Id));
            PrismHandler.SendPrismsListUpdateMessage (PrismFighter.PrismNpc.Alliance.Clients, PrismFighter.PrismNpc, true);
            base.StartFighting ();
        }
        public override void StartPlacement () {
            base.StartPlacement ();
            m_isAttackersPlacementPhase = true;
            PrismHandler.SendPrismsListUpdateMessage (PrismFighter.PrismNpc.Alliance.Clients, PrismFighter.PrismNpc, true);
            PrismFighter.PrismNpc.Alliance.SendPrismsInfoValidMessage ();
            m_placementTimer = Map.Area.CallDelayed (PvTAttackersPlacementPhaseTime, StartDefendersPlacement);
        }

        public void StartDefendersPlacement () {
            if (State != FightState.Placement)
                return;
            m_placementTimer.Dispose ();
            m_placementTimer = null;
            m_isAttackersPlacementPhase = false;
            if (DefendersQueue.Count == 0)
                StartFighting ();
            foreach (var character in DefendersQueue) {
                var defender = character;
                m_defendersMaps.Add (character.Fighter, defender.Map);
                //  defender.Teleport(defender.Map, defender.Map.Cells[defender.Cell.Id]);
                var defender1 = defender;
                defender.Area.ExecuteInContext (() => {
                    defender1.Teleport (Map, defender1.Cell);
                    defender1.ResetDefender ();
                    Map.Area.ExecuteInContext (() => {
                        DefendersTeamm.AddFighter (defender.CreateFighter (DefendersTeamm));
                        if (!DefendersQueue.All (
                                x => DefendersTeamm.Fighters.OfType<CharacterFighter> ().Any (y => y.Character == x)))
                            return;
                        m_placementTimer = Map.Area.CallDelayed (PvTDefendersPlacementPhaseTime,
                            StartFighting);
                    });
                });
            }
        }

        public FighterRefusedReasonEnum AddDefender (Character character) {
            FighterRefusedReasonEnum result;
            if (character.TaxCollectorDefendFight != null)
                result = FighterRefusedReasonEnum.IM_OCCUPIED;
            else {
                if (!IsAttackersPlacementPhase)
                    result = FighterRefusedReasonEnum.TOO_LATE;
                else {
                    if (character.Guild == null || character.Guild?.Alliance != PrismFighter.PrismNpc.Alliance)
                        result = FighterRefusedReasonEnum.WRONG_ALLIANCE;
                    else {
                        if (m_defendersQueue.Count >= 6)
                            result = FighterRefusedReasonEnum.TEAM_FULL;
                        else {
                            if (m_defendersQueue.Any (x => x.Client.IP == character.Client.IP))
                                result = FighterRefusedReasonEnum.MULTIACCOUNT_NOT_ALLOWED;
                            else {
                                if (m_defendersQueue.Contains (character))
                                    result = FighterRefusedReasonEnum.MULTIACCOUNT_NOT_ALLOWED;
                                else {
                                    m_defendersQueue.Add (character);
                                    character.SetDefender (this);
                                    PrismHandler.SendPrismFightDefenderAddMessage (
                                        PrismFighter.PrismNpc.Alliance.Clients, PrismFighter.PrismNpc.SubArea, this,
                                        character);
                                    result = FighterRefusedReasonEnum.FIGHTER_ACCEPTED;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool RemoveDefender (Character character) {
            bool result;
            if (!m_defendersQueue.Remove (character))
                result = false;
            else {
                character.ResetPrismDefender ();
                PrismHandler.SendPrismFightDefenderRemoveMessage (
                    PrismFighter.PrismNpc.Alliance.Clients, PrismFighter.PrismNpc.SubArea, this,
                    character);
                result = true;
            }
            return result;
        }

        public TimeSpan GetPlacementTimeLeft (FightActor fighter) {
            TimeSpan result;
            if (State == FightState.NotStarted && fighter.Team == AttackersTeam)
                result = TimeSpan.FromMilliseconds (PvTAttackersPlacementPhaseTime);
            else {
                if (fighter.Team == DefendersTeamm && m_placementTimer == null)
                    result = TimeSpan.FromMilliseconds (PvTDefendersPlacementPhaseTime);
                else {
                    if ((fighter.Team == AttackersTeam && IsAttackersPlacementPhase) ||
                        (fighter.Team == DefendersTeamm && IsDefendersPlacementPhase))
                        result = m_placementTimer.NextTick - DateTime.Now;
                    else
                        result = TimeSpan.Zero;
                }
            }
            return result;
        }
        public override bool CanChangePosition (FightActor fighter, Cell cell) => base.CanChangePosition (fighter, cell) &&
        ((IsAttackersPlacementPhase && fighter.Team == AttackersTeam) || (IsDefendersPlacementPhase && fighter.Team == DefendersTeamm));

        protected override void OnFighterAdded (FightTeam team, FightActor actor) {
            var prism = actor as PrismFighter;
            if (prism != null) {
                if (PrismFighter != null)
                    logger.Error ("There is already a tax collector in this fight !");
                else {
                    PrismFighter = prism;
                    PrismFighter.PrismNpc.Alliance.Clients.Send (new PrismFightAddedMessage (PrismFighter.GetPrismFightersInformation ()));
                }
            }
            if (State == FightState.Placement && team == AttackersTeam) {
                var fighter = actor as CharacterFighter;
                if (fighter != null)
                    PrismHandler.SendPrismFightAttackerAddMessage (
                        PrismFighter.PrismNpc.Alliance.Clients, PrismFighter.PrismNpc.SubArea, this,
                        fighter.Character);
            }
            base.OnFighterAdded (team, actor);
        }

        protected override void OnFighterRemoved (FightTeam team, FightActor actor) {
            if (State == FightState.Placement && team == AttackersTeam && actor is CharacterFighter) {
                PrismHandler.SendPrismFightAttackerRemoveMessage (
                    PrismFighter.PrismNpc.Alliance.Clients, PrismFighter.PrismNpc.SubArea, this,
                    ((CharacterFighter) actor).Character);
            }
            var taxCollectorFighter = actor as TaxCollectorFighter;
            if (taxCollectorFighter != null && taxCollectorFighter.IsAlive ()) {
                taxCollectorFighter.TaxCollectorNpc.RejoinMap ();
            }
            base.OnFighterRemoved (team, actor);
        }
        protected override void DeterminsWinners () {
            if (DefendersTeamm.AreAllDead () && !ChallengersTeam.AreAllDead ()) {
                Winners = ChallengersTeam;
                Losers = ChallengersTeam;
                Draw = false;
            } else {
                if (!ChallengersTeam.AreAllDead () && ChallengersTeam.AreAllDead ()) {
                    Winners = ChallengersTeam;
                    Losers = ChallengersTeam;
                    Draw = false;
                } else {
                    Draw = true;
                }
            }
            OnWinnersDetermined (Winners, Losers, Draw);
        }
        protected override void OnWinnersDetermined (FightTeam winners, FightTeam losers, bool draw) {
            //TaxCollectorHandler.SendTaxCollectorAttackedResultMessage(TaxCollector.TaxCollectorNpc.Guild.Clients,
            //    Winners != DefendersTeam && !draw, TaxCollector.TaxCollectorNpc);
            if (Winners == DefendersTeam || draw) {
                PrismFighter.PrismNpc.IsWeakened = false;
                using (var enumerator = m_defendersQueue.GetEnumerator ()) {
                    while (enumerator.MoveNext ()) {
                    var current = enumerator.Current.Fighter;
                    if (current != null && m_defendersMaps.ContainsKey (current)) {
                    current.NextMap = m_defendersMaps[current];
                        }
                    }
                    goto IL_C4;
                }
            }
            PrismFighter.PrismNpc.IsWeakened = true;
            var hour = PrismFighter.PrismNpc.Date.Hour;
            var minute = PrismFighter.PrismNpc.Date.Minute;
            var time = DateTime.Now.AddDays (-1); //EDITLATER!!!!!!
            if (hour > time.Hour) {
                time = time.AddHours (hour - time.Hour);
                if (minute > time.Minute)
                    time = time.AddHours (minute - time.Minute);
            }
            PrismFighter.PrismNpc.NextDate = time;
            //Add to task queue
            KoHManager.Instance.AddSubAreaInConflictAsync (PrismFighter.PrismNpc);
            IL_C4:
                PrismFighter.PrismNpc.RejoinMap ();
            base.OnWinnersDetermined (winners, losers, draw);
        }
        /*protected override void OnWinnersDetermined(FightTeam winners, FightTeam losers, bool draw)
        {
            // TaxCollectorHandler.SendTaxCollectorAttackedResultMessage(TaxCollector.TaxCollectorNpc.Guild.Clients,
                // Winners != DefendersTeam && !draw, TaxCollector.TaxCollectorNpc); 
            Console.WriteLine("Chegou aki");
            if (Winners == DefendersTeamm || draw)
                PrismFighter.PrismNpc.IsWeakened = false;
            Console.WriteLine("Chegou aki2");

            foreach (var defender in DefendersTeamm.Fighters.Where(defender => m_defendersMaps.ContainsKey(defender)).OfType<CharacterFighter>())
            {
                if(defender != null )
                defender.Character.NextMap = m_defendersMaps[defender];
            }
            Console.WriteLine("Chegou aki3");
            foreach (var defender in DefendersQueue)
            {
                defender.ResetDefender();
                goto IL_C4;

            }
            Console.WriteLine("Chegou aki4");
            PrismFighter.PrismNpc.IsWeakened = true;
            var hour = PrismFighter.PrismNpc.Date.Hour;
            var minute = PrismFighter.PrismNpc.Date.Minute;
            var time = DateTime.Now.AddDays(-1); //EDITLATER!!!!!!
            if (hour > time.Hour)
            {
                time = time.AddHours(hour - time.Hour);
                if (minute > time.Minute)
                    time = time.AddHours(minute - time.Minute);
            }
            PrismFighter.PrismNpc.NextDate = time;
            //Add to task queue
            KoHManager.Instance.AddSubAreaInConflictAsync(PrismFighter.PrismNpc);
            IL_C4:
            PrismFighter.PrismNpc.RejoinMap();
            Console.WriteLine("Chegou aki5");
            base.OnWinnersDetermined(winners, losers, draw);
           
        } */

        public PrismFighter PrismFighter { get; set; }

        public int GetTimeMaxBeforeFightStart () {
            return (int) TimeSpan.FromMilliseconds (PvTAttackersPlacementPhaseTime).TotalMilliseconds;
        }
    }
}