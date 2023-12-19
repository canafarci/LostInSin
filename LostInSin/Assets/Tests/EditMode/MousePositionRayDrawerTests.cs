using System.Collections;
using System.Collections.Generic;
using LostInSin.Raycast;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace LostInSin.Tests.EditMode
{
    [TestFixture]
    public class MousePositionRayDrawerTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public void Setup()
        {
            Container.BindInterfacesAndSelfTo<MousePositionRayDrawer>().AsSingle();
        }

        [Test]
        public void DrawRay_ReturnsCorrectRay()
        {
            // Arrange
            var mousePositionRayDrawer = Container.Resolve<MousePositionRayDrawer>();

            // Act
            Ray ray = mousePositionRayDrawer.DrawRay();

            // Assert
            Assert.IsNotNull(ray);
        }
    }
}
