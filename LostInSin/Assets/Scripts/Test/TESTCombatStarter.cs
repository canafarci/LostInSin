using LostInSin.Signals.Combat;
using UnityEngine;
using Zenject;

namespace LostInSin.Test
{
    public class TESTCombatStarter : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.C))
                _signalBus.Fire(new CombatStartedSignal());
        }
    }
}