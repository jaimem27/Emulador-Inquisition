using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Fights.Teams.Prism {
    public class FightPrismDefenderTeam : FightTeamWithLeader<PrismFighter> {
        public override TeamTypeEnum TeamType => TeamTypeEnum.TEAM_TYPE_PRISM;

        public FightPrismDefenderTeam (TeamEnum id, Cell[] placementCells) : base (id, placementCells) { }
        public FightPrismDefenderTeam (TeamEnum id, Cell[] placementCells, AlignmentSideEnum alignmentSide) : base (id, placementCells, alignmentSide) { }
        public override FighterRefusedReasonEnum CanJoin (Character character) {
            FighterRefusedReasonEnum result;
            if (!character.IsGameMaster () && character.Guild?.Alliance != Leader.PrismNpc.Alliance)
                result = FighterRefusedReasonEnum.WRONG_ALLIANCE;
            else
                result = base.CanJoin (character);
            return result;
        }
    }
}