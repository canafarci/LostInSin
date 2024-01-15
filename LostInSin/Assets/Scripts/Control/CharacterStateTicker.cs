using Zenject;
using LostInSin.Characters;
using LostInSin.Raycast;
using LostInSin.Input;
using UnityEngine.InputSystem;

namespace LostInSin.Control
{
    public class CharacterStateTicker : ITickable, IInitializable
    {
        [Inject] private IComponentRaycaster<Character> _characterRaycaster;
        [Inject] private GameInput _gameInput;
        private const int _characterLayerMask = 1 << 6;
        private Character _selectedCharacter = null;

        public void Initialize()
        {
            _gameInput.GameplayActions.Click.performed += OnClicked;
        }

        private void OnClicked(InputAction.CallbackContext context)
        {
            TryRaycastCharacter();
        }

        public void Tick()
        {
            _selectedCharacter?.TickState();
        }

        private void TryRaycastCharacter()
        {
            if (_characterRaycaster.RaycastComponent(out Character character, _characterLayerMask))
                TryChangeCharacter(character);
        }

        private void TryChangeCharacter(Character character)
        {
            if (_selectedCharacter == null)
                SetNewCharacterAsSelected(character);
            else if (_selectedCharacter.CanExitTickingCharacter()) SetNewCharacterAsSelected(character);
        }

        private void SetNewCharacterAsSelected(Character character)
        {
            _selectedCharacter = character;
            _selectedCharacter.SetAsTickingCharacter();
        }
    }
}