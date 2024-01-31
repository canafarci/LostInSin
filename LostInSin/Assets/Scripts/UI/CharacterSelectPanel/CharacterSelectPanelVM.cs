using System;
using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Signals;
using LostInSin.UI.Data;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.UI.CharacterSelectPanel
{
    public class CharacterSelectPanelVM : ViewModelBase, IInitializable
    {
        [Inject] private readonly CharacterSelectPanelModel _panelModel;
        [Inject] private readonly CharacterSelectPanelView _panelView;
        [Inject] private readonly Data _data;
        [Inject] private readonly CharacterSelectPanelIconView.Factory _iconViewFactory;

        private List<Character> _characters;

        public void Initialize()
        {
            _panelModel.PlayerCharacters
                       .Subscribe(CharacterDataSetHandler)
                       .AddTo(_disposables);
        }

        private void CharacterDataSetHandler(List<Character> characters)
        {
            if (characters == null) return;

            _characters = characters;

            foreach (Character character in characters)
            {
                _iconViewFactory.Create(_data.CharacterSelectPanelPrefab,
                                        _panelView.CharacterSelectIconHolder,
                                        this,
                                        character);
            }
        }

        public class Data
        {
            [SerializeField] private CharacterSelectPanelData _characterSelectPanelData;

            public CharacterSelectPanelIconView CharacterSelectPanelPrefab =>
                _characterSelectPanelData.CharacterSelectIconPrefab;
        }

        public void OnButtonClicked(Character clickedCharacter)
        {
            CharacterPortraitClickedSignal signal = new(clickedCharacter);
            _signalBus.Fire(signal);
        }
    }
}