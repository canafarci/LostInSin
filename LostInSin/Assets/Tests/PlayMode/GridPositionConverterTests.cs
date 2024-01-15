using NUnit.Framework;
using UnityEngine;
using Zenject;
using UnityEngine.TestTools;
using System.Collections;
using LostInSin.Grid.Data;
using LostInSin.Grid.DataObjects;
using LostInSin.Raycast;

namespace LostInSin.Grid.Tests
{
    [TestFixture]
    public class GridPositionConverterTests : ZenjectIntegrationTestFixture
    {
        [Inject] private GridModel _gridModel;
        [Inject] private GridPositionConverter _gridPositionConverter;
        private GridGenerationSO _gridGenerationSO;

        public void SetUp()
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.AddComponent<BoxCollider>();
            plane.layer = 3;
            plane.transform.localScale = Vector3.one * 200f;

            _gridGenerationSO = ScriptableObject.CreateInstance<GridGenerationSO>();

            _gridGenerationSO.GridXSize = 1;
            _gridGenerationSO.GridYSize = 1;
            _gridGenerationSO.GridRowCount = 9;
            _gridGenerationSO.GridColumnCount = 9;

            GridModel.Data data = new();
            data.GridData = _gridGenerationSO;

            PreInstall();
            // Set up the Zenject container and bindings
            Container.BindInstances(data);
            Container.Bind<GridModel>().AsSingle();

            Container.Bind<IGridRaycaster>().To<GridRaycaster>().AsSingle();
            Container.Bind<IGridCellGenerator>().To<GridCellGenerator>().AsSingle();
            Container.Bind<IGridPointsGenerator>().To<GridPointsGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridGenerator>().AsSingle().NonLazy();

            Container.Bind<IGridPositionConverter>().To<GridPositionConverter>().AsSingle();
            GridPositionConverter posConverter = Container.Resolve<IGridPositionConverter>() as GridPositionConverter;
            Container.Bind<GridPositionConverter>().FromInstance(posConverter);


            PostInstall();
        }

        [UnityTest]
        public IEnumerator GetWorldPoint_WithRowAndColumn_ShouldReturnCorrectWorldPoint()
        {
            SetUp();
            yield return null;
            yield return null;
            yield return null;

            // Arrange
            int row = 1;
            int column = 1;

            // Assuming a specific center position for this cell
            Debug.Log(_gridModel);
            Vector3 expectedWorldPoint = _gridModel.GetGridCellData(row, column).CenterPosition;

            // Act
            Vector3 worldPoint = _gridPositionConverter.GetWorldPoint(row, column);

            // Assert
            Assert.AreEqual(expectedWorldPoint, worldPoint);
        }

        [UnityTest]
        public IEnumerator GetWorldPoint_WithGridCell_ShouldReturnCorrectWorldPoint()
        {
            SetUp();
            yield return null;

            // Arrange
            int row = 1;
            int column = 1;
            GridCellData gridCell = _gridModel.GetGridCellData(row, column);

            // Assuming a specific center position for this cell
            Vector3 expectedWorldPoint = gridCell.CenterPosition;

            // Act
            Vector3 worldPoint = _gridPositionConverter.GetWorldPoint(1, 1);

            // Assert
            Assert.AreEqual(expectedWorldPoint, worldPoint);
        }

        [UnityTest]
        public IEnumerator GetCell_WithWorldPosition_ShouldReturnCorrectCellData()
        {
            SetUp();
            yield return null;

            // Arrange
            Vector3 worldPosition = new(0.5f, 0, 0.5f); // Example position within the grid
            GridCellData
                expectedCellData = _gridModel.GetGridCellData(4, 4); // Assuming this position corresponds to row 0, column 0

            // Act
            bool result = _gridPositionConverter.GetCell(worldPosition, out GridCellData cellData);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedCellData, cellData);
        }

        [UnityTest]
        public IEnumerator GetWorldPoint_WithGridCell_ShouldReturnCenterPointOfCell()
        {
            SetUp();
            yield return null;

            // Arrange
            GridCell testCell = CreateTestGridCell();
            Vector3 expectedWorldPoint = testCell.Center.ToVector3();

            // Act
            Vector3 worldPoint = _gridPositionConverter.GetWorldPoint(testCell);

            // Assert
            Assert.AreEqual(expectedWorldPoint, worldPoint);
        }

        private GridCell CreateTestGridCell()
        {
            GridPoint topLeft = new(0, 0, 0);
            GridPoint topRight = new(1, 0, 0);
            GridPoint bottomLeft = new(0, 0, 1);
            GridPoint bottomRight = new(1, 0, 1);

            return new GridCell(topLeft, topRight, bottomLeft, bottomRight, false);
        }
    }
}