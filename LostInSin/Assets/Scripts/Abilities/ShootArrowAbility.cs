using Cysharp.Threading.Tasks;
using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Core;
using LostInSin.Identifiers;
using LostInSin.Signals;
using UnityEngine;
using Zenject;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Shoot Arrow Ability", menuName = "LostInSin/Abilities/Archer/Shoot Arrow Ability",
                     order = 0)]
    public class ShootArrowAbility : AbilityBlueprint
    {
        [Inject] private readonly PointerOverUIChecker _pointerOverUIChecker;

        public GameObject ArrowPrefab;

        public override UniTask<bool> CanCast(Character instigator) =>
            _pointerOverUIChecker.PointerIsOverUI ? new UniTask<bool>(false) : new UniTask<bool>(true);

        public override void OnAbilitySelected(Character instigator)
        {
            FireDrawArrowSignal(instigator, AnimationIdentifier.StartAimingArrow);
        }

        public override UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(Character instigator)
        {
            ArcherAnimationReference animationReference = instigator.AnimationReference as ArcherAnimationReference;
            Transform spawnTransform = animationReference.ArrowSpawnPoint;

            GameObject arrow = Instantiate(ArrowPrefab, spawnTransform);

            arrow.transform.localPosition = animationReference.ArrowSpawnPosition;
            arrow.transform.localRotation = animationReference.ArrowSpawnRotation;
            arrow.transform.localScale = animationReference.ArrowSpawnScale;

            throw new System.NotImplementedException();
        }

        public override UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target) =>
            throw new System.NotImplementedException();

        public override UniTask<bool> PostCast(Character instigator) => throw new System.NotImplementedException();

        public override void OnAbilityDeselected(Character instigator)
        {
            Debug.Log("called");
            FireDrawArrowSignal(instigator, AnimationIdentifier.CancelAimingArrow);
        }

        private void FireDrawArrowSignal(Character instigator, AnimationIdentifier abilityID)
        {
            AnimationChangeSignal animationChangeSignal = new AnimationChangeSignalBuilder()
                                                          .SetAnimationParameter(new byte()) //type hint for trigger
                                                          .SetAnimationIdentifier(abilityID)
                                                          .Build();

            instigator.SignalBus.AbstractFire(animationChangeSignal);
        }
    }
}