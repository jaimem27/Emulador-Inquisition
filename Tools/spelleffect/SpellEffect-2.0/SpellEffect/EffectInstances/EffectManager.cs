using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellEffect.EffectInstances
{
    class EffectManager
    {
        public static byte[] SerializeEffect(EffectBase effect)
        {
            return effect.Serialize();
        }
        public static byte[] SerializeEffects(IEnumerable<EffectBase> effects)
        {
            var buffer = new List<byte>();

            foreach (var effect in effects)
            {
                buffer.AddRange(effect.Serialize());
            }

            return buffer.ToArray();
        }
        public static EffectBase DeserializeEffect(byte[] buffer, ref int index)
        {
            if (buffer.Length < index)
                throw new Exception("buffer too small to contain an Effect");

            var identifier = buffer[0 + index];
            EffectBase effect;

            switch (identifier)
            {
                case 1:
                    effect = new EffectBase();
                    break;
                case 2:
                    effect = new EffectCreature();
                    break;
                case 3:
                    effect = new EffectDate();
                    break;
                case 4:
                    effect = new EffectDice();
                    break;
                case 5:
                    effect = new EffectDuration();
                    break;
                case 6:
                    effect = new EffectInteger();
                    break;
                case 7:
                    effect = new EffectLadder();
                    break;
                case 8:
                    effect = new EffectMinMax();
                    break;
                case 9:
                    effect = new EffectMount();
                    break;
                case 10:
                    effect = new EffectString();
                    break;
                default:
                    throw new Exception(string.Format("Incorrect identifier : {0}", identifier));
            }

            index++;
            effect.DeSerialize(buffer, ref index);

            return effect;
        }
        public static List<EffectBase> DeserializeEffects(byte[] buffer)
        {
            var result = new List<EffectBase>();

            var i = 0;
            while (i + 1 < buffer.Length)
            {
                result.Add(DeserializeEffect(buffer, ref i));
            }

            return result;
        }

    }
}
