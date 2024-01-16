using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityPanelIconView : MonoBehaviour
    {
        [SerializeField] private int _buttonIndex;
        [Inject] private AbilityPanelViewModel _panelViewModel;

        private readonly CompositeDisposable _disposables = new();
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();

            _panelViewModel.Abilities
                           .Subscribe(AbilitiesInfoReceivedHandler)
                           .AddTo(_disposables);
        }

        private void AbilitiesInfoReceivedHandler(List<AbilityInfo> abilities)
        {
            if (abilities == null) return;
            _text.text = abilities[_buttonIndex].Name;
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }
    }
}