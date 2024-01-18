using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LostInSin.Abilities;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace LostInSin.AbilitySystem
{
    public class AbilitySystemManager : IInitializable, IDisposable
    {
        [Inject] private readonly SignalBus _signalBus;

        private readonly CompositeDisposable _disposables = new();

        private Character _instigator;
        private readonly Stack<AbilityInfo> _abilityStack = new();
        private AbilityInfo _ability => _abilityStack.Peek();

        public void Initialize()
        {
            _signalBus.GetStream<SelectedAbilityChangedSignal>()
                      .Subscribe(OnSelectedAbilityChanged)
                      .AddTo(_disposables);

            _signalBus.GetStream<CharacterSelectedSignal>()
                      .Subscribe(OnCharacterSelectedSignal)
                      .AddTo(_disposables);
        }

        private void OnSelectedAbilityChanged(SelectedAbilityChangedSignal signal)
        {
            Debug.Log(_abilityStack.Count);
            //if stack only contains move action, add to stack
            if (_abilityStack.Count == 1)
            {
                _abilityStack.Push(signal.Ability);
                Debug.Log("asddd1");
            }
            else //pop last element and add the new element
            {
                AbilityInfo ability = _abilityStack.Pop();
                ability.AbilityBlueprint.OnAbilityDeselected(_instigator);

                _abilityStack.Push(signal.Ability);

                Debug.Log("asddd2");
            }

            _ability.AbilityBlueprint.OnAbilitySelected(_instigator);

            if (_ability.AbilityBlueprint.IsUICastedAbility)
                CastAbility();

            Debug.Log(_abilityStack.Count);
        }

        private void OnCharacterSelectedSignal(CharacterSelectedSignal signal)
        {
            _abilityStack.Clear();

            _instigator = signal.SelectedCharacter;
            AbilityInfo moveAbility = signal.SelectedCharacter
                                            .Abilities
                                            .Find(x => x.AbilityIdentifier == AbilityIdentifiers.Move);
            _abilityStack.Push(moveAbility);
        }

        /// <summary>
        /// Casts the ability represented by the current AbilityBlueprint.
        /// </summary>
        /// <returns>Returns the result of the ability cast.</returns>
        public async UniTask<AbilityCastResult> CastAbility()
        {
            AbilityBlueprint abilityBlueprint = _ability.AbilityBlueprint;
            AbilityCastResult abilityCastResult = AbilityCastResult.Fail;

            if (await abilityBlueprint.CanCast(_instigator))
            {
                (AbilityCastResult castResult, AbilityTarget target) target = await abilityBlueprint.PreCast(_instigator);

                if (target.castResult == AbilityCastResult.Fail)
                    return AbilityCastResult.Fail;

                await abilityBlueprint.Cast(_instigator, target.target);

                if (target.castResult == AbilityCastResult.Fail)
                    return AbilityCastResult.Fail;

                await abilityBlueprint.PostCast(_instigator);
            }

            if (_abilityStack.Count > 1)
                _abilityStack.Pop();

            return abilityCastResult;
        }

        public void Dispose()
        {
            _disposables.Clear();
        }
    }
}