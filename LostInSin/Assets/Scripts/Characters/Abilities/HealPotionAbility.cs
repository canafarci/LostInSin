using UnityEngine;

namespace LostInSin.Characters.Abilities
{
    [CreateAssetMenu(fileName = "AbilityBlueprint", menuName = "LostInSin/Abilities", order = 0)]
    public class HealPotionAbility : AbilityBlueprint
    {
        public float HealAmount;

        public override void ApplyEffect(Character instigator, AbilityTarget target)
        {
            //target.Character.AttributeSet
        }
    }
}