using Cysharp.Threading.Tasks;
using LostInSin.Characters;
using LostInSin.Identifiers;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Null Ability", menuName = "LostInSin/Abilities/Null Ability Object", order = 0)]
    public class AbilityNullObject : AbilityBlueprint
    {
        public override UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            Debug.LogWarning($"Tried to use NULL ability object, caller is {instigator}, target is {target}",
                             instigator.gameObject);

            return new UniTask<AbilityCastResult>(AbilityCastResult.Fail);
        }

        public override UniTask<bool> CanCast(Character instigator) => new(false);

        public override UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(Character instigator)
        {
            AbilityCastResult castResult = AbilityCastResult.Fail;
            AbilityTarget target = default;
            return new UniTask<(AbilityCastResult castResult, AbilityTarget target)>((castResult, target));
        }

        public override UniTask<bool> PostCast(Character instigator) => new(false);
    }
}