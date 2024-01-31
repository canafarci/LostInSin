using System;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Signals;
using LostInSin.Signals.Characters;
using UniRx;
using Zenject;

namespace LostInSin.Control
{
    public class CombatCharacterSelector : IInitializable, IDisposable
    {
        [Inject] private CharacterStateTicker _stateTicker;
        [Inject] private SignalBus _signalBus;

        private const int CHARACTER_LAYER_MASK = 1 << 6;
        private readonly CompositeDisposable _disposables = new();

        private Character _selectedCharacter = null;

        public void Initialize()
        {
            _signalBus.GetStream<SelectCharactersSignal>()
                      .Subscribe(OnSelectCharactersSignal)
                      .AddTo(_disposables);

            _signalBus.GetStream<CharacterPortraitClickedSignal>()
                      .Subscribe(OnCharacterPortraitClicked)
                      .AddTo(_disposables);
        }

        private void OnSelectCharactersSignal(SelectCharactersSignal signal)
        {
            SetNewCharacterAsSelected(signal.InitialCharacter);
        }

        private void OnCharacterPortraitClicked(CharacterPortraitClickedSignal signal)
        {
            TryChangeCharacter(signal.Character);
        }

        private void TryChangeCharacter(Character character)
        {
            if (_selectedCharacter == character) return;

            if (CanChangeCharacter(character))
                SetNewCharacterAsSelected(character);
        }

        private bool CanChangeCharacter(Character character) =>
            character.CharacterPersistentData.CharacterTeam == CharacterTeam.Friendly &&
            _selectedCharacter.CanExitTickingCharacter();

        private void SetNewCharacterAsSelected(Character character)
        {
            _selectedCharacter = character;
            _signalBus.Fire(new CharacterSelectSignal(_selectedCharacter));
            _stateTicker.SetTickingCharacter(_selectedCharacter);
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}