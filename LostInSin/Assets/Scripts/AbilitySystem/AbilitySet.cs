using System.Collections.Generic;
using LostInSin.Abilities;
using LostInSin.Characters.PersistentData;

namespace LostInSin.AbilitySystem
{
    public class AbilitySet
    {
        private readonly List<AbilityInfo> _characterAbilities;

        public List<AbilityInfo> CharacterAbilities => _characterAbilities;

        public AbilitySet(CharacterPersistentData persistentData)
        {
            _characterAbilities = new List<AbilityInfo>(persistentData.CharacterAbilities);
        }
    }
}