using LostInSin.Abilities.AbilityData.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "LostInSin/Abilities/AbilityData", order = 0)]

    public class AbilityTemplate : SerializedScriptableObject
    {
        public AbilityCastingStarter[] CastingStarters;
    }
}