using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.Signals.Characters;
using UniRx;
using Zenject;

namespace LostInSin.UI.AbilityPanel
{
    public class AbilityPanelModel : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<List<AbilityInfo>> _abilities = new();

        public ReactiveProperty<List<AbilityInfo>> Abilities => _abilities;

        public void Initialize()
        {
            _signalBus.GetStream<CharacterSelectSignal>()
                      .Subscribe(OnCharacterSelected)
                      .AddTo(_disposables);
        }

        private void OnCharacterSelected(CharacterSelectSignal signal)
        {
            _abilities.Value = signal.Character.Abilities;
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}