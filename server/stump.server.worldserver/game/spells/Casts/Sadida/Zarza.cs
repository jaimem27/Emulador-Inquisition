using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.game.spells.Casts.Sadida
{
    [SpellCastHandler(SpellIdEnum.BRAMBLE_183)]
    public class Zarza : DefaultSpellCastHandler
    {
        public Zarza(SpellCastInformations cast) : base(cast)
        {
        }

        public override void Execute()
        {
            var hadlers = Handlers;
            base.Execute();
        }
    }
}