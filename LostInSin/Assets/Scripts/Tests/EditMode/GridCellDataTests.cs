using LostInSin.Runtime.Grid.Data;
using NUnit.Framework;
using UnityEngine;

namespace LostInSin.Tests.EditMode.Tests.EditMode
{
	public class GridCellDataTests
	{
		[Test]
		public void GridCellData_DefaultState_ShouldBeUnoccupied()
		{
			// Arrange
			GridCellData cellData = new();
			// Assert
			Assert.IsFalse(cellData.isOccupied);
		}

		[Test]
		public void SetAsOccupied_WhenCalled_ShouldSetCellAsOccupied()
		{
			// Arrange
			GridCellData cellData = new();

			// Act
			cellData.SetAsOccupied();

			// Assert
			Assert.IsTrue(cellData.isOccupied);
		}

		[Test]
		public void SetAsOccupied_WhenAlreadyOccupied_ShouldThrowException()
		{
			// Arrange
			GridCellData cellData = new();
			cellData.SetAsOccupied();

			// Act & Assert
			Assert.Throws<System.Exception>(() => cellData.SetAsOccupied());
		}

		[Test]
		public void SetAsUnoccupied_WhenCalled_ShouldSetCellAsUnoccupied()
		{
			// Arrange
			GridCellData cellData = new();
			cellData.SetAsOccupied(); // First set it as occupied

			// Act
			cellData.SetAsUnoccupied();

			// Assert
			Assert.IsFalse(cellData.isOccupied);
		}

		[Test]
		public void SetAsUnoccupied_WhenAlreadyUnoccupied_ShouldThrowException()
		{
			// Arrange
			GridCellData cellData = new();

			// Act & Assert
			Assert.Throws<System.Exception>(() => cellData.SetAsUnoccupied());
		}

		[Test]
		public void CenterPosition_WhenSet_ShouldStoreValue()
		{
			// Arrange
			GridCellData cellData = new();
			Vector3 expectedPosition = new(1, 2, 3);

			// Act
			cellData.centerPosition = expectedPosition;

			// Assert
			Assert.AreEqual(expectedPosition, cellData.centerPosition);
		}
	}
}