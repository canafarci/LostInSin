using Cinemachine;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;
using LostInSin.Input;
using System;


namespace LostInSin.Camera
{
    public class CameraMover : ITickable
    {
        private readonly GameInput _gameInput;
        private readonly CameraModel _cameraModel;

        private CameraMover(GameInput gameInput,
                            CameraModel cameraModel)
        {
            _gameInput = gameInput;
            _cameraModel = cameraModel;
        }

        public void Tick()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            CheckMouseBorderMovement(mousePosition);
            CheckCameraMovementInputAction();
        }

        private void CheckMouseBorderMovement(Vector2 mousePosition)
        {
            float borderThreshold = 10; // Define how close to the edge it should react

            if (mousePosition.x < borderThreshold || mousePosition.x > Screen.width - borderThreshold ||
                mousePosition.y < borderThreshold || mousePosition.y > Screen.height - borderThreshold)
            {
                Vector2 direction = GetDirectionBasedOnMousePosition(mousePosition);
                MoveCamera(direction);
            }
        }

        private Vector2 GetDirectionBasedOnMousePosition(Vector2 mousePosition)
        {
            Vector2 direction = Vector2.zero;

            // Check horizontal movement
            if (mousePosition.x < 10) direction.x = -1;
            else if (mousePosition.x > Screen.width - 10) direction.x = 1;

            // Check vertical movement
            if (mousePosition.y < 10) direction.y = -1;
            else if (mousePosition.y > Screen.height - 10) direction.y = 1;

            return direction;
        }

        private void CheckCameraMovementInputAction()
        {
            InputAction cameraMovement = _gameInput.GameplayActions.CameraMovement;
            Vector2 movementDirection = cameraMovement.ReadValue<Vector2>();

            if (movementDirection == default) return; //there is no input

            MoveCamera(movementDirection);
        }

        private void MoveCamera(Vector2 direction)
        {
            // Calculate the movement step
            Vector3 movement = new Vector3(direction.x, 0, direction.y);
            Vector3 movementStep = _cameraModel.CameraMoveSpeed * Time.deltaTime * movement;
            // Move the camera target
            Transform cameraTargetTransform = _cameraModel.CameraTarget.transform;
            cameraTargetTransform.Translate(movementStep, Space.World);
        }

    }
}
