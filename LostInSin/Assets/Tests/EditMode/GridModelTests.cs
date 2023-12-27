using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LostInSin.Grid.Tests
{
    public class GridModelTests : ZenjectUnitTestFixture
    {
        private GridGenerationSO _gridGenerationSO;

        [SetUp]
        public void SetUp()
        {
            GridGenerationSO gridGenerationSO = AssetDatabase.LoadAssetAtPath<GridGenerationSO>("Assets/Data/GridGenerationData.asset");
            GridModel.Data data = new GridModel.Data();
            data.GridData = gridGenerationSO;

            Container.Bind<GridModel>().AsSingle();
            Container.BindInstances(data);
            Container.Inject(this);
        }

        [Inject] private GridModel.Data _gridData;
        [Inject] private GridModel _gridModel;

        [Test]
        public void GridDimensionsAndOffsets_ShouldBeCorrect()
        {
            // Assert
            Assert.AreEqual(_gridData.GridData.GridXSize, _gridModel.GridCellWidth);
            Assert.AreEqual(_gridData.GridData.GridYSize, _gridModel.GridCellHeight);
            Assert.AreEqual(RoundToEvenNumber(_gridData.GridData.GridRowCount), _gridModel.GridRowCount);
            Assert.AreEqual(RoundToEvenNumber(_gridData.GridData.GridColumnCount), _gridModel.GridColumnCount);
            Assert.AreEqual(_gridModel.GridCellHeight * _gridModel.GridColumnCount / 2f, _gridModel.GridRowOffset);
            Assert.AreEqual(_gridModel.GridCellWidth * _gridModel.GridRowCount / 2f, _gridModel.GridColumnOffset);
        }

        private int RoundToEvenNumber(int number)
        {
            return number % 2 == 0 ? number : number - 1;
        }

        [Test]
        public void SetAndGetGridCells_ShouldWorkCorrectly()
        {
            // Arrange
            var testCells = new GridCell[2, 2]{{new GridCell(){
                                                        Center = new GridPoint(0, 0, 0, false),
                                                        },
                                                        new GridCell(){
                                                        Center = new GridPoint(0, 0, 0, false),
                                                        }},
                                                        {new GridCell(){
                                                        Center = new GridPoint(0, 0, 1, false),
                                                        },
                                                        new GridCell(){
                                                        Center = new GridPoint(1, 0, 1, true),
                                                        }}};

            var testCellData = new GridCellData[2, 2] {{new GridCellData(){
                                                        CenterPosition = testCells[0,0].Center.ToVector3()
                                                        },
                                                        new GridCellData(){
                                                        CenterPosition = testCells[0,1].Center.ToVector3()
                                                        }},
                                                         {new GridCellData(){
                                                        CenterPosition = testCells[1,0].Center.ToVector3()
                                                        },
                                                        new GridCellData(){
                                                        CenterPosition = testCells[1,1].Center.ToVector3()
                                                        }}};



            _gridModel.SetGridCells(testCells, testCellData);

            // Act & Assert
            Assert.AreEqual(testCells, _gridModel.GridCells);
            var retrievedData = _gridModel.GetGridCellData(0, 0);
            Assert.AreEqual(testCellData[0, 0].CenterPosition, retrievedData.CenterPosition);
        }

        [Test]
        public void GetGridCellData_ShouldReturnCorrectData()
        {
            // Arrange
            var gridCells = new GridCell[,] { { new GridCell(new GridPoint(0, 0, 0, false),
                                                             new GridPoint(1, 1, 1, false),
                                                             new GridPoint(0, 1, 0, false),
                                                             new GridPoint(0, -1, 0, false),
                                                             false) } };

            var gridCellData = new GridCellData[,] { { new GridCellData() } };
            _gridModel.SetGridCells(gridCells, gridCellData);

            // Act
            var data = _gridModel.GetGridCellData(0, 0);

            // Assert
            Assert.AreEqual(gridCells[0, 0].Center.ToVector3(), data.CenterPosition);
        }
    }
}
