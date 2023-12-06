
using System;
using LostInSin.Animation.Data;
using LostInSin.Identifiers;

namespace LostInSin.Characters.StateMachine.Signals
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

        public IState TargetState { get { return _targetState; } }
        public AnimationStateChangeData AnimationStateChangeData { get { return _animationChangeData; } }

    }
}
