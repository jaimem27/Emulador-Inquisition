using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public class PrestigeManager : Singleton<PrestigeManager>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public const int ItemForBonus = 15711;

        public static ItemTemplate BonusItem;

        [Variable]
        public static short[] PrestigeTitles =
        {
            527, 528, 529, 530, 531, 532, 533, 534, 535, 536
        };

        private static readonly EffectInteger[][] m_prestigesBonusViajero =
        {
            new[] {new EffectInteger(EffectsEnum.Effect_AddDodge, 15)},
            new[] 
            {
                new EffectInteger(EffectsEnum.Effect_AddDodgeAPProbability, 5),
                new EffectInteger(EffectsEnum.Effect_AddDodgeMPProbability, 5)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalHit, 10)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 50),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddDamageBonus, 5)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalDamageBonus, 5)},          
            new[] {new EffectInteger(EffectsEnum.Effect_RangedDamageDonePercent, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_SpellDamageBonusPercent, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddInitiative, 300)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddAP_111, 1)},

        };

        private static readonly EffectInteger[][] m_prestigesBonusValeroso =
        {
            new[] {new EffectInteger(EffectsEnum.Effect_AddDodge, 15)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddDodgeAPProbability, 5),
                new EffectInteger(EffectsEnum.Effect_AddDodgeMPProbability, 5)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalHit, 10)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 50),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddDamageBonus, 5)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalDamageBonus, 5)},
            new[] {new EffectInteger(EffectsEnum.Effect_MeeleDamageDonePercent, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_SpellDamageBonusPercent, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddInitiative, 300)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddAP_111, 1)},

        };

        private static readonly EffectInteger[][] m_prestigesBonusCorazon =
        {
            new[] {new EffectInteger(EffectsEnum.Effect_AddDodge, 15)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddDodgeAPProbability, 5),
                new EffectInteger(EffectsEnum.Effect_AddDodgeMPProbability, 5)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddHealBonus, 20)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 50),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddVitality, 100)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddSummonLimit, 2)},
            new[] {new EffectInteger(EffectsEnum.Effect_RangedResistence, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_SpellDamageBonusPercent, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddRange, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddAP_111, 1)},

        };

        private static readonly EffectInteger[][] m_prestigesBonusMajestuoso =
        {
            new[] {new EffectInteger(EffectsEnum.Effect_AddDodge, 15)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddDodgeAPProbability, 5),
                new EffectInteger(EffectsEnum.Effect_AddDodgeMPProbability, 5)
            },
            new[] {
                new EffectInteger(EffectsEnum.Effect_AddAPAttack, 15),
                new EffectInteger(EffectsEnum.Effect_AddMPAttack, 15)
            },
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 50),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddVitality, 100)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddWisdom, 55)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalHit, 7)},
            new[] {new EffectInteger(EffectsEnum.Effect_SpellDamageBonusPercent, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddRange, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddMP_128, 1)},

        };

        private static readonly EffectInteger[][] m_prestigesBonusRocoso =
        {
            new[] {new EffectInteger(EffectsEnum.Effect_AddLock, 15)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddDodgeAPProbability, 5),
                new EffectInteger(EffectsEnum.Effect_AddDodgeMPProbability, 5)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddVitality, 200)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 50),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },
             new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddDamageReflection_220, 15),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_MeeleResistence, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_RangedResistence, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_SpellResistence, 1)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddAirResistPercent, 3),
                new EffectInteger(EffectsEnum.Effect_AddWaterResistPercent, 3),
                new EffectInteger(EffectsEnum.Effect_AddNeutralResistPercent, 3),
                new EffectInteger(EffectsEnum.Effect_AddFireResistPercent, 3),
                new EffectInteger(EffectsEnum.Effect_AddEarthResistPercent, 3)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddMP_128, 1)},

        };

        private static readonly EffectInteger[][] m_prestigesBonusTramposo =
        {
            new[] {new EffectInteger(EffectsEnum.Effect_AddDodge, 15)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddDodgeAPProbability, 5),
                new EffectInteger(EffectsEnum.Effect_AddDodgeMPProbability, 5)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddWisdom, 100)},
            new[]
            {
                new EffectInteger(EffectsEnum.Effect_AddChance, 50),
                new EffectInteger(EffectsEnum.Effect_AddIntelligence, 50),
                new EffectInteger(EffectsEnum.Effect_AddWisdom, 50),
                new EffectInteger(EffectsEnum.Effect_AddAgility, 50),
                new EffectInteger(EffectsEnum.Effect_AddStrength, 50)
            },
            new[] {new EffectInteger(EffectsEnum.Effect_AddProspecting, 50)},
            new[] {new EffectInteger(EffectsEnum.Effect_IncreaseWeight, 500)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddCriticalHit, 7)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddRange, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddMP_128, 1)},
            new[] {new EffectInteger(EffectsEnum.Effect_AddAP_111, 1)},

        };



        private bool m_disabled;

        public bool PrestigeEnabled
        {
            get { return !m_disabled; }
        }

        [Initialization(typeof(ItemManager), Silent = true)]
        public void Initialize()
        {
            BonusItem = ItemManager.Instance.TryGetTemplate(ItemForBonus);

            if (BonusItem != null)
                return;

            logger.Error("Item {0} not found, prestiges disabled", ItemForBonus);
            m_disabled = true;
        }

        public EffectInteger[] GetPrestigeEffectsViajero(int rank)
        {
            return m_prestigesBonusViajero.Take(rank).SelectMany(x => x.Select(y => (EffectInteger)y.Clone())).ToArray();
        }

        public EffectInteger[] GetPrestigeEffectsValeroso(int rank)
        {
            return m_prestigesBonusValeroso.Take(rank).SelectMany(x => x.Select(y => (EffectInteger)y.Clone())).ToArray();
        }

        public EffectInteger[] GetPrestigeEffectsCorazon(int rank)
        {
            return m_prestigesBonusCorazon.Take(rank).SelectMany(x => x.Select(y => (EffectInteger)y.Clone())).ToArray();
        }

        public EffectInteger[] GetPrestigeEffectsMajestuoso(int rank)
        {
            return m_prestigesBonusMajestuoso.Take(rank).SelectMany(x => x.Select(y => (EffectInteger)y.Clone())).ToArray();
        }

        public EffectInteger[] GetPrestigeEffectsRocoso(int rank)
        {
            return m_prestigesBonusRocoso.Take(rank).SelectMany(x => x.Select(y => (EffectInteger)y.Clone())).ToArray();
        }

        public EffectInteger[] GetPrestigeEffectsTramposo(int rank)
        {
            return m_prestigesBonusTramposo.Take(rank).SelectMany(x => x.Select(y => (EffectInteger)y.Clone())).ToArray();
        }

        public short GetPrestigeTitle(int rank)
        {
            return PrestigeTitles[rank - 1];
        }
    }
}