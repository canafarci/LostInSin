using LostInSin.Characters;
using Sirenix.OdinInspector;

namespace LostInSin.Abilities.AbilityData.Abstract
{
    public abstract class AbilityTargetGetterVisual : SerializedScriptableObject
    {
        public abstract void TickVisual(Character instigator);
    }
}