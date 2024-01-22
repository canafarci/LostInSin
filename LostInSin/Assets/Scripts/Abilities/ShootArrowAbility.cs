using System;
using Cysharp.Threading.Tasks;
using LostInSin.Animation;
using LostInSin.Characters;
using LostInSin.Core;
using LostInSin.Identifiers;
using LostInSin.Raycast;
using LostInSin.Signals;
using UnityEngine;
using Zenject;
using DG.Tweening;
using LostInSin.Input;
using UnityEngine.InputSystem;

namespace LostInSin.Abilities
{
    [CreateAssetMenu(fileName = "Shoot Arrow Ability", menuName = "LostInSin/Abilities/Archer/Shoot Arrow Ability",
                     order = 0)]
    public class ShootArrowAbility : AbilityBlueprint
    {
        [Inject] private readonly PointerOverUIChecker _pointerOverUIChecker;
        [Inject] private readonly IComponentRaycaster<Character> _characterRaycaster;
        [Inject] private readonly GameInput _gameInput;

        public GameObject ArrowPrefab;

        private const int CHARACTER_LAYER_MASK = 1 << 6;
        private GameObject _arrow;
        private AbilityState _state = AbilityState.Inactive;
        private AbilityTarget _target = default;

        private enum AbilityState
        {
            Inactive,
            SelectingTarget,
            SelectedTarget
        }

        public override void Initialize()
        {
            _gameInput.GameplayActions.Click.performed += OnClickPerformed;
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            if (_state is AbilityState.Inactive or AbilityState.SelectedTarget) return;

            if (_characterRaycaster.RaycastComponent(out Character character, CHARACTER_LAYER_MASK))
            {
                Debug.Log(character);
                _target = new AbilityTarget() { Character = character };
                _state = AbilityState.SelectedTarget;
            }
        }

        public override void OnAbilitySelected(Character instigator)
        {
            FireDrawArrowSignal(instigator, AnimationIdentifier.StartAimingArrow);
        }

        public override UniTask<bool> CanCast(Character instigator) =>
            _pointerOverUIChecker.PointerIsOverUI ? new UniTask<bool>(false) : new UniTask<bool>(true);

        public override async UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(Character instigator)
        {
            AbilityCastResult castResult = AbilityCastResult.Fail;

            ArcherAnimationReference animationReference = instigator.AnimationReference as ArcherAnimationReference;
            Transform spawnTransform = animationReference.ArrowSpawnPoint;

            if (_arrow == null)
                _arrow = Instantiate(ArrowPrefab, spawnTransform);

            _arrow.transform.localPosition = animationReference.ArrowSpawnPosition;
            _arrow.transform.localRotation = animationReference.ArrowSpawnRotation;
            _arrow.transform.localScale = animationReference.ArrowSpawnScale;

            _state = AbilityState.SelectingTarget;

            await UniTask.WaitUntil(() => _state == AbilityState.SelectedTarget);

            castResult = AbilityCastResult.InProgress;

            return (castResult, _target);
        }

        public override async UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            FireDrawArrowSignal(instigator, AnimationIdentifier.ShootArrow);
            await UniTask.Delay(500);


            _arrow.transform.parent = null;

            _arrow.transform.LookAt(target.Character.transform);
            await _arrow.transform.DOMove(target.Character.transform.position, 1f).SetEase(Ease.OutExpo);
            return AbilityCastResult.InProgress;
        }

        public override UniTask<bool> PostCast(Character instigator)
        {
            Debug.Log("ARROW HIT");
            Debug.Log("ARROW HIT2");
            return new UniTask<bool>(true);
        }

        public override void OnAbilityDeselected(Character instigator)
        {
            FireDrawArrowSignal(instigator, AnimationIdentifier.CancelAimingArrow);
            _state = AbilityState.Inactive;
            _target = default;
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