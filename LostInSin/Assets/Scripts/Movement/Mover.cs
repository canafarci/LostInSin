using System;
using UnityEngine;
using Zenject;

namespace LostInSin.Movement
{
    public class Mover : IMover
    {
        private enum MovementState
        {
            Idle,
            Moving
        }

        private readonly float _movementDelta = Mathf.Pow(0.05f, 2);
        private readonly Transform _transform;
        private readonly Settings _settings;
        private Vector3 _target;
        private MovementState _currentState = MovementState.Idle;
        public bool IsMoving => _currentState == MovementState.Moving;

        public Mover(Transform transform, Settings settings)
        {
            _settings = settings;
            _transform = transform;
        }

        public void InitializeMovement(Vector3 target)
        {
            _target = target;
            _currentState = MovementState.Moving;
        }

        public bool Move()
        {
            if (_currentState != MovementState.Moving) return false;

            Vector3 normalizedDirection = CalculateNormalizedDirection();
            TurnTowards(normalizedDirection);
            MoveTowards(normalizedDirection);

            return HasReachedDestination();
        }

        private void TurnTowards(Vector3 normalizedDirection)
        {
            if (normalizedDirection != default)
            {
                Quaternion toRotation = Quaternion.LookRotation(normalizedDirection, Vector3.up);
                float interpolationFactor = _settings.TurnSpeed * Time.deltaTime;
                _transform.rotation = Quaternion.Slerp(_transform.rotation, toRotation, interpolationFactor);
            }
        }

        private void MoveTowards(Vector3 normalizedDirection)
        {
            Vector3 movementStep = _settings.MoveSpeed * Time.deltaTime * normalizedDirection;
            _transform.Translate(movementStep, Space.World);
        }

        private Vector3 CalculateNormalizedDirection()
        {
            Vector3 direction = _target - _transform.position;
            return direction.normalized;
        }

        private bool HasReachedDestination()
        {
            if (_currentState != MovementState.Moving) return false;

            bool reached = Vector3.SqrMagnitude(_transform.position - _target) <  Mathf.Pow(0.05f, 2);

            if (reached)
                _currentState = MovementState.Idle;

            return reached;
        }

        [Serializable]
        public struct Settings
        {
            public float MoveSpeed;
            public float TurnSpeed;
        }
    }
}