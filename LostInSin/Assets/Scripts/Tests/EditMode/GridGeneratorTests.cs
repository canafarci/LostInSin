// using LostInSin.Grid.Data;
// using LostInSin.Grid.DataObjects;
// using NUnit.Framework;
// using Moq;
// using Zenject;
// using Unity.Collections;
// using UnityEngine;
// using LostInSin.Raycast;
// using LostInSin.Raycast.Data;
// using UnityEditor;
//
// namespace LostInSin.Grid.Tests
// {
//     [TestFixture]
//     public class GridGeneratorTests : ZenjectUnitTestFixture
//     {
//         private Mock<IGridRaycaster> _mockRaycaster;
//         private Mock<IGridPointsGenerator> _mockPointsGenerator;
//         private Mock<IGridCellGenerator> _mockCellGenerator;
//         private GridModel _gridModel;
//
//         [SetUp]
//         public void CommonInstall()
//         {
//             _mockRaycaster = new Mock<IGridRaycaster>();
//             _mockPointsGenerator = new Mock<IGridPointsGenerator>();
//             _mockCellGenerator = new Mock<IGridCellGenerator>();
//
//             GridGenerationSO gridGenerationSO =
//                 Resources.Load<GridGenerationSO>("Data/GridGenerationData");
//
//             GridModel.Data data = new();
//             data.GridData = gridGenerationSO;
//
//             Container.Bind<IGridRaycaster>().FromInstance(_mockRaycaster.Object);
//             Container.Bind<IGridPointsGenerator>().FromInstance(_mockPointsGenerator.Object);
//             Container.Bind<IGridCellGenerator>().FromInstance(_mockCellGenerator.Object);
//             Container.Bind<GridGenerator>().AsSingle();
//             Container.Bind<GridModel>().AsSingle();
//             Container.BindInstances(data);
//
//             Container.Inject(this);
//         }
//
//         [Inject] private GridGenerator _gridGenerator;
//
//         [Test]
//         public void Initialize_CallsDependenciesCorrectly()
//         {
//             // Arrange
//             NativeArray<RaycastHit> mockRaycastResults = new( /*...*/); // Setup your test data
//             NativeArray<GridPoint> mockGridPoints = new( /*...*/);
//             (GridCell[,], GridCellData[,]) mockGridCells = (new GridCell[,]
//                                                             {
//                                                                 /*...*/
//                                                             }, new GridCellData[,]
//                                                                {
//                                                                    /*...*/
//                                                                });
//
//             _mockRaycaster.Setup(x => x.PerformRaycasting(It.IsAny<GridRaycastData>()))
//                           .Returns(mockRaycastResults);
//             _mockPointsGenerator.Setup(x => x.GenerateGridPoints(mockRaycastResults))
//                                 .Returns(mockGridPoints);
//             _mockCellGenerator.Setup(x => x.GenerateGridCells(mockGridPoints))
//                               .Returns(mockGridCells);
//
//             // Act
//             _gridGenerator.GenerateGrid();
//
//             // Assert
//             _mockRaycaster.Verify(x => x.PerformRaycasting(It.IsAny<GridRaycastData>()), Times.Once);
//             _mockPointsGenerator.Verify(x => x.GenerateGridPoints(mockRaycastResults), Times.Once);
//             _mockCellGenerator.Verify(x => x.GenerateGridCells(mockGridPoints), Times.Once);
//         }
//     }
// }

