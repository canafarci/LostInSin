using System;
using LostInSin.Characters;
using LostInSin.Core;
using LostInSin.Input;
using LostInSin.Raycast;
using LostInSin.Signals;
using UniRx;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Control
{
    public class CharacterSelector : IInitializable, IDisposable
    {
        [Inject] private GameInput _gameInput;
        [Inject] private IComponentRaycaster<Character> _characterRaycaster;
        [Inject] private CharacterStateTicker _stateTicker;
        [Inject] private SignalBus _signalBus;
        [Inject] private PointerOverUIChecker _pointerOverUIChecker;

        private const int CHARACTER_LAYER_MASK = 1 << 6;
        private readonly CompositeDisposable _disposables = new();

        private Character _selectedCharacter = null;

        public void Initialize()
        {
            _gameInput.GameplayActions.Click.performed += OnClickPerformed;

            _signalBus.GetStream<SelectInitialCharacterSignal>()
                      .Subscribe(OnInitialCharacterSelect)
                      .AddTo(_disposables);
        }

        private void OnInitialCharacterSelect(SelectInitialCharacterSignal signal)
        {
            SetNewCharacterAsSelected(signal.InitialCharacter);
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            if (_pointerOverUIChecker.PointerIsOverUI) return;
            TryRaycastCharacter();
        }

        private void TryRaycastCharacter()
        {
            if (_characterRaycaster.RaycastComponent(out Character character, CHARACTER_LAYER_MASK))
                TryChangeCharacter(character);
        }

        private void TryChangeCharacter(Character character)
        {
            if (_selectedCharacter == null)
                SetNewCharacterAsSelected(character);

            else if (_selectedCharacter.CanExitTickingCharacter())
                SetNewCharacterAsSelected(character);
        }

        private void SetNewCharacterAsSelected(Character character)
        {
            _selectedCharacter = character;
            _signalBus.Fire(new CharacterSelectedSignal(_selectedCharacter));
            _stateTicker.SetTickingCharacter(_selectedCharacter);
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}