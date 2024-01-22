using UnityEngine;

namespace LostInSin.Animation
{
    public class AnimationReference : MonoBehaviour
    {
        [SerializeField] protected Transform _hitTarget;
        public Transform HitTarget => _hitTarget;
    }
}