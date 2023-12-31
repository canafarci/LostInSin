using System.Collections;
using System.Collections.Generic;
using LostInSin.Grid.Visual;
using NUnit.Framework;
using UnityEngine;

namespace LostInSin.Grid.Tests
{
    [TestFixture]
    public class GridMeshGeneratorTests
    {
        private GridMeshGenerator _gridMeshGenerator;
        private GridCell[,] _gridCells;

        [SetUp]
        public void SetUp()
        {
            _gridMeshGenerator = new GridMeshGenerator();

            // Initialize grid cells with mock data
            _gridCells = new GridCell[2, 2]
            {
            {
                new GridCell(new GridPoint(0, 0, 0), new GridPoint(1, 0, 0), new GridPoint(0, 0, 1), new GridPoint(1, 0, 1), false),
                new GridCell(new GridPoint(1, 0, 0), new GridPoint(2, 0, 0), new GridPoint(1, 0, 1), new GridPoint(2, 0, 1), false)
            },
            {
                new GridCell(new GridPoint(0, 0, 1), new GridPoint(1, 0, 1), new GridPoint(0, 0, 2), new GridPoint(1, 0, 2), false),
                new GridCell(new GridPoint(1, 0, 1), new GridPoint(2, 0, 1), new GridPoint(1, 0, 2), new GridPoint(2, 0, 2), false)
            }
            };
        }

        [Test]
        public void CreateGridMesh_ShouldCreateMeshWithCorrectVertices()
        {
            // Act
            Mesh mesh = _gridMeshGenerator.CreateGridMesh(_gridCells);

            // Assert
            int cellCount = 4;
            int verticesPerCell = 4;
            Assert.AreEqual(cellCount * verticesPerCell, mesh.vertices.Length);
        }

        [Test]
        public void CreateGridMesh_ShouldCreateMeshWithCorrectTriangles()
        {
            // Act
            Mesh mesh = _gridMeshGenerator.CreateGridMesh(_gridCells);

            // Assert
            int cellCount = 4;
            int trianglesPerCell = 2;
            int triangleVerticesPerCell = 3;

            Assert.AreEqual(cellCount * trianglesPerCell * triangleVerticesPerCell, mesh.triangles.Length); // 2 triangles per cell, 3 vertices each
        }

        [Test]
        public void CreateGridMesh_ShouldCreateMeshWithCorrectUVs()
        {
            // Act
            Mesh mesh = _gridMeshGenerator.CreateGridMesh(_gridCells);

            // Assert
            int cellCount = 4;
            int uvIndexesPerCell = 4;
            Assert.AreEqual(cellCount * uvIndexesPerCell, mesh.uv.Length);
        }

        [Test]
        public void CreateGridMesh_ShouldExcludeInvalidCells()
        {
            // Arrange
            _gridCells[1, 1].IsInvalid = true; // Marking the cell as invalid

            // Act
            Mesh mesh = _gridMeshGenerator.CreateGridMesh(_gridCells);

            // Assert
            int cellCount = 3;
            int verticesPerCell = 4; // 3 valid cells with 2 vertices each
            Assert.AreEqual(cellCount * verticesPerCell, mesh.vertices.Length);
        }
    }
}
