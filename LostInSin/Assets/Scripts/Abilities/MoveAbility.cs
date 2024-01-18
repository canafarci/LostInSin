using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Core;
using LostInSin.Grid;
using LostInSin.Grid.Data;
using LostInSin.Identifiers;
using LostInSin.Raycast;
using LostInSin.Signals;
using UnityEngine;
using Zenject;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Move", menuName = "LostInSin/Abilities/Move", order = 0)]
    public class MoveAbility : AbilityBlueprint
    {
        [Inject] private readonly IGridPositionConverter _gridPositionConverter;
        [Inject] private readonly IPositionRaycaster _positionRaycaster;

        private GridCellData _gridCell;

        public override async UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(Character instigator)
        {
            AbilityCastResult castResult = AbilityCastResult.Fail;
            AbilityTarget target = default;

            if (_positionRaycaster.GetWorldPosition(out Vector3 position) && await CanMovePlayer(instigator, position))
            {
                FireRunningAnimationChangeSignal(instigator, true);
                castResult = AbilityCastResult.InProgress;
                target.Point = position;
            }

            return (castResult, target);
        }

        public override UniTask<bool> CanCast(Character instigator) => new(true);

        public override async UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            instigator.Mover.InitializeMovement(_gridCell.CenterPosition);
            instigator.RuntimeData.ChangeOccupiedCell(_gridCell);

            await UniTask.WaitWhile(() => !instigator.Mover.Move());

            return AbilityCastResult.Success;
        }

        public override UniTask<bool> PostCast(Character instigator)
        {
            FireRunningAnimationChangeSignal(instigator, false);
            return new UniTask<bool>(true);
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