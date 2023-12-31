using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using LostInSin.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace LostInSin.Camera
{
    public class CameraZoomer : ITickable
    {
        private readonly GameInput _gameInput;
        private readonly CameraModel _cameraModel;

        private CameraZoomer(GameInput gameInput,
                            CameraModel cameraModel)
        {
            _gameInput = gameInput;
            _cameraModel = cameraModel;
        }

        public void Tick()
        {
            CheckCameraMovementZoomAction();
        }

        private void CheckCameraMovementZoomAction()
        {
            InputAction cameraZoom = _gameInput.GameplayActions.CameraZoom;
            Vector2 zoomDirection = cameraZoom.ReadValue<Vector2>();
            Debug.Log(zoomDirection);

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
            float targetZoom = currentZoom + zoomDirection.y * Time.deltaTime;

            // Clamp the target zoom level within the min and max bounds
            targetZoom = Mathf.Clamp(minZoomDistance, targetZoom, maxZoomDistance);

            // Smoothly interpolate to the target zoom level
            float cameraZoomSpeed = _cameraModel.CameraZoomSpeed;
            float zoomDelta = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * cameraZoomSpeed);
            _cameraModel.CameraTransposer.m_FollowOffset.y = zoomDelta;
        }

    }
}
