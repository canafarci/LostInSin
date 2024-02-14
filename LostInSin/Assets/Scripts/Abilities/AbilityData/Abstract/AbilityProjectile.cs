using System.Collections.Generic;
using LostInSin.Characters;
using Sirenix.OdinInspector;

namespace LostInSin.Abilities.AbilityData.Abstract
{
    public abstract class AbilityProjectile : SerializedScriptableObject
    {
        public abstract bool MoveProjectile(Character instigator, List<AbilityTarget> targets);
    }
}