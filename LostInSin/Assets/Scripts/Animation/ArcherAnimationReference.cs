using UnityEngine;

namespace LostInSin.Animation
{
    public class ArcherAnimationReference : AnimationReference
    {
        [SerializeField] private Transform _arrowSpawnPoint;
        [SerializeField] private Vector3 _arrowSpawnPosition;
        [SerializeField] private Quaternion _arrowSpawnRotation;
        [SerializeField] private Vector3 _arrowSpawnScale;

        public Transform ArrowSpawnPoint => _arrowSpawnPoint;

        public Vector3 ArrowSpawnPosition => _arrowSpawnPosition;

        public Quaternion ArrowSpawnRotation => _arrowSpawnRotation;

        public Vector3 ArrowSpawnScale => _arrowSpawnScale;
    }
}