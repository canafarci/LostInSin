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
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 randPos = Random.insideUnitCircle * 5f;
                Vector3 pos = new(randPos.x, 0f, randPos.y);
                _characterFactory.Create(pos);
            }
        }
    }
}


