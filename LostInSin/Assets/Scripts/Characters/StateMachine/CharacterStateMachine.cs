using System;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using UniRx;
using Zenject;

namespace LostInSin.Characters.StateMachine
{
    public class CharacterStateMachine : ITickable, IInitializable, IDisposable
    {
        private IState _currentState;
        private SignalBus _signalBus;
        readonly private CompositeDisposable _disposables = new();

        private CharacterStateMachine(SignalBus signalBus,
                                      [Inject(Id = CharacterStates.WaitState)] IState waitState)
        {
            _currentState = waitState;
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
    }
}