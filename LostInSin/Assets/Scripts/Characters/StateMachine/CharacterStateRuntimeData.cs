using LostInSin.Grid;
using LostInSin.Grid.Data;

namespace LostInSin.Characters.StateMachine
{
    public class CharacterStateRuntimeData
    {
        private bool _isTicking = false;
        private bool _canExitTicking = true;
        private GridCellData _occupiedCell = null;

        public bool IsTicking
        {
            get => _isTicking;
            set => _isTicking = value;
        }

        public bool CanExitTicking
        {
            get => _canExitTicking;
            set => _canExitTicking = value;
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