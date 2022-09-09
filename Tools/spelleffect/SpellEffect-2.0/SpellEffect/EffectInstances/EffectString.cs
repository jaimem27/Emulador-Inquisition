﻿using Stump.DofusProtocol.Enums;
using System;

namespace SpellEffect.EffectInstances
{
    [Serializable]
    public class EffectString : EffectBase
    {
        protected string m_value;

        public EffectString()
        {
            
        }

        public EffectString(EffectString copy)
            : this(copy.Id, copy.m_value, copy)
        {
            
        }

        public EffectString(short id, string value, EffectBase effect)
            : base(id, effect)
        {
            m_value = value;
        }

        public EffectString(EffectsEnum id, string value)
            : this((short) id, value, new EffectBase())
        {
        }

        public override int ProtocoleId
        {
            get { return 74; }
        }

        public override byte SerializationIdenfitier
        {
            get
            {
                return 10;
            }
        }

        public string Text => m_value;

        public override object[] GetValues()
        {
            return new object[] {m_value};
        }

        public override EffectBase GenerateEffect(EffectGenerationContext context, EffectGenerationType type = EffectGenerationType.Normal)
        {
            return new EffectString(this);
        }
        protected override void InternalSerialize(ref System.IO.BinaryWriter writer)
        {
            base.InternalSerialize(ref writer);

            writer.Write(m_value);
        }

        protected override void InternalDeserialize(ref System.IO.BinaryReader reader)
        {
            base.InternalDeserialize(ref reader);

            m_value = reader.ReadString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EffectString))
                return false;
            return base.Equals(obj) && m_value == (obj as EffectString).m_value;
        }

        public static bool operator ==(EffectString a, EffectString b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object) a == null) || ((object) b == null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EffectString a, EffectString b)
        {
            return !(a == b);
        }

        public bool Equals(EffectString other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Equals(other.m_value, m_value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (m_value != null ? m_value.GetHashCode() : 0);
            }
        }
    }
}