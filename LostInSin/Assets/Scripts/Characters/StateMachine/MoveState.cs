using LostInSin.Input;
using LostInSin.Movement;
using UnityEngine;
using Zenject;
using UniRx;
using LostInSin.Raycast;

namespace LostInSin.Characters.StateMachine
{
    public class MoveState : IState, IInitializable
    {
        [Inject(Id = CharacterState.WaitState)] private readonly IState _waitState;
        private readonly SignalBus _signalBus;
        private readonly IMover _mover;
        private readonly GameInput _gameInput;
        private readonly IPositionRaycaster _positionRaycaster;

        private MoveState(SignalBus signalBus,
                          IMover mover,
                          GameInput gameInput,
                          IPositionRaycaster positionRaycaster)
        {
            _signalBus = signalBus;
            _mover = mover;
            _gameInput = gameInput;
            _positionRaycaster = positionRaycaster;
        }

        public void Initialize()
        {
            _gameInput.ClickStream.Subscribe(ctx =>
            {
                GetMovePosition();
            });
        }

        private void GetMovePosition()
        {
            if (_positionRaycaster.GetWorldPosition(out Vector3 position))
            {
                _mover.InitializeMovement(position);
            }
        }

        public void Enter()
        {

        }

        public void Exit()
        {
            _mover.MovementStarted = false;
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