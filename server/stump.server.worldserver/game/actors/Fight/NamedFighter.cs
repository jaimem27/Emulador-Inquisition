using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Handlers.Chat;

namespace Stump.Server.WorldServer.Game.Actors.Fight
{
    public abstract class NamedFighter : FightActor, INamedActor
    {
        protected NamedFighter(FightTeam team)
            : base(team)
        {
        }

        public abstract string Name
        {
            get;
        }

        public void Say(string msg)
        {
            ChatHandler.SendChatServerMessage(Fight.Clients, this, ChatActivableChannelsEnum.CHANNEL_GLOBAL, msg);
        }

        public override string GetMapRunningFighterName()
        {
            return Name;
        }

        public virtual Damage CalculateDamageBonuses(Damage damage)
        {
            // formulas :
            // DAMAGE * [(100 + STATS + %BONUS + MULT)/100 + (BONUS + PHS/MGKBONUS + ELTBONUS)]

            var bonusPercent = Stats[PlayerFields.DamageBonusPercent].TotalSafe;
            var mult = Stats[PlayerFields.DamageMultiplicator].Total;
            var bonus = Stats[PlayerFields.DamageBonus].Total;
            var criticalBonus = 0;
            var phyMgkBonus = 0;
            var stats = 0;
            var eltBonus = 0;
            var weaponBonus = 0;
            var spellBonus = 0;

            if (m_isUsingWeapon)
                weaponBonus = Stats[PlayerFields.WeaponDamageBonus].TotalSafe;
            else
                spellBonus = Stats[PlayerFields.SpellDamageBonus].TotalSafe;

            switch (damage.School)
            {
                case EffectSchoolEnum.Neutral:
                    stats = Stats[PlayerFields.Strength].TotalSafe;
                    phyMgkBonus = Stats[PlayerFields.PhysicalDamage].TotalSafe;
                    eltBonus = Stats[PlayerFields.NeutralDamageBonus].Total;
                    break;
                case EffectSchoolEnum.Earth:
                    stats = Stats[PlayerFields.Strength].TotalSafe;
                    phyMgkBonus = Stats[PlayerFields.PhysicalDamage].TotalSafe;
                    eltBonus = Stats[PlayerFields.EarthDamageBonus].Total;
                    break;
                case EffectSchoolEnum.Air:
                    stats = Stats[PlayerFields.Agility].TotalSafe;
                    phyMgkBonus = Stats[PlayerFields.MagicDamage].TotalSafe;
                    eltBonus = Stats[PlayerFields.AirDamageBonus].Total;
                    break;
                case EffectSchoolEnum.Water:
                    stats = Stats[PlayerFields.Chance].TotalSafe;
                    phyMgkBonus = Stats[PlayerFields.MagicDamage].TotalSafe;
                    eltBonus = Stats[PlayerFields.WaterDamageBonus].Total;
                    break;
                case EffectSchoolEnum.Fire:
                    stats = Stats[PlayerFields.Intelligence].TotalSafe;
                    phyMgkBonus = Stats[PlayerFields.MagicDamage].TotalSafe;
                    eltBonus = Stats[PlayerFields.FireDamageBonus].Total;
                    break;
            }

            if (damage.IsCritical)
                criticalBonus += Stats[PlayerFields.CriticalDamageBonus].Total;

            if (damage.MarkTrigger is Glyph)
                bonusPercent += Stats[PlayerFields.GlyphBonusPercent].TotalSafe;

            if (damage.MarkTrigger is Trap)
                bonusPercent += Stats[PlayerFields.TrapBonusPercent].TotalSafe;

            damage.Amount = (int)Math.Max(0, (damage.Amount * (100 + stats + bonusPercent + weaponBonus + spellBonus) / 100d
                                               + (bonus + criticalBonus + phyMgkBonus + eltBonus)) * ((100 + mult) / 100.0));

            return damage;
        }
    }
}