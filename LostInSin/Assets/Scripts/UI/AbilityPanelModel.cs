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

        private List<AbilityInfo> _abilities;

        public event Action<List<AbilityInfo>> OnAbilitiesSet;


        public void Initialize()
        {
            _signalBus.GetStream<CharacterSelectedSignal>()
                      .Subscribe(OnInitialCharacterSelect)
                      .AddTo(_disposables);
        }

        private void OnInitialCharacterSelect(CharacterSelectedSignal signal)
        {
            _abilities = signal.SelectedCharacter.Abilities;
            OnAbilitiesSet?.Invoke(_abilities);
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}