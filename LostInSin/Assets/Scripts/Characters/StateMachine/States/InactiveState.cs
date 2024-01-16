using Cysharp.Threading.Tasks;
using LostInSin.Animation.Data;
using LostInSin.Identifiers;
using LostInSin.Signals;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters.StateMachine.States
{
    public class InactiveState : IState
    {
        [Inject(Id = CharacterStates.InitialSelectionState)]
        private IState _initialSelectionState;

        private SignalBus _signalBus;

        private InactiveState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Enter()
        {
            FireInactiveVisualSignals();
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            _signalBus.AbstractFire(new StateChangeSignal(_initialSelectionState));
        }

        private void FireInactiveVisualSignals()
        {
            _signalBus.AbstractFire(new SelectionChangeSignal(false));

            FireRunningAnimationChangeSignal(false);
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