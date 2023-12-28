using System.Collections;
using System.Collections.Generic;
using LostInSin.Raycast;
using NUnit.Framework;
using UnityEngine;
using Zenject;
using Moq;
using UnityEngine.TestTools;
using System;

namespace LostInSin.Raycast.Tests
{
    [TestFixture]
    public class ComponentRaycasterTests : ZenjectIntegrationTestFixture
    {
        private Mock<IRayDrawer> _mockRayDrawer;

        public void CommonInstall()
        {
            _mockRayDrawer = new Mock<IRayDrawer>();

            Vector3 rayOrigin = Vector3.zero;
            Vector3 rayDirection = new Vector3(0, 0, 4);
            Ray testRay = new Ray(rayOrigin, rayDirection);

            _mockRayDrawer.Setup(drawer => drawer.DrawRay()).Returns(testRay);

            PreInstall();

            Container.Bind<IRayDrawer>().FromInstance(_mockRayDrawer.Object);
            Container.Bind<ComponentRaycaster<TestComponent>>().AsSingle();

            PostInstall();
        }

        [UnityTest]
        public IEnumerator RaycastComponent_ComponentFound_ReturnsTrue()
        {
            CommonInstall();

            yield return null;
            // Arrange
            var raycaster = Container.Resolve<ComponentRaycaster<TestComponent>>();

            GameObject testObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            testObj.AddComponent<TestComponent>();

            testObj.transform.position = new Vector3(0, 0, 4);
            testObj.transform.localScale = new Vector3(3, 3, 3);

            yield return null;
            yield return null;
            yield return null;

            // Act
            bool result = raycaster.RaycastComponent(out TestComponent component, LayerMask.GetMask("Default"));

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(component);
        }

        [UnityTest]
        public IEnumerator RaycastComponent_DifferentLayer_ReturnsFalse()
        {
            CommonInstall();

            yield return null;
            // Arrange
            var raycaster = Container.Resolve<ComponentRaycaster<TestComponent>>();

            GameObject testObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            testObj.AddComponent<TestComponent>();

            testObj.transform.position = new Vector3(0, 0, 4);
            testObj.transform.localScale = new Vector3(3, 3, 3);

            // Act
            bool result = raycaster.RaycastComponent(out TestComponent component, LayerMask.GetMask("Ground"));
            yield return null;
            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(component);
        }

        [UnityTest]
        public IEnumerator RaycastComponent_NoComponent_ReturnsFalse()
        {
            CommonInstall();

            yield return null;
            // Arrange
            var raycaster = Container.Resolve<ComponentRaycaster<TestComponent>>();

            GameObject testObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            testObj.transform.position = new Vector3(0, 0, 4);
            testObj.transform.localScale = new Vector3(3, 3, 3);


            yield return null;
            yield return null;
            yield return null;
            // Assert
            Assert.That(() => raycaster.RaycastComponent(out TestComponent component, LayerMask.GetMask("Default")),
                  Throws.TypeOf<Exception>());
        }
    }

    public class TestComponent : MonoBehaviour
    {

    }
}

