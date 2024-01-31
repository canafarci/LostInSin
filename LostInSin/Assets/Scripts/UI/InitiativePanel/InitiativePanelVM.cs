using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Signals.UI;
using UniRx;
using UnityEngine;
using Zenject;


namespace LostInSin.UI.InitiativePanel
{
    public class InitiativePanelVM : ViewModelBase, IInitializable
    {
        [Inject] private InitiativePanelView _panelView;
        [Inject] private InitiativePanelIconView.Pool _initiativePanelIconViewPool;

        private const int PANEL_ICON_COUNT = 9;
        private int _currentPanelIconCount = 0;
        private LinkedList<Character> _orderedCharacters;

        public void Initialize()
        {
            _signalBus.GetStream<SetupInitiativePanelSignal>()
                      .Subscribe(OnSetupInitiativePanel)
                      .AddTo(_disposables);
        }

        private void OnSetupInitiativePanel(SetupInitiativePanelSignal signal)
        {
            _orderedCharacters = signal.Characters;
            _panelView.InitativePanelHolder.gameObject.SetActive(true);

            LinkedListNode<Character> currentNode = _orderedCharacters.First;
            while (_currentPanelIconCount < PANEL_ICON_COUNT)
            {
                if (currentNode == null) currentNode = _orderedCharacters.First;

                _initiativePanelIconViewPool.Spawn(_panelView.InitativePanelHolder, currentNode.Value);

                currentNode = currentNode.Next;
                _currentPanelIconCount++;
            }
        }
    }
}