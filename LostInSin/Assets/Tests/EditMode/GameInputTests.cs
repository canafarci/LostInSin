using NUnit.Framework;
using LostInSin.Input;
using UniRx;

namespace LostInSin.Input.Tests
{
    [TestFixture]
    public class GameInputTests
    {
        private GameInput _gameInput;

        [SetUp]
        public void SetUp()
        {
            _gameInput = new GameInput();
        }

        [Test]
        public void Initialize_ShouldSetUpClickStream()
        {
            // Act
            _gameInput.Initialize();

            // Assert
            Assert.IsNotNull(_gameInput.ClickStream);
        }
    }
}
