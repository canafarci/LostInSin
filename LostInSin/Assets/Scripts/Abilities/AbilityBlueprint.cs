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
        public bool IsPointTargeted;

        public abstract UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target);
        public abstract UniTask<bool> CanCast(Character instigator, AbilityTarget target);
    }
}