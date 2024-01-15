using LostInSin.Grid.Data;
using LostInSin.Grid.DataObjects;
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
            GridGenerationSO gridGenerationSO =
                AssetDatabase.LoadAssetAtPath<GridGenerationSO>("Assets/Data/GridGenerationData.asset");
            GridModel.Data data = new();
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
            GridCell[,] testCells = new GridCell[2, 2]
                                    {
                                        {
                                            new()
                                            {
                                                Center = new GridPoint(0, 0, 0, false)
                                            },
                                            new()
                                            {
                                                Center = new GridPoint(0, 0, 0, false)
                                            }
                                        },
                                        {
                                            new()
                                            {
                                                Center = new GridPoint(0, 0, 1, false)
                                            },
                                            new()
                                            {
                                                Center = new GridPoint(1, 0, 1, true)
                                            }
                                        }
                                    };

            GridCellData[,] testCellData = new GridCellData[2, 2]
                                           {
                                               {
                                                   new()
                                                   {
                                                       CenterPosition =
                                                           testCells[0, 0].Center.ToVector3()
                                                   },
                                                   new()
                                                   {
                                                       CenterPosition =
                                                           testCells[0, 1].Center.ToVector3()
                                                   }
                                               },
                                               {
                                                   new()
                                                   {
                                                       CenterPosition =
                                                           testCells[1, 0].Center.ToVector3()
                                                   },
                                                   new()
                                                   {
                                                       CenterPosition =
                                                           testCells[1, 1].Center.ToVector3()
                                                   }
                                               }
                                           };


            _gridModel.SetGridCells(testCells, testCellData);

            // Act & Assert
            Assert.AreEqual(testCells, _gridModel.GridCells);
            GridCellData retrievedData = _gridModel.GetGridCellData(0, 0);
            Assert.AreEqual(testCellData[0, 0].CenterPosition, retrievedData.CenterPosition);
        }

        [Test]
        public void GetGridCellData_ShouldReturnCorrectData()
        {
            // Arrange
            GridCell[,] gridCells = new GridCell[,]
                                    {
                                        {
                                            new(new GridPoint(0, 0, 0, false),
                                                new GridPoint(1, 1, 1, false),
                                                new GridPoint(0, 1, 0, false),
                                                new GridPoint(0, -1, 0, false),
                                                false)
                                        }
                                    };

            GridCellData[,] gridCellData = new GridCellData[,] { { new() } };
            _gridModel.SetGridCells(gridCells, gridCellData);

            // Act
            GridCellData data = _gridModel.GetGridCellData(0, 0);

            // Assert
            Assert.AreEqual(gridCells[0, 0].Center.ToVector3(), data.CenterPosition);
        }
    }
}