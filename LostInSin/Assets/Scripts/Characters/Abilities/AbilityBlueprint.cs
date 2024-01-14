using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Characters.Abilities
{
    [CreateAssetMenu(fileName = "AbilityBlueprint", menuName = "LostInSin/Abilities", order = 0)]
    public abstract class AbilityBlueprint : SerializedScriptableObject
    {
        public abstract void ApplyEffect(Character instigator, AbilityTarget target);
    }
}