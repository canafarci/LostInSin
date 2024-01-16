using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LostInSin.Abilities;
using LostInSin.AbilitySystem;
using LostInSin.Identifiers;
using LostInSin.Input;
using LostInSin.Raycast;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Characters.StateMachine.States
{
    public class UseAbilityState : IState, IInitializable
    {
        [Inject(Id = CharacterStates.IdleState)]
        private readonly IState _idleState;

        [Inject] private readonly AbilitySystemManager _abilitySystemManager;
        [Inject] private readonly CharacterStateRuntimeData _runtimeData;
        [Inject] private readonly GameInput _gameInput;
        [Inject] private readonly IPositionRaycaster _positionRaycaster;

        private enum StateActivity
        {
            Active,
            Inactive
        }

        private StateActivity _stateActivity = StateActivity.Inactive;

        public void Initialize()
        {
            _gameInput.GameplayActions.Click.performed += OnClickPerformed;
        }

        private async void OnClickPerformed(InputAction.CallbackContext context)
        {
            await UniTask.NextFrame(); //wait one frame as character can be switched

            if (_stateActivity == StateActivity.Inactive || _abilitySystemManager.Ability == null) return;

            AbilityInfo abilityInfo = _abilitySystemManager.Ability;

            if (abilityInfo.IsPointTargeted)
                if (_positionRaycaster.GetWorldPosition(out Vector3 position))
                    await _abilitySystemManager.CastAbility(new AbilityTarget() { Point = position });
        }

        public void Tick()
        {
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