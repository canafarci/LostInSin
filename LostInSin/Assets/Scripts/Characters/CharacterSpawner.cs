using UnityEngine;
using Zenject;

namespace LostInSin.Characters
{
    public class CharacterSpawner : IInitializable
    {
        private readonly Character.Factory _characterFactory;

        private CharacterSpawner(Character.Factory characterFactory)
        {
            _characterFactory = characterFactory;
        }

        public void Initialize()
        {
            _characterFactory.Create(Vector3.zero);
        }
    }
}


