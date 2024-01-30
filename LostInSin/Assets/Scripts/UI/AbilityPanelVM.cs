using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.Signals;
using LostInSin.Signals.Abilities;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityPanelVM : IInitializable, IDisposable
    {
        [Inject] private readonly AbilityPanelModel _panelModel;
        [Inject] private readonly SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();

        private readonly ReactiveProperty<List<AbilityInfo>> _abilities = new();

        public ReactiveProperty<List<AbilityInfo>> Abilities => _abilities;

        public void Initialize()
        {
            _panelModel.Abilities
                       .Subscribe(AbilitiesSetHandler)
                       .AddTo(_disposables);
        }

        private void AbilitiesSetHandler(List<AbilityInfo> abilities)
        {
            _abilities.Value = abilities;
        }

        public void OnButtonClicked(AbilityInfo ability)
        {
            SelectedAbilityChangedSignal signal = new(ability);
            _signalBus.Fire(signal);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}