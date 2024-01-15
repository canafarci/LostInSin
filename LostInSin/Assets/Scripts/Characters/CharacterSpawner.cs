using Cysharp.Threading.Tasks;
using LostInSin.Characters.PersistentData;
using LostInSin.Signals;
using UnityEngine;
using Zenject;

namespace LostInSin.Characters
{
    public class CharacterSpawner : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly Character.Factory _characterFactory;
        private readonly SavedCharacters _savedCharacters;

        private CharacterSpawner(Character.Factory characterFactory,
                                 SavedCharacters savedCharacters,
                                 SignalBus signalBus)
        {
            _characterFactory = characterFactory;
            _savedCharacters = savedCharacters;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Spawn();
        }

        private void Spawn()
        {
            foreach (CharacterPersistentData characterData in _savedCharacters.PersistentCharacters)
            {
                Vector2 randPos = Random.insideUnitCircle * 5f;
                Vector3 pos = new(randPos.x, 0f, randPos.y);
                Character character = _characterFactory.Create(pos, characterData);

                FireInitialSelectedCharacterSignal(characterData, character);
            }
        }

        private async void FireInitialSelectedCharacterSignal(CharacterPersistentData characterData, Character character)
        {
            if (characterData.DefaultSelectedCharacter)
            {
                await UniTask.DelayFrame(1); //wait one frame to finish initialization
                _signalBus.Fire(new SelectInitialCharacterSignal(character));
            }
        }
    }
}