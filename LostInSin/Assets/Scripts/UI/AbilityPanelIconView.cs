using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityPanelIconView : MonoBehaviour
    {
        [SerializeField] private int _buttonIndex;
        [Inject] private AbilityPanelVM _panelVM;

        private readonly CompositeDisposable _disposables = new();
        private TextMeshProUGUI _text;
        private AbilityInfo _ability;
        private Button _button;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponentInChildren<Button>();

            _panelVM.Abilities
                    .Subscribe(AbilitiesInfoReceivedHandler)
                    .AddTo(_disposables);

            _button.onClick.AddListener(ButtonClickHandler);
        }

        private void ButtonClickHandler()
        {
            _panelVM.OnButtonClicked(_ability);
        }

        private void AbilitiesInfoReceivedHandler(List<AbilityInfo> abilities)
        {
            if (abilities == null) return;

            _ability = abilities[_buttonIndex];

            _text.text = _ability.Name;
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }
    }
}