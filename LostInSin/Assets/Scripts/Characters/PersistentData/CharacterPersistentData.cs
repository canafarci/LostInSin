using LostInSin.Abilities;
using Sirenix.OdinInspector;
using UnityEngine;
using LostInSin.Identifiers;
using Sirenix.Utilities;
using Zenject;

namespace LostInSin.Characters.PersistentData
{
    [CreateAssetMenu(fileName = "CharacterPersistentData", menuName = "Characters", order = 0)]
    public class CharacterPersistentData : SerializedScriptableObject
    {
        public void Inject(DiContainer container)
        {
            CharacterAbilities.ForEach(x => container.Inject(x.AbilityBlueprint));
            container.Inject(MoveAbility.AbilityBlueprint);
        }

        public bool DefaultSelectedCharacter = false;
        public CharacterClass CharacterClass;
        public AbilityInfo[] CharacterAbilities;
        public AbilityInfo MoveAbility;
    }
}