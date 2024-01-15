using LostInSin.Animation.Data;
using LostInSin.Characters.StateMachine;

namespace LostInSin.Signals
{
    public class AnimationChangeSignal : IAnimationChangeSignal
    {
        private readonly AnimationStateChangeData _animationChangeData;

        public AnimationStateChangeData AnimationStateChangeData => _animationChangeData;

        public AnimationChangeSignal(AnimationStateChangeData animationChangeData)
        {
            _animationChangeData = animationChangeData;
        }
    }
}