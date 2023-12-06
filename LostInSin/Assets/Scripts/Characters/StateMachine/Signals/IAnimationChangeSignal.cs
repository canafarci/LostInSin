using System;
using System.Collections;
using System.Collections.Generic;
using LostInSin.Animation.Data;
using LostInSin.Identifiers;
using UnityEngine;

namespace LostInSin.Characters.StateMachine
{
    public interface IAnimationChangeSignal
    {
        public AnimationStateChangeData AnimationStateChangeData { get; }
    }
}
