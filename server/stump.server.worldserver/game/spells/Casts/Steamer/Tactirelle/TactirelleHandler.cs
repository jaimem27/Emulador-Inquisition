using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Handlers.Actions;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Spells.Casts
{
    [SpellCastHandler(3214)]
    public class TactirelleHandler : DefaultSpellCastHandler
    {
        public TactirelleHandler(SpellCastInformations cast)
            : base(cast)
        {
        }

        public object TemporaryBoostStateEffect { get; private set; }

        public override void Execute()
        {
            try
            {
                List<SpellEffectHandler> effects = base.Handlers.ToList();

                if (!this.m_initialized)
                {
                    this.Initialize();
                }
                bool result = false;
                foreach (FightActor actor in Caster.Team.Fighters)
                {
                    foreach (SummonedFighter summon in actor.Summons)
                    {
                        if (summon is SummonedTurret && (summon as SummonedTurret).Monster.Template.Id == 3289)
                        {
                            effects[1].AddAffectedActor(summon);
                            result = true;
                            break;
                        }
                    }
                    if (actor is CharacterFighter && (actor as CharacterFighter).Character.BreedId == DofusProtocol.Enums.PlayableBreedEnum.Steamer)
                    {
                        actor.SpellHistory.ReducCooldown(3214, 3);
                        ActionsHandler.SendGameActionFightSpellCooldownVariationMessage(actor.CharacterContainer.Clients, Caster, actor, new Spell(3214, (byte)Caster.Level), 3);
                    }
                }
                if (!result)
                    effects.RemoveAt(1);

                base.Handlers = effects.ToArray();
                base.Execute();
            }
            catch (System.Exception)
            {


            }

        }
    }
}