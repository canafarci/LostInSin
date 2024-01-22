using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LostInSin.Characters;
using LostInSin.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "AbilityBlueprint", menuName = "LostInSin/Abilities", order = 0)]
    public abstract class AbilityBlueprint : SerializedScriptableObject
    {
        public AbilityIdentifiers AbilityIdentifier;
        public bool IsUICastedAbility;

        public virtual void Initialize()
        {
        }

        public virtual void OnAbilitySelected(Character instigator)
        {
        }

        public abstract UniTask<bool> CanCast(Character instigator, CancellationToken cancellationToken);

        public abstract UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(
            Character instigator,
            CancellationToken cancellationToken);

        public abstract UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target);
        public abstract UniTask<AbilityCastResult> PostCast(Character instigator);

        public virtual void OnAbilityDeselected(Character instigator)
        {
        }
    }
}