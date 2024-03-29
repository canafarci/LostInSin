using System.Collections.Generic;
using LostInSin.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData.Abstract
{
    public abstract class AbilityTargetSelector : SerializedScriptableObject
    {
        public abstract List<AbilityTarget> GetTarget(Character instigator);
    }
}