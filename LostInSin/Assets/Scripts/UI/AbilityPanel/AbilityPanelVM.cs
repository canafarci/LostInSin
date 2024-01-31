using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.Signals.Abilities;
using UniRx;
using Zenject;

namespace LostInSin.UI.AbilityPanel
{
    public class AbilityPanelVM : ViewModelBase, IInitializable
    {
        [Inject] private readonly AbilityPanelModel _panelModel;

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
    }
}