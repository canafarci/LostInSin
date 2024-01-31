using System.Collections.Generic;
using LostInSin.Characters;
using LostInSin.Characters.PersistentData;

namespace LostInSin.Signals.Characters
{
    public readonly struct PlayableCharactersSpawnedSignal
    {
        private readonly List<Character> _characters;
        public List<Character> Characters => _characters;

        public PlayableCharactersSpawnedSignal(List<Character> characters)
        {
            _characters = characters;
        }
    }
}