using LostInSin.Abilities.AbilityData.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "LostInSin/Abilities/Ability Blueprint", order = 0)]
    public class AbilityBlueprint : SerializedScriptableObject
    {
        public AbilityRequirements[] AbilityRequirements;
        public AbilityCastingStarter[] CastingStarters;
        public AbilityTargetSelector AbilityTargetSelector;
        public AbilityTargetSelectorVisual AbilityTargetSelectorVisual;
    }
}