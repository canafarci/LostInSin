using System;
using UnityEngine;

namespace LostInSin.Animation.Data
{
    public struct AnimationParameter<T>
    {
        public AnimationParameter(T type)
        {
            Value = type;
        }

        public T Value { private set; get; }
        public Type Type { get { return typeof(T); } }
    }
}
