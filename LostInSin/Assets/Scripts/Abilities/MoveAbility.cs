using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LostInSin.Characters;
using LostInSin.Grid;
using LostInSin.Grid.Data;
using LostInSin.Identifiers;
using LostInSin.Raycast;
using UnityEngine;
using Zenject;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Move", menuName = "LostInSin/Abilities/Move", order = 0)]
    public class MoveAbility : AbilityBlueprint
    {
        [Inject] private readonly IGridPositionConverter _gridPositionConverter;
        private GridCellData _gridCell;

        public override async UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            instigator.RuntimeData.CanExitState = false;
            instigator.RuntimeData.ChangeOccupiedCell(_gridCell);

            instigator.Mover.InitializeMovement(_gridCell.CenterPosition);

            UniTask.WaitWhile(() => !instigator.Mover.Move());

            instigator.RuntimeData.CanExitState = true;
            return AbilityCastResult.Success;
        }

        public override async UniTask<bool> CanCast(Character instigator, AbilityTarget target) =>
            await CanMovePlayer(instigator, target.Point);

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
    }
}