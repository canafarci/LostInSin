using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using UniRx;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityPanelViewModel : IInitializable, IDisposable
    {
        [Inject] private readonly AbilityPanelModel _panelModel;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<List<AbilityInfo>> _abilities = new();

        public ReactiveProperty<List<AbilityInfo>> Abilities => _abilities;

        public void Initialize()
        {
            _panelModel.Abilities.Subscribe(AbilitiesSetHandler).AddTo(_disposables);
        }

        private void AbilitiesSetHandler(List<AbilityInfo> abilities)
        {
            _abilities.Value = abilities;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}