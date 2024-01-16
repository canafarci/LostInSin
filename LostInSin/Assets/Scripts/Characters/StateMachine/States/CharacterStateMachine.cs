using System;
using LostInSin.Identifiers;
using LostInSin.Signals;
using UniRx;
using Zenject;

namespace LostInSin.Characters.StateMachine.States
{
    public class CharacterStateMachine : IStateTicker, IInitializable, IDisposable
    {
        [Inject] private CharacterStateRuntimeData _runtimeData;

        private IState _currentState;
        private IState _inactiveState;
        private SignalBus _signalBus;
        private readonly CompositeDisposable _disposables = new();

        private CharacterStateMachine(SignalBus signalBus,
                                      [Inject(Id = CharacterStates.InactiveState)]
                                      IState inactiveState)
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

            _signalBus.GetStream<SelectedAbilityChangedSignal>()
                      .Subscribe(OnSelectedAbilityChanged)
                      .AddTo(_disposables);
        }

        private void OnSelectedAbilityChanged(SelectedAbilityChangedSignal signal)
        {
            if (!_runtimeData.IsTicking || _runtimeData.CanExitState) return;
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