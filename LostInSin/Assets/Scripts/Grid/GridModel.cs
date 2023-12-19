using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridModel
    {
        public GridPoint[] _gridPoints;

        public void SetGrid(GridPoint[] gridPoints)
        {
            _gridPoints = gridPoints;
        }
    }
}
