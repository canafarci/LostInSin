using System.Collections;
using System.Collections.Generic;
using LostInSin.Animation.Data;
using UnityEngine;

namespace LostInSin.Characters.StateMachine
{
    public class AnimationChangeSignal : IAnimationChangeSignal
    {
        private readonly AnimationStateChangeData _animationChangeData;

        public AnimationStateChangeData AnimationStateChangeData { get { return _animationChangeData; } }

        public AnimationChangeSignal(AnimationStateChangeData animationChangeData)
        {
            _animationChangeData = animationChangeData;
        }
    }
}
