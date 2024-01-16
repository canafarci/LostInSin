using Cysharp.Threading.Tasks;
using LostInSin.Characters;
using LostInSin.Identifiers;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Null Ability", menuName = "LostInSin/Abilities/Null Ability Object", order = 0)]
    public class AbilityNullObject : AbilityBlueprint
    {
        public override async UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            Debug.LogWarning($"Tried to use NULL ability object, caller is {instigator}, target is {target}",
                             instigator.gameObject);

            return AbilityCastResult.Fail;
        }

        public override UniTask<bool> CanCast(Character instigator, AbilityTarget target) => new(false);
    }
}