using LostInSin.Input;
using LostInSin.Movement;
using UnityEngine;
using Zenject;
using LostInSin.Raycast;
using LostInSin.Characters.StateMachine.Signals;
using LostInSin.Identifiers;
using LostInSin.Animation.Data;
using Cysharp.Threading.Tasks;
using LostInSin.Grid;
using UnityEngine.InputSystem;

namespace LostInSin.Characters.StateMachine
{
    public class MoveState : IState, IInitializable
    {
        // Injected dependencies
        [Inject(Id = CharacterStates.IdleState)] private readonly IState _idleState;
        [Inject] private readonly CharacterStateRuntimeData _runtimeData;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IMover _mover;
        [Inject] private readonly GameInput _gameInput;
        [Inject] private readonly IPositionRaycaster _positionRaycaster;
        [Inject] private readonly IGridPositionConverter _gridPositionConverter;
        private enum StateActivity { Active, Inactive }
        private StateActivity _currentState = StateActivity.Inactive;


        public void Initialize()
        {
            _gameInput.GameplayActions.Click.performed += OnClickPerformed;
        }

        public void Enter()
        {
            _currentState = StateActivity.Active;
            UpdateRunningAnimation(false);
        }

        public void Exit()
        {
            _currentState = StateActivity.Inactive;
        }

        public void Tick()
        {
            CheckForStateTransition();
            _mover.Move();
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            if (_currentState == StateActivity.Inactive) return;
            HandlePlayerClick();
        }

        private async void HandlePlayerClick()
        {
            if (await IsMovePossibleAsync())
            {
                TryMoveCharacter();
            }
        }

        private async UniTask<bool> IsMovePossibleAsync()
        {
            await UniTask.NextFrame(); // Wait one frame for state ticker to check
            return _runtimeData.IsTicking && !_mover.IsMoving;
        }

        private void TryMoveCharacter()
        {
            if (TryGetTargetGridCell(out GridCellData gridCell))
            {
                StartCharacterMovement(gridCell);
            }
        }

        private bool TryGetTargetGridCell(out GridCellData gridCell)
        {
            gridCell = default;
            return _positionRaycaster.GetWorldPosition(out Vector3 position) &&
                   _gridPositionConverter.GetCell(position, out gridCell) &&
                   !gridCell.IsOccupied;
        }

        private void StartCharacterMovement(GridCellData gridCell)
        {
            _runtimeData.CanExitTicking = false;
            _runtimeData.ChangeOccupiedCell(gridCell);

            _mover.InitializeMovement(gridCell.CenterPosition);
            UpdateRunningAnimation(true);
        }

        private void CheckForStateTransition()
        {
            if (_mover.HasReachedDestination())
            {
                _runtimeData.CanExitTicking = true;
                ChangeStateToIdle();
            }
        }

        private void ChangeStateToIdle()
        {
            _signalBus.AbstractFire(new StateChangeSignal(_idleState));
        }

        private void UpdateRunningAnimation(bool isRunning)
        {
            var animationChangeSignal = CreateRunningAnimationSignal(isRunning);
            _signalBus.AbstractFire(animationChangeSignal);
        }

        private AnimationChangeSignal CreateRunningAnimationSignal(bool isRunning)
        {
            var animationChangeData = new AnimationStateChangeData();
            var animationParameter = new AnimationParameter<bool>(isRunning);
            animationChangeData.SetAnimationParameter(animationParameter);
            animationChangeData.SetAnimationIdentifier(AnimationIdentifier.IsRunning);

            return new AnimationChangeSignal(animationChangeData);
        }
    }
}
