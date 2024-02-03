using System;
using LostInSin.Signals.Combat;
using UnityEngine;
using Zenject;

namespace LostInSin.Test
{
    public class TESTCombat : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        private void Start()
        {
            _signalBus.Fire(new CombatStartedSignal());
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.C))
                _signalBus.Fire(new EndTurnSignal());
        }
    }
}