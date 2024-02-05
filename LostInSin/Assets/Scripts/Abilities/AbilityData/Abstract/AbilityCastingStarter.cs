using LostInSin.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData.Abstract
{
    public abstract class AbilityCastingStarter : SerializedScriptableObject
    {
        public abstract void StartCasting(Character instigator);
    }
}