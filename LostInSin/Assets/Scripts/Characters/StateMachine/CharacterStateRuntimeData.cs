using LostInSin.Grid;
using LostInSin.Grid.Data;

namespace LostInSin.Characters.StateMachine
{
    public class CharacterStateRuntimeData
    {
        private bool _isTicking = false;
        private bool _canExitState = true;
        private GridCellData _occupiedCell = null;

        public bool IsTicking
        {
            get => _isTicking;
            set => _isTicking = value;
        }

        public bool CanExitState
        {
            get => _canExitState;
            set => _canExitState = value;
        }

        public void ChangeOccupiedCell(GridCellData cell)
        {
            _occupiedCell?.SetAsUnoccupied();
            _occupiedCell = cell;
            _occupiedCell.SetAsOccupied();
        }

        public void ClearOccupiedCell()
        {
            _occupiedCell?.SetAsUnoccupied();
            _occupiedCell = null;
        }
    }
}