using LostInSin.AbilitySystem;
using LostInSin.Identifiers;
using LostInSin.PlayerInput;
using UnityEngine;
using Zenject;
using Input = UnityEngine.Windows.Input;

namespace LostInSin.Characters.StateMachine.States
{
    public class UseAbilityState : IState
    {
        [Inject(Id = CharacterStates.IdleState)]
        private readonly IState _idleState;

        [Inject] private readonly CharacterStateRuntimeData _runtimeData;
        [Inject] private readonly GameInput _gameInput;

        private StateActivity _stateActivity = StateActivity.Inactive;

        private enum StateActivity
        {
            Active,
            Inactive
        }

        public void Tick()
        {
            if (_stateActivity == StateActivity.Inactive) return;

            //_runtimeData.CanExitState = _abilitySystemManager.CastResult is not AbilityCastResult.InProgress;

            if (_gameInput.GameplayActions.Click.IsPressed())
            {
            }
        }

        public void Enter()
        {
            _stateActivity = StateActivity.Active;
        }

        public void Exit()
        {
            _stateActivity = StateActivity.Inactive;
        }
    }
}