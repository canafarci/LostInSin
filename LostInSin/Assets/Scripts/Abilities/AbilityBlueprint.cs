using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LostInSin.Abilities.AbilityData.Abstract;
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
        public bool IsUICastedAbility;

        public AbilityCastingStarter[] CastingStarters;
        public AbilityRequirements[] AbilityRequirements;
        public AbilityTargetSelector AbilityTargetSelector;
    }
}