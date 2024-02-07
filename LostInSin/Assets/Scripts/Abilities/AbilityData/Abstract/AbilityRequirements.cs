using LostInSin.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData.Abstract
{
    public abstract class AbilityRequirements : SerializedScriptableObject
    {
        public abstract bool CanCast(Character instigator);
    }
}