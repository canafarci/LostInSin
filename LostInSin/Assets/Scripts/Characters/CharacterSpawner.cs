using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LostInSin.Characters.PersistentData;
using LostInSin.Identifiers;
using LostInSin.Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace LostInSin.Characters
{
    public class CharacterSpawner : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly Character.Factory _characterFactory;
        private readonly SavedCharacters _savedCharacters;
        private readonly Dictionary<CharacterPersistentData, Character> _playerCharacters = new();

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

                if (character.CharacterTeam == CharacterTeam.Friendly)
                    _playerCharacters[characterData] = character;

                FireInitialSelectedCharacterSignal(characterData, character);
            }

            FireCharactersSpawnedSignal();
        }

        private async void FireInitialSelectedCharacterSignal(CharacterPersistentData characterData, Character character)
        {
            if (characterData.DefaultSelectedCharacter)
            {
                await UniTask.DelayFrame(1); //wait one frame to finish initialization
                _signalBus.Fire(new SelectInitialCharacterSignal(character));
            }
        }

        private async void FireCharactersSpawnedSignal()
        {
            await UniTask.DelayFrame(1); //wait one frame to finish initialization
            PlayableCharactersSpawnedSignal playableCharactersSpawnedSignal = new(_playerCharacters);
            _signalBus.Fire(playableCharactersSpawnedSignal);
        }

        public void Dispose()
        {
            _playerCharacters.Clear();
        }
    }
}