using LostInSin.Abilities;
using LostInSin.Identifiers;
using LostInSin.PlayerInput;
using Zenject;

namespace LostInSin.Characters.StateMachine.States
{
    public class UseAbilityState : IState
    {
        [Inject(Id = CharacterStates.IdleState)]
        private readonly IState _idleState;

        [Inject] private readonly CharacterStateRuntimeData _runtimeData;
        [Inject] private readonly GameInput _gameInput;
        [Inject] private readonly AbilityExecutor _abilityExecutor;

        private StateActivity _stateActivity = StateActivity.Inactive;

        private enum StateActivity
        {
            Active,
            Inactive
        }

        public void Tick()
        {
            if (_stateActivity == StateActivity.Inactive) return;

            AbilityCastResult abilityCastResult = _abilityExecutor.Tick();

            _runtimeData.CanExitState = abilityCastResult is not AbilityCastResult.InProgress;
        }

        public void Enter()
        {
            _stateActivity = StateActivity.Active;
            _abilityExecutor.StartExecutingAbility();
        }

        public void Exit()
        {
            _stateActivity = StateActivity.Inactive;
        }
    }
}