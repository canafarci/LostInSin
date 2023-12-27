using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace LostInSin.Grid.Tests
{
    public class GridCellDataTests
    {
        [Test]
        public void GridCellData_DefaultState_ShouldBeUnoccupied()
        {
            // Arrange
            var cellData = new GridCellData();
            // Assert
            Assert.IsFalse(cellData.IsOccupied);
        }

        [Test]
        public void SetAsOccupied_WhenCalled_ShouldSetCellAsOccupied()
        {
            // Arrange
            var cellData = new GridCellData();

            // Act
            cellData.SetAsOccupied();

            // Assert
            Assert.IsTrue(cellData.IsOccupied);
        }

        [Test]
        public void SetAsOccupied_WhenAlreadyOccupied_ShouldThrowException()
        {
            // Arrange
            var cellData = new GridCellData();
            cellData.SetAsOccupied();

            // Act & Assert
            Assert.Throws<System.Exception>(() => cellData.SetAsOccupied());
        }

        [Test]
        public void SetAsUnoccupied_WhenCalled_ShouldSetCellAsUnoccupied()
        {
            // Arrange
            var cellData = new GridCellData();
            cellData.SetAsOccupied(); // First set it as occupied

            // Act
            cellData.SetAsUnoccupied();

            // Assert
            Assert.IsFalse(cellData.IsOccupied);
        }

        [Test]
        public void SetAsUnoccupied_WhenAlreadyUnoccupied_ShouldThrowException()
        {
            // Arrange
            var cellData = new GridCellData();

            // Act & Assert
            Assert.Throws<System.Exception>(() => cellData.SetAsUnoccupied());
        }

        [Test]
        public void CenterPosition_WhenSet_ShouldStoreValue()
        {
            // Arrange
            var cellData = new GridCellData();
            var expectedPosition = new Vector3(1, 2, 3);

            // Act
            cellData.CenterPosition = expectedPosition;

            // Assert
            Assert.AreEqual(expectedPosition, cellData.CenterPosition);
        }
    }
}
