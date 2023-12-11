using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Characters.StateMachine
{
    public interface IStateTicker
    {
        public void Tick();
        public void SwitchToInactiveState();
    }
}
