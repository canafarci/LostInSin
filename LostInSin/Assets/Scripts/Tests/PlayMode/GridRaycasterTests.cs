// using NUnit.Framework;
// using Unity.Collections;
// using UnityEngine;
// using Zenject;
// using System.Collections;
// using UnityEngine.TestTools;
// using System.Reflection;
// using LostInSin.Raycast.Data;
//
// namespace LostInSin.Raycast.Tests
// {
//     [TestFixture]
//     public class GridRaycasterTests : ZenjectIntegrationTestFixture
//     {
//         private GridRaycaster _gridRaycaster;
//         private GridRaycastData _mockRaycastData;
//         private GameObject _plane;
//
//         private void SetUp()
//         {
//             _plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
//             _plane.AddComponent<BoxCollider>();
//             _plane.layer = 3;
//             _plane.transform.localScale = Vector3.one * 200f;
//
//             PreInstall();
//             // Initialize GridRaycaster
//             _gridRaycaster = new GridRaycaster();
//
//             // Mock GridRaycastData
//             _mockRaycastData = CreateMockGridRaycastData();
//             PostInstall();
//         }
//
//         [UnityTest]
//         public IEnumerator PerformRaycasting_ShouldReturnCorrectNumberOfHits()
//         {
//             SetUp();
//             yield return null;
//
//             // Act
//             NativeArray<RaycastHit> hitResults = _gridRaycaster.PerformRaycasting(_mockRaycastData);
//
//             // Assert
//             int expectedHitsCount = (_mockRaycastData.GridRowCount + 1) * (_mockRaycastData.GridColumnCount + 1);
//             Assert.AreEqual(expectedHitsCount, hitResults.Length);
//
//             // Clean up
//             hitResults.Dispose();
//         }
//
//         private GridRaycastData CreateMockGridRaycastData() =>
//             new()
//             {
//                 GridRowCount = 9,
//                 GridColumnCount = 9,
//                 GridCellWidth = 1f,
//                 GridCellHeight = 1f,
//                 GridRowOffset = 0.5f,
//                 GridColumnOffset = 0.5f
//             };
//
//         [UnityTest]
//         public IEnumerator RaycastCommands_PrepareViaReflection_ShouldHaveCorrectOriginsAndDirections()
//         {
//             SetUp();
//             yield return null;
//
//             // Arrange
//             int rowCount = _mockRaycastData.GridRowCount + 1;
//             int columnCount = _mockRaycastData.GridColumnCount + 1;
//             int gridSize = rowCount * columnCount;
//             NativeArray<RaycastCommand> raycastCommands = new(gridSize, Allocator.TempJob);
//
//             // Use reflection to invoke the private method
//             MethodInfo prepareMethod =
//                 typeof(GridRaycaster).GetMethod("PrepareRaycastCommands", BindingFlags.NonPublic | BindingFlags.Instance);
//             object[] parameters = { rowCount, columnCount, _mockRaycastData, raycastCommands };
//
//             // Act
//             prepareMethod?.Invoke(_gridRaycaster, parameters);
//
//             // Assert
//             for (int row = 0; row < rowCount; row++)
//             {
//                 for (int column = 0; column < columnCount; column++)
//                 {
//                     int index = row + column * rowCount;
//                     Vector3 expectedOrigin = new(_mockRaycastData.GridCellWidth * row - _mockRaycastData.GridRowOffset,
//                                                  10f,
//                                                  _mockRaycastData.GridCellHeight * column -
//                                                  _mockRaycastData.GridColumnOffset);
//
//                     Assert.AreEqual(expectedOrigin, raycastCommands[index].from);
//                     Assert.AreEqual(Vector3.down, raycastCommands[index].direction);
//                 }
//             }
//
//             // Clean up
//             raycastCommands.Dispose();
//         }
//
//         [UnityTest]
//         public IEnumerator RaycastHits_ShouldBeAccurate()
//         {
//             SetUp();
//             yield return null;
//             // Act
//             _plane.SetActive(true);
//             yield return null;
//             yield return null;
//             yield return null;
//             NativeArray<RaycastHit> hitResults = _gridRaycaster.PerformRaycasting(_mockRaycastData);
//             yield return null;
//             yield return null;
//
//             // Assert
//             foreach (RaycastHit hit in hitResults)
//                 // Check if each hit result accurately hits the plane
//                 Assert.IsTrue(hit.collider != null && hit.collider.gameObject == _plane);
//
//             // Clean up
//             hitResults.Dispose();
//         }
//
//         [UnityTest]
//         public IEnumerator PerformRaycasting_WithNoHits_ShouldReturnNoHits()
//         {
//             SetUp();
//             yield return null;
//             // Arrange
//             _plane.SetActive(false); // Remove the plane to ensure no hits
//             yield return null;
//
//             // Act
//             NativeArray<RaycastHit> hitResults = _gridRaycaster.PerformRaycasting(_mockRaycastData);
//
//             // Assert
//             foreach (RaycastHit hit in hitResults) Assert.IsNull(hit.collider); // Expect no colliders to be hit
//
//             // Clean up
//             hitResults.Dispose();
//         }
//     }
// }

