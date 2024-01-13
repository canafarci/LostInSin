using LostInSin.Characters.PersistentData;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters
{
    public class CharacterSpawner : IInitializable
    {
        private readonly Character.Factory _characterFactory;
        private Data _data;

        private CharacterSpawner(Character.Factory characterFactory, Data data)
        {
            _characterFactory = characterFactory;
            _data = data;
        }

        public void Initialize()
        {
            Spawn();
        }

        private void Spawn()
        {
            foreach (CharacterPersistentData character in _data.SavedCharacters.PersistentCharacters)
            {
                Vector2 randPos = Random.insideUnitCircle * 5f;
                Vector3 pos = new(randPos.x, 0f, randPos.y);
                _characterFactory.Create(pos);
            }
        }

        public class Data
        {
            public SavedCharacters SavedCharacters;
        }
    }
}