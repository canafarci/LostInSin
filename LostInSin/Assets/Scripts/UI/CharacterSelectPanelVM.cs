using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;
using LostInSin.Signals;
using LostInSin.UI.Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.UI
{
    public class CharacterSelectPanelVM : IInitializable, IDisposable
    {
        [Inject] private readonly CharacterSelectPanelModel _panelModel;
        [Inject] private readonly CharacterSelectPanelView _panelView;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly Data _data;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<Dictionary<CharacterPersistentData, Character>> _characterData = new();

        public ReactiveProperty<Dictionary<CharacterPersistentData, Character>> CharacterData => _characterData;


        public void Initialize()
        {
            _panelModel.CharacterData
                       .Subscribe(CharacterDataSetHandler)
                       .AddTo(_disposables);
        }

        private void CharacterDataSetHandler(Dictionary<CharacterPersistentData, Character> characters)
        {
            CharacterData.Value = characters;

            foreach (CharacterPersistentData data in characters.Keys)
            {
                CharacterSelectPanelIconView panelView = GameObject.Instantiate(_data.CharacterSelectPanelPrefab,
                                                                                _panelView.CharacterSelectIconHolder);

                panelView.Setup(data);
            }
        }

        public void Dispose()
        {
            _disposables.Clear();
        }

        public class Data
        {
            [SerializeField] private CharacterSelectPanelData _characterSelectPanelData;

            public CharacterSelectPanelIconView CharacterSelectPanelPrefab =>
                _characterSelectPanelData.CharacterSelectIconPrefab;
        }
    }
}