using LostInSin.Animation.Data;
using LostInSin.Identifiers;
using LostInSin.Signals;
using Zenject;

namespace LostInSin.Animation
{
    public class AnimationChangeSignalBuilder
    {
        private AnimationStateChangeData _animationChangeData = new();
        private AnimationIdentifier _animationIdentifier;

        public AnimationChangeSignalBuilder SetAnimationParameter<T>(T value)
        {
            AnimationParameter<T> animationParameter = new(value);
            _animationChangeData.SetAnimationParameter(animationParameter);
            return this;
        }

        public AnimationChangeSignalBuilder SetAnimationIdentifier(AnimationIdentifier animationIdentifier)
        {
            _animationIdentifier = animationIdentifier;
            return this;
        }

        public AnimationChangeSignal Build()
        {
            _animationChangeData.SetAnimationIdentifier(_animationIdentifier);
            return new AnimationChangeSignal(_animationChangeData);
        }
    }
}