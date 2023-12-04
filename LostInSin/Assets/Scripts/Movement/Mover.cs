using System;
using UnityEngine;
using Zenject;

namespace LostInSin.Movement
{
    public class Mover : IMover
    {
        [Inject] private readonly Transform _transform;
        private readonly Settings _settings;
        private Vector3 _target;

        private Mover(Settings settings)
        {
            _settings = settings;
        }

        public void InitializeMovement(Vector3 target)
        {
            _target = target;
        }

        public void Move()
        {
            Vector3 normalizedDirection = CalculateNormalizedDirection();
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
            return Vector3.SqrMagnitude(_transform.position - _target) < Mathf.Pow(0.1f, 2);
        }

        [Serializable]
        public struct Settings
        {
            public float MoveSpeed;
        }
    }
}