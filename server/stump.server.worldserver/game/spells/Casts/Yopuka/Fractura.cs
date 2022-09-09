using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Yopuka {
    [SpellCastHandler (SpellIdEnum.FRACTURE)]
    public class Fractura : DefaultSpellCastHandler {
        public Fractura (SpellCastInformations cast) : base (cast) { }

        public override void Execute () {
            if (!m_initialized)
                Initialize ();

            //Get all fighters in slide cells 
            var mAffectscells = Caster.Position.Point.ManhattanDistanceTo (TargetedCell);
            var mDirection = Caster.Position.Point.OrientationTo (TargetedCell);

            var mActors = Fight.GetAllFightersInLineToSacrier (Caster.Position.Point, (int) mAffectscells, mDirection).Where (x => {
                if (x.IsAlive ())
                    return x != Caster;
                return false;
            }).ToList ();

            //Set Affect Actors
            Handlers[1].SetAffectedActors (mActors);

            //Apply Effects
            Handlers[1].Apply ();
            Handlers[0].Apply ();
        }
    }
}