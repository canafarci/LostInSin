using System;
using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.Signals;
using UniRx;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityPanelModel : IInitializable, IDisposable
    {
        [Inject] private AbilityPanelViewModel _panelViewModel;
        [Inject] private SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<List<AbilityInfo>> _abilities = new();

        public ReactiveProperty<List<AbilityInfo>> Abilities => _abilities;

        public void Initialize()
        {
            _signalBus.GetStream<CharacterSelectedSignal>()
                      .Subscribe(OnCharacterSelected)
                      .AddTo(_disposables);
        }

        private void OnCharacterSelected(CharacterSelectedSignal signal)
        {
            _abilities.Value = signal.SelectedCharacter.Abilities;
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}