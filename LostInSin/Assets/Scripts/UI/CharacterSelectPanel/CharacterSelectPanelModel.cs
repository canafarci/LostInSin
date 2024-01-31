using System;
using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Signals.Characters;
using UniRx;
using Zenject;

namespace LostInSin.UI.CharacterSelectPanel
{
    public class CharacterSelectPanelModel : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<List<Character>> _playerCharacters = new();

        public ReactiveProperty<List<Character>> PlayerCharacters => _playerCharacters;

        public void Initialize()
        {
            _signalBus.GetStream<PlayableCharactersSpawnedSignal>()
                      .Subscribe(OnCharacterSpawned)
                      .AddTo(_disposables);
        }

        private void OnCharacterSpawned(PlayableCharactersSpawnedSignal signal)
        {
            _playerCharacters.Value = signal.Characters;
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}