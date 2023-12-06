using System;
using System.Collections;
using System.Collections.Generic;
using LostInSin.Identifiers;
using UnityEngine;

namespace LostInSin.Animation.Data
{
    public struct AnimationStateChangeData
    {
        private AnimationIdentifier _animationIdentifier;
        private object _animationParameterValue;
        private Type _parameterType;
        public Type ParameterType { get { return _parameterType; } }
        public AnimationIdentifier AnimationID { get { return _animationIdentifier; } }

        // Use a non-generic method to set the animation parameter
        public void SetAnimationParameter<T>(AnimationParameter<T> animationParameter)
        {
            _animationParameterValue = animationParameter;
            _parameterType = animationParameter.Type;
        }

        public void SetAnimationIdentifier(AnimationIdentifier animationIdentifier)
        {
            _animationIdentifier = animationIdentifier;
        }

        // Use a non-generic method to get the animation parameter
        public AnimationParameter<T> GetAnimationParameter<T>()
        {
            return (AnimationParameter<T>)_animationParameterValue;
        }
    }
}
