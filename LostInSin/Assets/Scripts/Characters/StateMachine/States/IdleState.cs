using Cysharp.Threading.Tasks;
using LostInSin.Animation.Data;
using LostInSin.Identifiers;
using LostInSin.Signals;
using LostInSin.Signals.Animations;
using LostInSin.Signals.Characters.States;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters.StateMachine.States
{
    public class IdleState : IState
    {
        [Inject(Id = CharacterStates.UseAbilityState)]
        private IState _useAbilityState;

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
            _signalBus.AbstractFire(new StateChangeSignal(_useAbilityState));
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