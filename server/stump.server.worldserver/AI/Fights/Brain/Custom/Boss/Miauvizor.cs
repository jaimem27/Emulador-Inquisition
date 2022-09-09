using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System;
using System.Linq;
using TreeSharp;
using static Stump.Server.WorldServer.Game.Actors.Fight.FightActor;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Boss
{
    [BrainIdentifier(4263)]
    public class Miauvizor : Brain
    {
        public Miauvizor(AIFighter fighter)
            : base(fighter)
        {
            //if(fighter.Map.IsDungeon())
            //    fighter.Fight.FightStarted += Fight_FightStarted;
            //fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6231, 1), Fighter.Cell); //Todo en 1 lag
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6232, 1), Fighter.Cell); // Invu + Dados ?
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6234, 1), Fighter.Cell); // Dados?
            //
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6236, 1), Fighter.Cell); //Nuse
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6240, 1), Fighter.Cell); //Nuse
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6241, 1), Fighter.Cell); // Nuse
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6329, 1), Fighter.Cell); // NUse
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6330, 1), Fighter.Cell); // Invo cartas + Invo dado (1)
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6448, 1), Fighter.Cell); //Marca de los dados miauvizor
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6455, 1), Fighter.Cell); //Marca dados jugadores
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6459, 1), Fighter.Cell); //Marca dados enemigos
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6474, 1), Fighter.Cell); // No se
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6475, 1), Fighter.Cell); //Cura aliados?!
            //Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6497, 1), Fighter.Cell); //Glifo encima solo del miu

        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (Fighter.Cell.Id % 2 == 0)
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6496, 1), Fighter.Cell); //Glifo cartas - Calavera

                }
                else
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.CATSEYE_GAME_6235, 1), Fighter.Cell); // Invo cartas + Invo dado (1)
                }

            }
            
            //LanzaDados();
        }

    }

    [BrainIdentifier(4284)]
    [BrainIdentifier(4286)]
    [BrainIdentifier(4287)]
    [BrainIdentifier(4283)]
    [BrainIdentifier(4285)]
    public class MiauvizorBichos : Brain
    {
        public MiauvizorBichos(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.FightStarted += Fight_FightStarted;
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted(IFight obj, FightActor actor)
        {
            if (Fighter == actor)
            {
                if (!Fighter.HasState((int)SpellIdEnum.THE_WHEETURN))
                {
                    Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.THE_WHEETURN, 1), Fighter.Cell);

                }

            }
        }


        private void Fight_FightStarted(IFight obj)
        {
            //Fighter.Stats[PlayerFields.SummonLimit].Additional = 9999;
            Fighter.CastAutoSpell(new Spell((int)SpellIdEnum.THE_WHEETURN, 1), Fighter.Cell);

        }
    }


}