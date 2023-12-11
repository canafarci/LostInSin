using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Characters.StateMachine
{
    public interface ISelectionChangeSignal
    {
        public bool Selected { get; }
    }
}
