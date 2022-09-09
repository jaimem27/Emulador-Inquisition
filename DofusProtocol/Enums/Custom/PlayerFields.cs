using System;
using System.Collections.Generic;
using Stump.DofusProtocol.Enums;

namespace Stump.DofusProtocol.Enums
{
    public enum PlayerFields : byte
    {
        Health = 1,
        Initiative = 2,
        Prospecting = 3,
        AP = 4,
        MP = 5,
        Strength = 6,
        Vitality = 7,
        Wisdom = 8,
        Chance = 9,
        Agility = 10,
        Intelligence = 11,
        Range = 12,
        SummonLimit = 13,
        DamageReflection = 14,
        CriticalHit = 15,
        CriticalMiss = 16,
        HealBonus = 17,
        DamageBonus = 18,
        WeaponDamageBonus = 19,
        DamageBonusPercent = 20,
        TrapBonus = 21,
        TrapBonusPercent = 22,
        GlyphBonusPercent,
        RuneBonusPercent,
        PermanentDamagePercent = 23,
        TackleBlock = 24,
        TackleEvade = 25,
        APAttack = 26,
        MPAttack = 27,
        PushDamageBonus = 28,
        CriticalDamageBonus = 29,
        NeutralDamageBonus = 30,
        EarthDamageBonus = 31,
        WaterDamageBonus = 32,
        AirDamageBonus = 33,
        FireDamageBonus = 34,
        DodgeAPProbability = 35,
        DodgeMPProbability = 36,
        NeutralResistPercent = 37,
        EarthResistPercent = 38,
        WaterResistPercent = 39,
        AirResistPercent = 40,
        FireResistPercent = 41,
        NeutralElementReduction = 42,
        EarthElementReduction = 43,
        WaterElementReduction = 44,
        AirElementReduction = 45,
        FireElementReduction = 46,
        PushDamageReduction = 47,
        CriticalDamageReduction = 48,
        PvpNeutralResistPercent = 49,
        PvpEarthResistPercent = 50,
        PvpWaterResistPercent = 51,
        PvpAirResistPercent = 52,
        PvpFireResistPercent = 53,
        PvpNeutralElementReduction = 54,
        PvpEarthElementReduction = 55,
        PvpWaterElementReduction = 56,
        PvpAirElementReduction = 57,
        PvpFireElementReduction = 58,
        GlobalDamageReduction = 59,
        DamageMultiplicator = 60,
        PhysicalDamage = 61,
        MagicDamage = 62,
        PhysicalDamageReduction = 63,
        MagicDamageReduction = 64,
        Weight = 65,
        WaterDamageArmor = 66,
        EarthDamageArmor = 67,
        NeutralDamageArmor = 68,
        AirDamageArmor = 69,
        FireDamageArmor = 70,
        Erosion = 71,
        ComboBonus = 72,
        Shield = 73,
        SpellDamageBonus = 74,
        MeleeDamageDonePercent,
        MeleeDamageReceivedPercent,
        MeeleDamageDonePercent = 75,
        MeeleDamageReceivedPercent = 76,
        RangedDamageDonePercent = 77,
        RangedDamageReceivedPercent = 78,
        WeaponDamageDonePercent = 79,
        WeaponDamageReceivedPercent = 80,
        SpellDamageDonePercent = 81,
        SpellDamageReceivedPercent = 82,
    }
}