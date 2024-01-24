using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Grid.Tests
{
    [TestFixture]
    public class GridPointsGeneratorTests
    {
        private GridPointsGenerator _gridPointsGenerator;
        private NativeArray<RaycastHit> _mockHitResults;

        [SetUp]
        public void SetUp()
        {
            _gridPointsGenerator = new GridPointsGenerator();

            // Mock RaycastHit data
            _mockHitResults = new NativeArray<RaycastHit>(4, Allocator.TempJob);
            _mockHitResults[0] = new RaycastHit { point = new Vector3(1, 1, 1), distance = 1 };
            _mockHitResults[1] = new RaycastHit { point = new Vector3(2, 2, 2), distance = 1 };
            _mockHitResults[2] = new RaycastHit { point = new Vector3(3, 3, 3), distance = 1 };
            _mockHitResults[3] = new RaycastHit
                                 { point = new Vector3(4, 4, 4), distance = 0 }; // This should result in a default GridPoint
        }

        [Test]
        public void GenerateGridPoints_ShouldCreateCorrectGridPoints()
        {
            // Act
            NativeArray<GridPoint> gridPoints = _gridPointsGenerator.GenerateGridPoints(_mockHitResults);

            // Assert
            Assert.AreEqual(4, gridPoints.Length);
            Assert.AreEqual(new Vector3(1, 1, 1), gridPoints[0].ToVector3());
            Assert.AreEqual(new Vector3(2, 2, 2), gridPoints[1].ToVector3());
            Assert.AreEqual(new Vector3(3, 3, 3), gridPoints[2].ToVector3());
            Assert.AreEqual(new Vector3(0, 0, 0), gridPoints[3].ToVector3()); // Default GridPoint for hit.distance == 0

            // Clean up
            gridPoints.Dispose();
        }

        [Test]
        public void GenerateGridPoints_WithEmptyRaycastHits_ShouldReturnEmptyArray()
        {
            // Arrange
            NativeArray<RaycastHit> emptyHits = new(0, Allocator.TempJob);

            // Act
            NativeArray<GridPoint> gridPoints = _gridPointsGenerator.GenerateGridPoints(emptyHits);

            // Assert
            Assert.AreEqual(0, gridPoints.Length);

            // Clean up
            _mockHitResults.Dispose();
            gridPoints.Dispose();
        }

        [Test]
        public void GenerateGridPoints_WithNonHitRaycasts_ShouldReturnDefaultGridPoints()
        {
            // Arrange
            _mockHitResults[0] = new RaycastHit { point = new Vector3(0, 0, 0), distance = 0 };

            // Act
            NativeArray<GridPoint> gridPoints = _gridPointsGenerator.GenerateGridPoints(_mockHitResults);

            // Assert
            Assert.AreEqual(new Vector3(0, 0, 0), gridPoints[0].ToVector3()); // Default GridPoint for non-hit

            // Clean up
            gridPoints.Dispose();
        }

        [Test]
        public void GenerateGridPoints_WithValidRaycastHits_ShouldReturnValidGridPoints()
        {
            // Arrange
            _mockHitResults[0] = new RaycastHit { point = new Vector3(5, 5, 5), distance = 1 };

            // Act
            NativeArray<GridPoint> gridPoints = _gridPointsGenerator.GenerateGridPoints(_mockHitResults);

            // Assert
            Assert.AreEqual(new Vector3(5, 5, 5), gridPoints[0].ToVector3());

            // Clean up
            gridPoints.Dispose();
        }
    }
}