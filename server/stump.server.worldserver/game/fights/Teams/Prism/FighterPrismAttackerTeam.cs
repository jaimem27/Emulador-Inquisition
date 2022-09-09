using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Fights.Teams.Prism {
    public class FightPrismAttackersTeam : FightTeamWithLeader<CharacterFighter> {
        public override TeamTypeEnum TeamType => TeamTypeEnum.TEAM_TYPE_PLAYER;

        public FightPrismAttackersTeam (TeamEnum id, Cell[] placementCells) : base (id, placementCells) { }
        public FightPrismAttackersTeam (TeamEnum id, Cell[] placementCells, AlignmentSideEnum alignmentSide) : base (id, placementCells, alignmentSide) { }
        public override FighterRefusedReasonEnum CanJoin (Character character) {
            FighterRefusedReasonEnum result;
            if (!character.IsGameMaster () && Fight is FightPvMr &&
                character.Guild?.Alliance == ((FightPvMr) Fight).PrismFighter.PrismNpc.Alliance)
                result = FighterRefusedReasonEnum.WRONG_ALLIANCE;
            else
                result = base.CanJoin (character);
            return result;
        }
    }
}