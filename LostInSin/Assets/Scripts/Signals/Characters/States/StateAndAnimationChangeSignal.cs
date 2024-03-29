using LostInSin.Animation.Data;
using LostInSin.Characters.StateMachine.States;
using LostInSin.Signals.Animations;

namespace LostInSin.Signals.Characters.States
{
    public class StateAndAnimationChangeSignal : IStateChangeSignal, IAnimationChangeSignal
    {
        private IState _targetState;
        private AnimationStateChangeData _animationChangeData;

        public StateAndAnimationChangeSignal(IState targetState,
                                             AnimationStateChangeData animationChangeData)
        {
            _targetState = targetState;
            _animationChangeData = animationChangeData;
        }

        public IState TargetState => _targetState;
        public AnimationStateChangeData AnimationStateChangeData => _animationChangeData;
    }
}