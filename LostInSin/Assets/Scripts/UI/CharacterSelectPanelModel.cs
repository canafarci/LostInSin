using System;
using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;
using LostInSin.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.UI
{
    public class CharacterSelectPanelModel : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<Dictionary<CharacterPersistentData, Character>> _characterData = new();

        public ReactiveProperty<Dictionary<CharacterPersistentData, Character>> CharacterData => _characterData;

        public void Initialize()
        {
            _signalBus.GetStream<PlayableCharactersSpawnedSignal>()
                      .Subscribe(OnCharacterSpawned)
                      .AddTo(_disposables);
        }

        private void OnCharacterSpawned(PlayableCharactersSpawnedSignal signal)
        {
            _characterData.Value = signal.Characters;
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}