using System;
using LostInSin.Signals;
using LostInSin.Signals.Characters;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.Visuals
{
    public class CharacterVisualChanger : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly CharacterVisualsVO _characterVisualsVO;
        private readonly CompositeDisposable _disposables = new();

        public CharacterVisualChanger(SignalBus signalBus, CharacterVisualsVO characterVisualsVO)
        {
            _signalBus = signalBus;
            _characterVisualsVO = characterVisualsVO;
        }

        public void Initialize()
        {
            _signalBus.GetStream<ISelectionChangeSignal>()
                      .Subscribe(x => ChangeVisual(x.Selected))
                      .AddTo(_disposables);
        }

        private void ChangeVisual(bool selected)
        {
            _characterVisualsVO.SelectionDecal.SetActive(selected);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}