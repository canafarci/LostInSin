using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using UniRx;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityPanelViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveCommand<List<AbilityInfo>> OnAbilityInfoReceived = new();
        [Inject] private readonly AbilityPanelModel _panelModel;

        private List<AbilityInfo> _abilities;

        public void Initialize()
        {
            _panelModel.OnAbilitiesSet += AbilitiesSetHandler;
        }

        private void AbilitiesSetHandler(List<AbilityInfo> abilities)
        {
            _abilities = abilities;
            OnAbilityInfoReceived.Execute(abilities);
        }

        public void Dispose()
        {
            _panelModel.OnAbilitiesSet -= AbilitiesSetHandler;
        }
    }
}