using System;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using UniRx;
using Zenject;

namespace LostInSin.Characters.StateMachine
{
    public class CharacterStateMachine : IStateTicker, IInitializable, IDisposable
    {
        private IState _currentState;
        private IState _inactiveState;
        private SignalBus _signalBus;
        readonly private CompositeDisposable _disposables = new();

        private CharacterStateMachine(SignalBus signalBus,
                                      [Inject(Id = CharacterStates.InactiveState)] IState inactiveState)
        {
            _inactiveState = inactiveState;
            _currentState = _inactiveState;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.GetStream<IStateChangeSignal>()
                      .Subscribe(x => TransitionState(x.TargetState))
                      .AddTo(_disposables);
        }

        public void Tick()
        {
            _currentState.Tick();
        }

        private void TransitionState(IState nextState)
        {
            _currentState.Exit();
            nextState.Enter();
            _currentState = nextState;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void SwitchToInactiveState()
        {
            _currentState.Exit();
            _currentState = _inactiveState;
            _currentState.Enter();
        }
    }
}
