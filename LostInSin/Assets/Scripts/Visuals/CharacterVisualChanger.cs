using System;
using System.Collections;
using System.Collections.Generic;
using LostInSin.Characters.StateMachine;
using UniRx;
using Zenject;

namespace LostInSin.Visuals
{
    public class CharacterVisualChanger : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly CharacterVisualsVO _characterVisualsVO;
        readonly private CompositeDisposable _disposables = new();

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
