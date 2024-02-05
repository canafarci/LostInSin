using Zenject;

namespace LostInSin.PlayerInput
{
    public class GameInput : IInitializable
    {
        private readonly PlayerInputActions _inputActions;
        public PlayerInputActions.GameplayActions GameplayActions => _gameplayActions;
        public PlayerInputActions.GameplayActions _gameplayActions;

        public GameInput()
        {
            _inputActions = new PlayerInputActions();
        }

        public void Initialize()
        {
            _inputActions.Enable();
            _gameplayActions = _inputActions.Gameplay;
        }
    }
}