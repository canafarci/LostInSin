using LostInSin.Animation.Data;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using Zenject;

namespace LostInSin.Characters.StateMachine
{
    public class IdleState : IState
    {
        [Inject(Id = CharacterStates.MoveState)] private IState _moveState;
        private SignalBus _signalBus;

        private IdleState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Enter()
        {
            FireRunningAnimationChangeSignal(false);
        }

        public void Exit()
        {
        }

        public void Tick()
        {
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
