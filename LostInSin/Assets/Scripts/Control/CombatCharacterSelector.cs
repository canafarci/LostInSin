using System;
using LostInSin.Characters;
using LostInSin.Combat;
using LostInSin.Identifiers;
using LostInSin.Signals;
using LostInSin.Signals.Characters;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.Control
{
    public class CombatCharacterSelector : IInitializable, IDisposable
    {
        [Inject] private CharacterStateTicker _stateTicker;
        [Inject] private SignalBus _signalBus;
        [Inject] private TurnManager _turnManager;

        private readonly CompositeDisposable _disposables = new();

        private Character _selectedCharacter;

        public void Initialize()
        {
            _signalBus.GetStream<CharacterSelectSignal>()
                      .Subscribe(OnCharacterSelectSignal)
                      .AddTo(_disposables);

            _signalBus.GetStream<CharacterPortraitClickedSignal>()
                      .Subscribe(OnCharacterPortraitClicked)
                      .AddTo(_disposables);
        }

        private void OnCharacterSelectSignal(CharacterSelectSignal signal)
        {
            if (signal.Character.CharacterPersistentData.CharacterTeam == CharacterTeam.Friendly)
            {
                SetNewCharacterAsSelected(signal.Character);
            }
            else if (_selectedCharacter != null)
            {
                _stateTicker.SetTickingCharacter(null);
                _selectedCharacter.ExitTickingCharacter();
                _selectedCharacter = null;
            }
        }

        private void OnCharacterPortraitClicked(CharacterPortraitClickedSignal signal)
        {
            TryChangeCharacter(signal.Character);
        }

        private void TryChangeCharacter(Character character)
        {
            if (_selectedCharacter == character) return;

            if (CanChangeCharacter(character))
                _signalBus.Fire(new CharacterSelectSignal(character));
        }

        private bool CanChangeCharacter(Character character)
        {
            bool canChangeCharacter = false;

            bool isCharactersTurn = _turnManager.SelectableCharacters.Contains(character);

            if (_selectedCharacter == null)
                canChangeCharacter = isCharactersTurn;
            else
                canChangeCharacter = _selectedCharacter.CanExitTickingCharacter() &&
                                     isCharactersTurn;


            return canChangeCharacter;
        }

        private void SetNewCharacterAsSelected(Character character)
        {
            if (_selectedCharacter != null)
                _selectedCharacter.ExitTickingCharacter();

            _selectedCharacter = character;

            _stateTicker.SetTickingCharacter(_selectedCharacter);
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}