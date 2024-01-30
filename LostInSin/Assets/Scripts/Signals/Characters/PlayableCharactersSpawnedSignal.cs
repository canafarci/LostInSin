using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;

namespace LostInSin.Signals.Characters
{
    public readonly struct PlayableCharactersSpawnedSignal
    {
        private readonly Dictionary<CharacterPersistentData, Character> _characters;
        public Dictionary<CharacterPersistentData, Character> Characters => _characters;

        public PlayableCharactersSpawnedSignal(Dictionary<CharacterPersistentData, Character> characters)
        {
            _characters = characters;
        }
    }
}