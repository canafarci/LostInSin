using System;
using LostInSin.Abilities;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private int _buttonIndex;
        [Inject] private AbilityViewModel _viewModel;

        private TextMeshProUGUI _text;

        private readonly CompositeDisposable _disposables = new();

        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            IDisposable disposable = _viewModel.OnAbilityInfoReceived.Subscribe(AbilitiesInfoReceivedHandler);
            _disposables.Add(disposable);
        }

        private void AbilitiesInfoReceivedHandler(AbilityInfo[] abilities)
        {
            _text.text = abilities[_buttonIndex].Name;
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }
    }
}