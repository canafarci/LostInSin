using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Characters.StateMachine
{
    public class ListenForInputState //: IState
    {
        // [Inject(Id = CharacterState.MoveState)] private IState _moveState;
        // private SignalBus _signalBus;
        // private float _waitDuration;

        // private WaitState(SignalBus signalBus)
        // {
        //     _signalBus = signalBus;
        // }

        // public void Enter()
        // {
        //     _waitDuration = Random.Range(1f, 3f);
        // }

        // public void Exit()
        // {
        // }

        // public void Tick()
        // {
        //     CheckTransition();

        //     _waitDuration -= Time.deltaTime;
        // }

        // private void CheckTransition()
        // {
        //     if (_waitDuration <= 0f)
        //         _signalBus.Fire(new StateChangeSignal(_moveState));
        // }
    }
}
