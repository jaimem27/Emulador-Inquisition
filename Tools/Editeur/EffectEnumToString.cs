using Stump.DofusProtocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editeur
{
    public static class EffectEnumToString
    {

        public static List<string> Convert()
        {
            string[] effectsEnum = Enum.GetNames(typeof(EffectsEnum));
            var effectsfinal = effectsEnum.ToList();
            return effectsfinal;
        }

    }
}
