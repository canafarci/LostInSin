using LostInSin.Animation.Data;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters.StateMachine
{
    public class WaitState : IState
    {
        [Inject(Id = CharacterStates.MoveState)] private IState _moveState;
        private SignalBus _signalBus;
        private float _waitDuration;

        private WaitState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Enter()
        {
            _waitDuration = Random.Range(0f, 0.1f);

            FireRunningAnimationChangeSignal(false);
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            CheckTransition();

            _waitDuration -= Time.deltaTime;
        }

        private void CheckTransition()
        {
            if (_waitDuration <= 0f)
                _signalBus.AbstractFire(new StateChangeSignal(_moveState));
        }

        private void FireRunningAnimationChangeSignal(bool value)
        {
            AnimationChangeSignal animationChangeSignal = CreateRunningAnimationSignal(value);
            _signalBus.AbstractFire(animationChangeSignal);
        }

        private AnimationChangeSignal CreateRunningAnimationSignal(bool value)
        {
            AnimationStateChangeData animationChangeData = new();
            AnimationParameter<bool> animationParameter = new(value);

            animationChangeData.SetAnimationParameter(animationParameter);
            animationChangeData.SetAnimationIdentifier(AnimationIdentifier.IsRunning);

            AnimationChangeSignal animationChangeSignal = new(animationChangeData);
            return animationChangeSignal;
        }
    }
}
