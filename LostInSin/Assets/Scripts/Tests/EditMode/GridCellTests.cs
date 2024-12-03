using LostInSin.Runtime.Gameplay.Grid.Data;
using NUnit.Framework;
using UnityEngine;

namespace LostInSin.Tests.EditMode.Tests.EditMode
{
	public class GridCellTests
	{
		[Test]
		public void GridCellData_DefaultState_ShouldBeUnoccupied()
		{
			// Arrange
			GridCell cell = new();
			// Assert
			Assert.IsFalse(cell.isOccupied);
		}

		[Test]
		public void SetAsOccupied_WhenCalled_ShouldSetCellAsOccupied()
		{
			// Arrange
			GridCell cell = new();

			// Act
			cell.SetAsOccupied();

			// Assert
			Assert.IsTrue(cell.isOccupied);
		}

		[Test]
		public void SetAsOccupied_WhenAlreadyOccupied_ShouldThrowException()
		{
			// Arrange
			GridCell cell = new();
			cell.SetAsOccupied();

			// Act & Assert
			Assert.Throws<System.Exception>(() => cell.SetAsOccupied());
		}

		[Test]
		public void SetAsUnoccupied_WhenCalled_ShouldSetCellAsUnoccupied()
		{
			// Arrange
			GridCell cell = new();
			cell.SetAsOccupied(); // First set it as occupied

			// Act
			cell.SetAsUnoccupied();

			// Assert
			Assert.IsFalse(cell.isOccupied);
		}

		[Test]
		public void SetAsUnoccupied_WhenAlreadyUnoccupied_ShouldThrowException()
		{
			// Arrange
			GridCell cell = new();

			// Act & Assert
			Assert.Throws<System.Exception>(() => cell.SetAsUnoccupied());
		}

		[Test]
		public void CenterPosition_WhenSet_ShouldStoreValue()
		{
			// Arrange
			GridCell cell = new();
			Vector3 expectedPosition = new(1, 2, 3);

			// Act
			cell.centerPosition = expectedPosition;

			// Assert
			Assert.AreEqual(expectedPosition, cell.centerPosition);
		}
	}
}