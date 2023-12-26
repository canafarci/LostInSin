using LostInSin.Raycast;
using Raycast.Data;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Zenject;

namespace LostInSin.Grid
{
    public class GridGenerator : IInitializable
    {
        private readonly IGridRaycaster _raycaster;
        private readonly IGridPointsGenerator _pointsGenerator;
        private readonly IGridCellGenerator _cellGenerator;
        private readonly GridModel _gridModel;
        private readonly int _groundLayerMask;

        private GridGenerator(GridModel gridModel,
                              IGridRaycaster raycaster,
                              IGridPointsGenerator pointsGenerator,
                              IGridCellGenerator cellGenerator)
        {
            _gridModel = gridModel;
            _raycaster = raycaster;
            _pointsGenerator = pointsGenerator;
            _cellGenerator = cellGenerator;
            _groundLayerMask = LayerMask.GetMask("Ground");
        }

        public void Initialize()
        {
            GridRaycastData raycastData = CreateRaycastData();

            NativeArray<RaycastHit> raycastResults = _raycaster.PerformRaycasting(raycastData);
            NativeArray<GridPoint> gridPoints = _pointsGenerator.GenerateGridPoints(raycastResults);
            (GridCell[,], GridCellData[,]) gridCells = _cellGenerator.GenerateGridCells(gridPoints);

            _gridModel.SetGridCells(gridCells.Item1, gridCells.Item2);
        }

        private GridRaycastData CreateRaycastData()
        {
            return new()
            {
                GridRowCount = _gridModel.GridRowCount,
                GridCellWidth = _gridModel.GridCellWidth,
                GridCellHeight = _gridModel.GridCellHeight,
                GridRowOffset = _gridModel.GridRowOffset,
                GridColumnOffset = _gridModel.GridColumnOffset,
                GridColumnCount = _gridModel.GridColumnCount,
            };
        }
    }
}