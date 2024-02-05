using LostInSin.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData.Abstract
{
    [CreateAssetMenu(fileName = "Ability Requirements", menuName = "LostInSin/Abilities", order = 0)]
    public abstract class AbilityRequirements : SerializedScriptableObject
    {
        public abstract bool CanCast(Character instigator);
    }
}