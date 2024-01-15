using System.Collections;
using System.Collections.Generic;
using LostInSin.Grid;
using LostInSin.Grid.Data;
using LostInSin.Grid.DataObjects;
using NUnit.Framework;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace LostInSin.Grid.Tests
{
    [TestFixture]
    public class GridCellGeneratorTests : ZenjectIntegrationTestFixture
    {
        private GridGenerationSO _gridGenerationSO;

        public void CommonInstall()
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.AddComponent<BoxCollider>();
            plane.layer = 3;
            plane.transform.position = Vector3.up * 4f;

            _gridGenerationSO = ScriptableObject.CreateInstance<GridGenerationSO>();
            _gridGenerationSO.GridXSize = 10;
            _gridGenerationSO.GridYSize = 10;
            _gridGenerationSO.GridRowCount = 9;
            _gridGenerationSO.GridColumnCount = 9;

            GridModel.Data data = new();
            data.GridData = _gridGenerationSO;

            // Set up the Zenject container and bindings
            PreInstall();

            Container.Bind<GridModel>().AsSingle();
            Container.BindInstances(data);
            Container.Bind<GridCellGenerator>().AsSingle();

            PostInstall();
        }

        [Inject] private GridModel _gridModel;
        [Inject] private GridCellGenerator _gridCellGenerator;

        [UnityTest]
        public IEnumerator GenerateGridCells_WithValidInput_ShouldGenerateCorrectGrid()
        {
            CommonInstall();

            yield return null;

            // Arrange
            int rowCount = _gridModel.GridRowCount + 1;
            int columnCount = _gridModel.GridColumnCount + 1;
            int gridSize = rowCount * columnCount;

            NativeArray<GridPoint> gridPoints = CreateMockGridPoints(rowCount, columnCount);

            // Act
            (GridCell[,] gridCells, GridCellData[,] gridCellsData) = _gridCellGenerator.GenerateGridCells(gridPoints);

            // Assert
            int expectedSize = rowCount - 1;
            Assert.AreEqual(expectedSize, gridCells.GetLength(0));
            Assert.AreEqual(expectedSize, gridCells.GetLength(1));
        }

        private NativeArray<GridPoint> CreateMockGridPoints(int row, int column)
        {
            NativeArray<GridPoint> gridPoints = new(row * column, Allocator.Temp);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    float posX = i * 1.0f;
                    float posY = j % 2 == 0 ? 2.0f : 0f;
                    float posZ = j * 1.0f;
                    bool isVoid = j % 5 == 0;

                    gridPoints[i * row + j] = new GridPoint(posX, posY, posZ, isVoid);
                }
            }

            return gridPoints;
        }

        [TearDown]
        public void Cleanup()
        {
            if (_gridGenerationSO != null) ScriptableObject.DestroyImmediate(_gridGenerationSO);
        }
    }
}