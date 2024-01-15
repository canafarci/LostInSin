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

        private TextMeshProUGUI _text;

        private readonly CompositeDisposable _disposables = new();

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            IDisposable disposable = _panelViewModel.OnAbilityInfoReceived.Subscribe(AbilitiesInfoReceivedHandler);
            _disposables.Add(disposable);
        }

        private void AbilitiesInfoReceivedHandler(List<AbilityInfo> abilities)
        {
            _text.text = abilities[_buttonIndex].Name;
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }
    }
}