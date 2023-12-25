using System.Collections;
using System.Collections.Generic;
using LostInSin.Grid;
using UnityEngine;

namespace LostInSin.Characters
{
    public class CharacterStateRuntimeData
    {
        private bool _isTicking = false;
        private bool _canExitTicking = true;
        private GridCellData _occupiedCell = null;
        public bool IsTicking { get { return _isTicking; } set { _isTicking = value; } }
        public bool CanExitTicking { get { return _canExitTicking; } set { _canExitTicking = value; } }
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
