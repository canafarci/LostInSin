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

        private Button _button;
        private Character _character;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();

            _button.onClick.AddListener(ButtonClickHandler);
        }

        private void ButtonClickHandler()
        {
            _characterSelectPanelVM.OnButtonClicked(_character);
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        [Inject]
        private void Init(Character character, CharacterSelectPanelVM panelVM, RectTransform parent)
        {
            _characterSelectPanelVM = panelVM;
            
            transform.SetParent(parent);
            transform.localScale = Vector3.one;

            _character = character;

            CharacterPersistentData data = character.CharacterPersistentData;

            _text.text = data.CharacterName;
            _image.sprite = data.CharacterAvatar;
        }

        public class Factory : PlaceholderFactory<Object, RectTransform, CharacterSelectPanelVM, Character,
            CharacterSelectPanelIconView>
        {
        }
    }
}