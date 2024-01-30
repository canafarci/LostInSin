using Cysharp.Threading.Tasks;
using LostInSin.Animation;
using LostInSin.Animation.Data;
using LostInSin.Identifiers;
using LostInSin.Signals;
using LostInSin.Signals.Animations;
using LostInSin.Signals.Characters.States;
using LostInSin.Signals.Characters.Visuals;
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
            AnimationChangeSignal animationChangeSignal = new AnimationChangeSignalBuilder()
                                                          .SetAnimationParameter(value)
                                                          .SetAnimationIdentifier(AnimationIdentifier.IsRunning)
                                                          .Build();

            _signalBus.AbstractFire(animationChangeSignal);
        }
    }
}