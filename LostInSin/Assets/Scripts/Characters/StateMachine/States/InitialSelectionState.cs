using LostInSin.Identifiers;
using LostInSin.Signals;
using LostInSin.Signals.Characters.States;
using LostInSin.Signals.Characters.Visuals;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters.StateMachine.States
{
    public class InitialSelectionState : IState
    {
        [Inject(Id = CharacterStates.IdleState)]
        private IState _idleState;

        private SignalBus _signalBus;

        private InitialSelectionState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Enter()
        {
            _signalBus.AbstractFire(new SelectionChangeSignal(true));
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            _signalBus.AbstractFire(new StateChangeSignal(_idleState));
        }
    }
}