using LostInSin.Characters;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Null Ability", menuName = "LostInSin/Abilities/Null Ability Object", order = 0)]
    public class AbilityNullObject : AbilityBlueprint
    {
        public override void ApplyEffect(Character instigator, AbilityTarget target)
        {
            Debug.LogWarning($"Tried to use NULL ability object, caller is {instigator}, target is {target}",
                             instigator.gameObject);
        }
    }
}