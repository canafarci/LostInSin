using System.Collections;
using System.Collections.Generic;
using LostInSin.Characters.StateMachine.States;
using UnityEngine;

namespace LostInSin.Characters.StateMachine
{
    public interface IStateChangeSignal
    {
        public IState TargetState { get; }
    }
}