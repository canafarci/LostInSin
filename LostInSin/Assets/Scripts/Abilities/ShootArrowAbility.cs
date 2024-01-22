using System;
using System.Threading;
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
using LostInSin.Attributes;
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
        [Inject] private readonly IPositionRaycaster _positionRaycaster;

        public float BaseDamage = 15f;
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
            if (_state is AbilityState.Inactive or AbilityState.SelectedTarget ||
                _pointerOverUIChecker.PointerIsOverUI) return;

            if (_characterRaycaster.RaycastComponent(out Character character, CHARACTER_LAYER_MASK))
            {
                _target = new AbilityTarget() { Character = character };
                _state = AbilityState.SelectedTarget;
            }
        }

        public override UniTask<bool> CanCast(Character instigator, CancellationToken cancellationToken)
        {
            FireDrawArrowSignal(instigator, AnimationIdentifier.StartAimingArrow);
            return new UniTask<bool>(true);
        }

        public override async UniTask<(AbilityCastResult castResult, AbilityTarget target)> PreCast(
            Character instigator,
            CancellationToken cancellationToken)
        {
            try
            {
                AbilityCastResult castResult = AbilityCastResult.Fail;

                ArcherAnimationReference animationReference = instigator.AnimationReference as ArcherAnimationReference;
                Transform spawnTransform = animationReference.ArrowSpawnPoint;

                if (_arrow == null)
                    CreateArrow(spawnTransform, animationReference);
                else
                    _arrow.SetActive(true);

                _state = AbilityState.SelectingTarget;

                MoveCharacterTowardsTarget(instigator, cancellationToken);
                await UniTask.WaitUntil(() => _state == AbilityState.SelectedTarget,
                                        cancellationToken: cancellationToken);

                castResult = AbilityCastResult.InProgress;
                return (castResult, _target);
            }
            catch (Exception)
            {
                ResetArrowPosition(instigator);
                return (AbilityCastResult.Fail, _target);
            }
        }

        public override async UniTask<AbilityCastResult> Cast(Character instigator, AbilityTarget target)
        {
            FireDrawArrowSignal(instigator, AnimationIdentifier.ShootArrow);
            await UniTask.Delay(100);

            _arrow.transform.parent = null;

            _arrow.transform.LookAt(target.Character.transform);
            await _arrow.transform.DOMove(target.Character.AnimationReference.HitTarget.position, .5f).SetEase(Ease.OutExpo);
            IAttribute targetHealth = _target.Character.AttributeSet.GetAttribute(AttributeIdentifiers.Health);
            targetHealth.AddToValue(-1f * BaseDamage);
            return AbilityCastResult.InProgress;
        }

        public override UniTask<AbilityCastResult> PostCast(Character instigator)
        {
            ResetArrowPosition(instigator);
            return new UniTask<AbilityCastResult>(AbilityCastResult.Success);
        }

        public override void OnAbilityDeselected(Character instigator)
        {
            FireDrawArrowSignal(instigator, AnimationIdentifier.CancelAimingArrow);
            _state = AbilityState.Inactive;
            _target = default;
        }

        private async void MoveCharacterTowardsTarget(Character instigator, CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.WaitUntil(() =>
                                        {
                                            _positionRaycaster.GetWorldPosition(out Vector3 position);
                                            Vector3 direction = CalculateNormalizedDirection(position, instigator.transform);
                                            TurnTowards(direction, instigator.transform);
                                            return _state == AbilityState.SelectedTarget;
                                        },
                                        cancellationToken: cancellationToken);
            }
            catch (Exception)
            {
            }
        }

        private Vector3 CalculateNormalizedDirection(Vector3 target, Transform instigator)
        {
            Vector3 direction = target - instigator.position;
            return direction.normalized;
        }

        private void TurnTowards(Vector3 normalizedDirection, Transform instigator)
        {
            if (normalizedDirection != default)
            {
                Quaternion toRotation = Quaternion.LookRotation(normalizedDirection, Vector3.up);
                toRotation *= Quaternion.Euler(new Vector3(0f, 90f, 0f));
                float interpolationFactor = 7f * Time.deltaTime;
                instigator.rotation = Quaternion.Slerp(instigator.rotation, toRotation, interpolationFactor);
            }
        }

        private void CreateArrow(Transform spawnTransform, ArcherAnimationReference animationReference)
        {
            _arrow = Instantiate(ArrowPrefab, spawnTransform);
            _arrow.transform.localPosition = animationReference.ArrowSpawnPosition;
            _arrow.transform.localRotation = animationReference.ArrowSpawnRotation;
            _arrow.transform.localScale = animationReference.ArrowSpawnScale;
        }

        private void ResetArrowPosition(Character instigator)
        {
            ArcherAnimationReference animationReference = instigator.AnimationReference as ArcherAnimationReference;
            Transform spawnTransform = animationReference.ArrowSpawnPoint;
            _arrow.transform.parent = spawnTransform;
            _arrow.transform.localPosition = animationReference.ArrowSpawnPosition;
            _arrow.transform.localRotation = animationReference.ArrowSpawnRotation;
            _arrow.SetActive(false);
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