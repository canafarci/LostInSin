using UnityEngine;
using Zenject;

namespace LostInSin.Characters
{
    public class Character
    {
        private Transform _transform;

        private Character(Transform transform, Vector3 position)
        {
            _transform = transform;
            _transform.position = position;
        }

        public class Factory : PlaceholderFactory<Vector3, Character>
        {
        }
    }
}

