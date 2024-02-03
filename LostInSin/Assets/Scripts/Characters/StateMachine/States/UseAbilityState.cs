using LostInSin.AbilitySystem;
using LostInSin.Identifiers;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters.StateMachine.States
{
    public class UseAbilityState : IState
    {
        [Inject(Id = CharacterStates.IdleState)]
        private readonly IState _idleState;

        [Inject] private readonly AbilitySystemManager _abilitySystemManager;
        [Inject] private readonly CharacterStateRuntimeData _runtimeData;

        private StateActivity _stateActivity = StateActivity.Inactive;

        private enum StateActivity
        {
            Active,
            Inactive
        }

        public void Tick()
        {
            if (_stateActivity == StateActivity.Active)
                _runtimeData.CanExitState = _abilitySystemManager.CastResult is not AbilityCastResult.InProgress;
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