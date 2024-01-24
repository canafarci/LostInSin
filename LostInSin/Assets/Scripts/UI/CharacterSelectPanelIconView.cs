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
        private CharacterSelectPanelVM _characterSelectPanelVM;

        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        private readonly CompositeDisposable _disposables = new();


        private CharacterPersistentData _data;
        private Button _button;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();

            _button.onClick.AddListener(ButtonClickHandler);
        }

        private void ButtonClickHandler()
        {
            _characterSelectPanelVM.OnButtonClicked(_data);
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        public void Setup(CharacterPersistentData data, CharacterSelectPanelVM characterSelectPanelVM)
        {
            _data = data;
            _text.text = data.CharacterName;
            _image.sprite = data.CharacterAvatar;
            _characterSelectPanelVM = characterSelectPanelVM;
        }
    }
}