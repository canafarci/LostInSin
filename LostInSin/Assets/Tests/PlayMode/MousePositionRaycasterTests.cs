using System.Collections;
using System.Collections.Generic;
using LostInSin.Raycast;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace LostInSin.Raycast.Tests
{
    [TestFixture]
    public class MousePositionRaycasterTests : ZenjectIntegrationTestFixture
    {
        private Mock<IRayDrawer> _rayDrawer;

        public void CommonInstall()
        {
            _rayDrawer = CreateMockRayDrawer();
            PreInstall();

            Container.BindInterfacesAndSelfTo<MousePositionRaycaster>().AsSingle();
            Container.Bind<IRayDrawer>().FromInstance(_rayDrawer.Object);

            PostInstall();
        }

        [UnityTest]
        public IEnumerator GetWorldPosition_RayHitsGround_ReturnsTrueAndCorrectPosition()
        {
            CommonInstall();
            yield return null;

            // Arrange
            var raycaster = Container.Resolve<MousePositionRaycaster>();

            var ray = new Ray(Vector3.up, Vector3.down);

            _rayDrawer.Setup(x => x.DrawRay()).Returns(ray);

            GameObject testObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            testObj.transform.position = new Vector3(0, -0.5f, 0);
            testObj.transform.localScale = new Vector3(10, 1, 10);
            testObj.layer = 3; //ground

            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 3);

            // Act
            bool result = raycaster.GetWorldPosition(out Vector3 position);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(hit.point, position);
        }

        [UnityTest]
        public IEnumerator GetWorldPosition_RayDoesNotHitGround_ReturnsFalseAndDefaultPosition()
        {
            CommonInstall();
            yield return null;

            // Arrange
            var raycaster = Container.Resolve<MousePositionRaycaster>();

            var ray = new Ray(Vector3.zero, Vector3.up);

            _rayDrawer.Setup(x => x.DrawRay()).Returns(ray);

            // Act
            bool result = raycaster.GetWorldPosition(out Vector3 position);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(Vector3.zero, position);
        }

        private Mock<IRayDrawer> CreateMockRayDrawer()
        {
            var mock = new Mock<IRayDrawer>();
            return mock;
        }
    }
}
