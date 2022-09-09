using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.History;
using Stump.Server.WorldServer.Handlers.Actions;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Sram
{
    [SpellCastHandler(SpellIdEnum.DOUBLE_74)]
    public class DoubleCastHandler : DefaultSpellCastHandler
    {
        public DoubleCastHandler(SpellCastInformations informations)
            : base(informations)
        {
        }

        public override void Execute()
        {
            Spell.Template.VerboseCast = true;
            base.Execute();

            Caster.SpellHistory.RegisterCastedSpell(new SpellHistoryEntry(Caster.SpellHistory, Spell.CurrentSpellLevel,
                Caster, Caster, Fight.TimeLine.RoundNumber, (int?) Spell.CurrentSpellLevel.MinCastInterval));
            ActionsHandler.SendGameActionFightSpellCooldownVariationMessage(Caster.Fight.Clients, Caster, Caster, Spell, (short) Spell.CurrentSpellLevel.MinCastInterval);
        }
    }
    
    [SpellCastHandler(SpellIdEnum.PLOTTER)]
    public class PlotterCastHandler : DefaultSpellCastHandler
    {
        public PlotterCastHandler(SpellCastInformations informations)
            : base(informations)
        {
        }

        public override void Execute()
        {
            Spell.Template.VerboseCast = true;
            base.Execute();

            Caster.SpellHistory.RegisterCastedSpell(new SpellHistoryEntry(Caster.SpellHistory, Spell.CurrentSpellLevel,
                Caster, Caster, Fight.TimeLine.RoundNumber, (int?) Spell.CurrentSpellLevel.MinCastInterval));
            ActionsHandler.SendGameActionFightSpellCooldownVariationMessage(Caster.Fight.Clients, Caster, Caster, Spell, (short) Spell.CurrentSpellLevel.MinCastInterval);
        }
    }
}