using LostInSin.Animation.Data;

namespace LostInSin.Signals.Animations
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