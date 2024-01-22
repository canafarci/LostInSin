using System.Threading;
using Cysharp.Threading.Tasks;
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

        public override UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            IAttribute healthAttribute = target.Character.AttributeSet.GetAttribute(AttributeIdentifiers.Health);
            healthAttribute.AddToValue(HealAmount);
            return new UniTask<AbilityCastResult>(AbilityCastResult.Fail);
        }

        public override UniTask<bool> CanCast(Character instigator, CancellationToken cancellationToken) => new(true);

        public override UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(
            Character instigator,
            CancellationToken cancellationToken)
        {
            AbilityCastResult castResult = AbilityCastResult.Success;
            AbilityTarget target = new() { Character = instigator };

            return new UniTask<(AbilityCastResult castResult, AbilityTarget target)>((castResult, target));
        }

        public override UniTask<AbilityCastResult> PostCast(Character instigator) => new(AbilityCastResult.Success);
    }
}