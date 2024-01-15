using LostInSin.Animation.Data;

namespace LostInSin.Signals
{
    public interface IAnimationChangeSignal
    {
        public AnimationStateChangeData AnimationStateChangeData { get; }
    }
}