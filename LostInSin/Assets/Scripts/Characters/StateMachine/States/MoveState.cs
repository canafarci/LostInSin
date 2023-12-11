using LostInSin.Input;
using LostInSin.Movement;
using UnityEngine;
using Zenject;
using UniRx;
using LostInSin.Raycast;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using LostInSin.Animation.Data;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace LostInSin.Characters.StateMachine
{
    public class MoveState : IState, IInitializable
    {
        [Inject(Id = CharacterStates.IdleState)] private readonly IState _idleState;
        [Inject] private CharacterStateRuntimeData _runtimeData;
        private readonly SignalBus _signalBus;
        private readonly IMover _mover;
        private readonly GameInput _gameInput;
        private readonly IPositionRaycaster _positionRaycaster;
        private bool _stateIsActive = false;

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
                if (!_stateIsActive) return;
                GetMovePosition();
            });
        }

        private async void GetMovePosition()
        {
            if (await CheckCanMoveAsync())
            {
                if (_positionRaycaster.GetWorldPosition(out Vector3 position))
                {
                    _runtimeData.CanExitTicking = false;
                    _mover.InitializeMovement(position);

                    FireRunningAnimationChangeSignal(true);
                }
            }
        }

        private async Task<bool> CheckCanMoveAsync()
        {
            await UniTask.NextFrame(); //wait one frame for state ticker to check other characters for switching
            return _runtimeData.IsTicking && !_mover.MovementStarted;
        }

        public void Enter()
        {
            _stateIsActive = true;
            FireRunningAnimationChangeSignal(false);
        }

        public void Exit()
        {
            _stateIsActive = false;
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
                _runtimeData.CanExitTicking = true;
                _signalBus.AbstractFire(new StateChangeSignal(_idleState));
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