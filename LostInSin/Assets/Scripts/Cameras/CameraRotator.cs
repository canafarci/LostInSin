using System;
using LostInSin.PlayerInput;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Cameras
{
    public class CameraRotator : ITickable, IInitializable, IDisposable
    {
        private readonly GameInput _gameInput;
        private readonly CameraModel _cameraModel;
        private Transform _cameraTargetTransform;
        private PollingState _currentState = PollingState.Inactive;

        private enum PollingState
        {
            Inactive,
            Polling
        }

        private CameraRotator(GameInput gameInput,
                              CameraModel cameraModel)
        {
            _gameInput = gameInput;
            _cameraModel = cameraModel;
        }

        public void Initialize()
        {
            _cameraTargetTransform = _cameraModel.CameraTarget.transform;

            _gameInput.GameplayActions.CameraRotationStartStop.performed += CameraStartRotationHandler;
            _gameInput.GameplayActions.CameraRotationStartStop.canceled += CameraStopRotationHandler;
        }

        private void CameraStopRotationHandler(InputAction.CallbackContext context)
        {
            _currentState = PollingState.Inactive;
        }

        private void CameraStartRotationHandler(InputAction.CallbackContext context)
        {
            _currentState = PollingState.Polling;
        }

        public void Tick()
        {
            if (_currentState == PollingState.Inactive) return;

            Vector2 mouseDelta = _gameInput.GameplayActions.CameraRotation.ReadValue<Vector2>();
            float rotationDelta = mouseDelta.x * Time.deltaTime * _cameraModel.CameraRotateSpeed;

            _cameraTargetTransform.Rotate(new Vector3(0f, rotationDelta, 0f));
        }

        public void Dispose()
        {
            _gameInput.GameplayActions.CameraRotationStartStop.performed -= CameraStartRotationHandler;
            _gameInput.GameplayActions.CameraRotationStartStop.canceled -= CameraStopRotationHandler;
        }
    }
}