using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.Stats;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Prisms;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Actors.Fight {
    public sealed class PrismFighter : AIFighter {
        private readonly StatsFields m_stats;

        public PrismFighter (FightTeam team, PrismNpc prismNpc) : base (team, Enumerable.Empty<Spell> ()) {
            PrismNpc = prismNpc;
            Look = prismNpc.Look.Clone ();
            var grade = Singleton<MonsterManager>.Instance.GetMonsterGrade (3451, 1);
            Monster = new Monster (grade, new MonsterGroup (0, prismNpc.Position.Clone ()));
            m_stats = new StatsFields (this);
            m_stats.Initialize (Monster.Grade);
            Cell cell;
            if (Fight.FindRandomFreeCell (this, out cell, false))
                Position = new ObjectPosition (PrismNpc.Map, cell, PrismNpc.Direction);
        }

        public Monster Monster { get; set; }

        public override int Id => PrismNpc.Id;
        public override string Name => PrismNpc.Alliance.Name;

        public override ObjectPosition MapPosition => PrismNpc.Position;
        //public override bool Vip
        //{
        //    get { return false; }
        //}
        public override ushort Level => (byte) Monster.Grade.Level;

        public override StatsFields Stats => m_stats;
        public PrismNpc PrismNpc { get; set; }

        public override string GetMapRunningFighterName () {
            return Name;
        }
        public override GameContextActorInformations GetGameContextActorInformations (Character character) {
            return GetGameFightFighterInformations ();
        }

        public override GameFightFighterInformations GetGameFightFighterInformations (WorldClient client = null) {
            return new GameFightMonsterInformations (Id, Look.GetEntityLook (), GetEntityDispositionInformations (client),
                (sbyte) Team.Id, 0, IsAlive (), GetGameFightMinimalStats (client), new ushort[0],
                (ushort) Monster.Template.Id, (sbyte) Monster.Grade.GradeId, (short)Monster.Grade.Level);
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations () {
            return new FightTeamMemberMonsterInformations (Id, Monster.Template.Id, (sbyte) Monster.Grade.GradeId);
        }

        public override GameFightFighterLightInformations GetGameFightFighterLightInformations (WorldClient client = null) {
            return new GameFightFighterMonsterLightInformations (false, IsAlive (), Id, 0, (ushort) Level, 0,
                (ushort) Monster.Template.Id);
        }

        public override string ToString () {
            return Monster.ToString ();
        }

        public PrismFightersInformation GetPrismFightersInformation () {
            System.Collections.Generic.IEnumerable<CharacterMinimalPlusLookInformations> arg_81_0;
            if (Fight.State == FightState.Placement && Fight is FightPvMr)
                arg_81_0 =
                from x in ((FightPvMr) Fight).DefendersQueue
            select x.GetCharacterMinimalPlusLookInformations ();
            else
                arg_81_0 =
                from x in Team.Fighters.OfType<CharacterFighter> ()
            //CharacterMinimalAllianceInformations
            select x.Character.GetCharacterMinimalPlusLookInformations ();
            System.Collections.Generic.IEnumerable<CharacterMinimalPlusLookInformations> allyCharactersInformations = arg_81_0;
            return new PrismFightersInformation ((ushort) PrismNpc.SubArea.Id, new ProtectedEntityWaitingForHelpInfo (((FightPvMr) Fight).GetTimeMaxBeforeFightStart () / 100,
                    FightPvMr.PvTAttackersPlacementPhaseTime / 100, 5), allyCharactersInformations.ToArray(), //TEST
                (from x in OpposedTeam.Fighters.OfType<CharacterFighter> () select x.Character.GetCharacterMinimalPlusLookInformations ()).ToArray());
        }

    }
}