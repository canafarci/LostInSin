using LostInSin.Grid.Data;
using LostInSin.Raycast;
using LostInSin.Raycast.Data;
using Unity.Collections;
using UnityEngine;
using Zenject;

namespace LostInSin.Grid
{
    public class GridGenerator
    {
        private readonly IGridRaycaster _raycaster;
        private readonly IGridPointsGenerator _pointsGenerator;
        private readonly IGridCellGenerator _cellGenerator;
        private readonly GridModel _gridModel;

        private GridGenerator(GridModel gridModel,
                              IGridRaycaster raycaster,
                              IGridPointsGenerator pointsGenerator,
                              IGridCellGenerator cellGenerator)
        {
            _gridModel = gridModel;
            _raycaster = raycaster;
            _pointsGenerator = pointsGenerator;
            _cellGenerator = cellGenerator;
        }

        public void GenerateGrid()
        {
            GridRaycastData raycastData = CreateRaycastData();

            NativeArray<RaycastHit> raycastResults = _raycaster.PerformRaycasting(raycastData);
            NativeArray<GridPoint> gridPoints = _pointsGenerator.GenerateGridPoints(raycastResults);
            (GridCell[,] cells, GridCellData[,] data ) gridCells = _cellGenerator.GenerateGridCells(gridPoints);

            _gridModel.SetGridCells(gridCells.cells, gridCells.data);
        }

        private GridRaycastData CreateRaycastData() =>
            new()
            {
                GridRowCount = _gridModel.GridRowCount,
                GridCellWidth = _gridModel.GridCellWidth,
                GridCellHeight = _gridModel.GridCellHeight,
                GridRowOffset = _gridModel.GridRowOffset,
                GridColumnOffset = _gridModel.GridColumnOffset,
                GridColumnCount = _gridModel.GridColumnCount
            };
    }
}