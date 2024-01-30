using LostInSin.Animation.Data;

namespace LostInSin.Signals.Animations
{
    public interface IAnimationChangeSignal
    {
        public AnimationStateChangeData AnimationStateChangeData { get; }
    }
}