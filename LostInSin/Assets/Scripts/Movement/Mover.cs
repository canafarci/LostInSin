using System;
using UnityEngine;
using Zenject;

namespace LostInSin.Movement
{
    public class Mover : IMover
    {
        private readonly Transform _transform;
        private readonly Settings _settings;
        private Vector3 _target;
        private bool _movementStarted = false;
        public bool MovementStarted { set { _movementStarted = value; } get { return _movementStarted; } }

        private Mover(Transform transform, Settings settings)
        {
            _settings = settings;
            _transform = transform;
        }

        public void InitializeMovement(Vector3 target)
        {
            _movementStarted = true;
            _target = target;
        }

        public void Move()
        {
            if (!_movementStarted) return;

            Vector3 normalizedDirection = CalculateNormalizedDirection();

            TurnTowards(normalizedDirection);
            MoveTowards(normalizedDirection);
        }

        private void TurnTowards(Vector3 normalizedDirection)
        {
            if (normalizedDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(normalizedDirection, Vector3.up);
                float interpolationFactor = _settings.TurnSpeed * Time.deltaTime;
                _transform.rotation = Quaternion.Slerp(_transform.rotation, toRotation, interpolationFactor);
            }
        }

        private void MoveTowards(Vector3 normalizedDirection)
        {
            Vector3 movementStep = _settings.MoveSpeed * Time.deltaTime * normalizedDirection;
            _transform.position += movementStep;
        }

        private Vector3 CalculateNormalizedDirection()
        {
            Vector3 direction = _target - _transform.position;
            Vector3 directionNormalized = direction.normalized;
            return directionNormalized;
        }

        public bool HasReachedDestination()
        {
            return _movementStarted && Vector3.SqrMagnitude(_transform.position - _target) < Mathf.Pow(0.02f, 2);
        }

        [Serializable]
        public struct Settings
        {
            public float MoveSpeed;
            public float TurnSpeed;
        }
    }
}