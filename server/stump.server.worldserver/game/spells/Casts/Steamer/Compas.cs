using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Steamer
{
    [SpellCastHandler(SpellIdEnum.COMPASS_9872)]
    public class Compas : DefaultSpellCastHandler
    {
        public Compas(SpellCastInformations cast) : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();
            var target = Fight.GetOneFighter(TargetedCell);

            if (target != null)
            {
                var cellSecondTarget = Handlers[0].AffectedCells[1];
                var secondTarget = Fight.GetOneFighter(cellSecondTarget);
                if (secondTarget != null && secondTarget.CanSwitchPos())
                    target.ExchangePositions(secondTarget);
            }

            base.Execute();
        }
    }
}