using System.Collections;
using System.Collections.Generic;
using LostInSin.Raycast;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace LostInSin.Raycast.Tests
{
    [TestFixture]
    public class MousePositionRayDrawerTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            Container.BindInterfacesAndSelfTo<MousePositionRayDrawer>().AsSingle();
        }

        [Test]
        public void DrawRay_ReturnsCorrectRay()
        {
            // Arrange
            MousePositionRayDrawer mousePositionRayDrawer = Container.Resolve<MousePositionRayDrawer>();

            // Act
            Ray ray = mousePositionRayDrawer.DrawRay();

            // Assert
            Assert.IsNotNull(ray);
        }
    }
}