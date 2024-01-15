using System;
using LostInSin.Characters;
using LostInSin.Input;
using LostInSin.Raycast;
using LostInSin.Signals;
using UniRx;
using UnityEngine;
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

        private const int _characterLayerMask = 1 << 6;
        private readonly CompositeDisposable _disposables = new();

        private Character _selectedCharacter = null;

        public void Initialize()
        {
            _gameInput.GameplayActions.Click.performed += OnClicked;

            _signalBus.GetStream<SelectInitialCharacterSignal>()
                      .Subscribe(OnInitialCharacterSelect)
                      .AddTo(_disposables);
        }

        private void OnInitialCharacterSelect(SelectInitialCharacterSignal signal)
        {
            SetNewCharacterAsSelected(signal.InitialCharacter);
        }

        private void OnClicked(InputAction.CallbackContext context)
        {
            TryRaycastCharacter();
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