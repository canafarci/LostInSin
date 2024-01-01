using LostInSin.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Camera
{
    public class CameraZoomer : ITickable, IInitializable
    {
        private readonly GameInput _gameInput;
        private readonly CameraModel _cameraModel;
        private float _targetZoom;

        private CameraZoomer(GameInput gameInput,
                            CameraModel cameraModel)
        {
            _gameInput = gameInput;
            _cameraModel = cameraModel;
        }

        public void Tick()
        {
            CheckCameraMovementZoomAction();
            InterpolateCameraZoom();
        }

        public void Initialize()
        {
            _targetZoom = _cameraModel.CameraTransposer.m_FollowOffset.y;
        }

        private void CheckCameraMovementZoomAction()
        {
            InputAction cameraZoom = _gameInput.GameplayActions.CameraZoom;
            Vector2 zoomDirection = cameraZoom.ReadValue<Vector2>();

            if (zoomDirection == default) return; //there is no input

            ZoomCamera(zoomDirection);
        }
        private void ZoomCamera(Vector2 zoomDirection)
        {
            float minZoomDistance = _cameraModel.CameraZoomMinDistance;
            float maxZoomDistance = _cameraModel.CameraZoomMaxDistance;

            // Current follow offset
            float currentZoom = _cameraModel.CameraTransposer.m_FollowOffset.y;

            // Calculate target zoom level based on input direction
            _targetZoom = currentZoom + zoomDirection.y;

            // Clamp the target zoom level within the min and max bounds
            _targetZoom = Mathf.Clamp(_targetZoom, minZoomDistance, maxZoomDistance);
        }

        private void InterpolateCameraZoom()
        {
            float currentZoom = _cameraModel.CameraTransposer.m_FollowOffset.y;

            if (Mathf.Approximately(_targetZoom, currentZoom)) return;

            // Smoothly interpolate to the target zoom level
            float cameraZoomSpeed = _cameraModel.CameraZoomSpeed;
            float zoomDelta = Mathf.Lerp(currentZoom, _targetZoom, Time.deltaTime * cameraZoomSpeed);

            _cameraModel.CameraTransposer.m_FollowOffset.y = zoomDelta;
        }
    }
}
