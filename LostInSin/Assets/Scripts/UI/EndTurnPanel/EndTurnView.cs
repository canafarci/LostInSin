using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LostInSin.UI.EndTurnPanel
{
    public class EndTurnView : MonoBehaviour
    {
        [Inject] private EndTurnVM _endTurnVM;

        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnEndTurnButtonClicked);
        }

        private void OnEndTurnButtonClicked()
        {
            _endTurnVM.EndTurnClickedHandler();
        }
    }
}