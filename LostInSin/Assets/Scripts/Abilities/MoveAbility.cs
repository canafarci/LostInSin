using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Core;
using LostInSin.Grid;
using LostInSin.Grid.Data;
using LostInSin.Identifiers;
using LostInSin.PlayerInput;
using LostInSin.Raycast;
using LostInSin.Signals.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Move", menuName = "LostInSin/Abilities/Move", order = 0)]
    public class MoveAbility : AbilityBlueprint
    {
        [Inject] private readonly IGridPositionConverter _gridPositionConverter;
        [Inject] private readonly IPositionRaycaster _positionRaycaster;
        [Inject] private readonly GameInput _gameInput;
        [Inject] private readonly PointerOverUIChecker _pointerOverUIChecker;

        private GridCellData _gridCell;
        private AbilityState _state = AbilityState.Inactive;
        private AbilityTarget _target = default;
        private Character _instigator;

        private enum AbilityState
        {
            Inactive,
            SelectingTarget,
            SelectedTarget
        }

        public override void Initialize()
        {
            _gameInput.GameplayActions.Click.performed += OnClickPerformed;
        }

        private async void OnClickPerformed(InputAction.CallbackContext context)
        {
            if (_state is AbilityState.Inactive or AbilityState.SelectedTarget ||
                _pointerOverUIChecker.PointerIsOverUI) return;

            if (_positionRaycaster.GetWorldPosition(out Vector3 position) && await CanMovePlayer(_instigator, position))
            {
                FireRunningAnimationChangeSignal(_instigator, true);
                _target.Point = position;
                _state = AbilityState.SelectedTarget;
            }
        }

        public override UniTask<bool> CanCast(Character instigator, CancellationToken cancellationToken)
        {
            _instigator = instigator;
            return new UniTask<bool>(_instigator != null);
        }

        public override async UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(
            Character instigator,
            CancellationToken cancellationToken)
        {
            try
            {
                AbilityCastResult castResult = AbilityCastResult.Fail;
                _state = AbilityState.SelectingTarget;

                await UniTask.WaitUntil(() => _state == AbilityState.SelectedTarget,
                                        cancellationToken: cancellationToken);

                castResult = AbilityCastResult.InProgress;

                return (castResult, _target);
            }
            catch (Exception)
            {
                FireRunningAnimationChangeSignal(instigator, false);
                _state = AbilityState.Inactive;
                return (AbilityCastResult.Fail, default);
            }
        }

        public override async UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            instigator.Mover.InitializeMovement(_gridCell.CenterPosition);
            instigator.RuntimeData.ChangeOccupiedCell(_gridCell);

            _state = AbilityState.SelectingTarget;
            await UniTask.WaitWhile(() => !instigator.Mover.Move());

            _state = AbilityState.Inactive;
            return AbilityCastResult.Success;
        }

        public override UniTask<AbilityCastResult> PostCast(Character instigator)
        {
            FireRunningAnimationChangeSignal(instigator, false);
            return new UniTask<AbilityCastResult>(AbilityCastResult.Success);
        }

        private async UniTask<bool> CanMovePlayer(Character instigator, Vector3 position)
        {
            bool canMove = false;

            if (await IsMovePossibleAsync(instigator))
                canMove = TryMoveCharacter(position);

            return canMove;
        }

        private async UniTask<bool> IsMovePossibleAsync(Character instigator)
        {
            await UniTask.NextFrame(); // Wait one frame for state ticker to check
            return !instigator.Mover.IsMoving;
        }

        private bool TryMoveCharacter(Vector3 position)
        {
            bool canMove = false;

            if (TryGetTargetGridCell(out GridCellData gridCell, position))
            {
                _gridCell = gridCell;
                canMove = true;
            }

            return canMove;
        }

        private bool TryGetTargetGridCell(out GridCellData gridCell, Vector3 position)
        {
            gridCell = default;
            return _gridPositionConverter.GetCell(position, out gridCell) &&
                   !gridCell.IsOccupied;
        }

        public override void OnAbilityDeselected(Character instigator)
        {
            FireRunningAnimationChangeSignal(instigator, false);
            _state = AbilityState.Inactive;
            _target = default;
        }

        private void FireRunningAnimationChangeSignal(Character instigator, bool value)
        {
            AnimationChangeSignal animationChangeSignal = new AnimationChangeSignalBuilder()
                                                          .SetAnimationParameter(value)
                                                          .SetAnimationIdentifier(AnimationIdentifier.IsRunning)
                                                          .Build();

            instigator.SignalBus.AbstractFire(animationChangeSignal);
        }
    }
}