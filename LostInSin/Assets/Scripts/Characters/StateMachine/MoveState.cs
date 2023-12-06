using LostInSin.Input;
using LostInSin.Movement;
using UnityEngine;
using Zenject;
using UniRx;
using LostInSin.Raycast;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using LostInSin.Animation.Data;

namespace LostInSin.Characters.StateMachine
{
    public class MoveState : IState, IInitializable
    {
        [Inject(Id = CharacterStates.WaitState)] private readonly IState _waitState;
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
            if (!_mover.MovementStarted && _positionRaycaster.GetWorldPosition(out Vector3 position))
            {
                _mover.InitializeMovement(position);
                FireRunningAnimationChangeSignal(true);
            }
        }

        public void Enter()
        {
            FireRunningAnimationChangeSignal(false);
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
            {
                _signalBus.AbstractFire(new StateChangeSignal(_waitState));
            }
        }

        private void FireRunningAnimationChangeSignal(bool value)
        {
            AnimationChangeSignal animationChangeSignal = CreateRunningAnimationSignal(value);
            _signalBus.AbstractFire(animationChangeSignal);
        }

        private AnimationChangeSignal CreateRunningAnimationSignal(bool value)
        {
            AnimationStateChangeData animationChangeData = new();
            AnimationParameter<bool> animationParameter = new(value);

            animationChangeData.SetAnimationParameter(animationParameter);
            animationChangeData.SetAnimationIdentifier(AnimationIdentifier.IsRunning);

            AnimationChangeSignal animationChangeSignal = new(animationChangeData);
            return animationChangeSignal;
        }
    }
}