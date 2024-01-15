using LostInSin.Characters;
using LostInSin.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "AbilityBlueprint", menuName = "LostInSin/Abilities", order = 0)]
    public abstract class AbilityBlueprint : SerializedScriptableObject
    {
        public AbilityIdentifiers AbilityIdentifier;
        public abstract void ApplyEffect(Character instigator, AbilityTarget target);
    }
}