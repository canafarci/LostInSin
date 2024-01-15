using LostInSin.Attributes;
using LostInSin.Characters;
using LostInSin.Identifiers;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Heal Potion", menuName = "LostInSin/Abilities/Heal Potion", order = 0)]
    public class HealPotionAbility : AbilityBlueprint
    {
        public float HealAmount;

        public override void ApplyEffect(Character instigator, AbilityTarget target)
        {
            IAttribute healthAttribute = target.Character.AttributeSet.GetAttribute(AttributeIdentifiers.Health);
            healthAttribute.AddToValue(HealAmount);
        }
    }
}