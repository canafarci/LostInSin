using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LostInSin.UI
{
    public class CharacterSelectPanelIconView : MonoBehaviour
    {
        [Inject] private CharacterSelectPanelVM _characterSelectPanelVm;

        private readonly CompositeDisposable _disposables = new();
        private CharacterPersistentData _data;
        private Button _button;
        private Image _image;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            _image = GetComponentInChildren<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();

            _button.onClick.AddListener(ButtonClickHandler);
        }

        private void ButtonClickHandler()
        {
            //_characterSelectViewModel.OnButtonClicked(_ability);
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        public void Setup(CharacterPersistentData data)
        {
            _data = data;
            _text.text = data.CharacterName;
            _image.sprite = data.CharacterAvatar;
        }
    }
}