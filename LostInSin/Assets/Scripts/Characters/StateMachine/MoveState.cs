using LostInSin.Movement;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters.StateMachine
{
    public class MoveState : IState
    {
        [Inject(Id = CharacterState.WaitState)] private readonly IState _waitState;
        private readonly SignalBus _signalBus;
        private readonly IMover _mover;

        private MoveState(SignalBus signalBus, IMover mover)
        {
            _signalBus = signalBus;
            _mover = mover;
        }

        public void Enter()
        {
            Vector2 randomPos = Random.insideUnitCircle * 5f;
            _mover.InitializeMovement(new Vector3(randomPos.x, 0f, randomPos.y));
        }

        public void Exit()
        {

        }

        public void Tick()
        {
            CheckTransition();

            _mover.Move();
        }

        private void CheckTransition()
        {
            if (_mover.HasReachedDestination())
                _signalBus.Fire(new StateChangeSignal(_waitState));
        }
    }
}