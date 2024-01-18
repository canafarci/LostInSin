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
            //if stack only contains move action, add to stack
            if (_abilityStack.Count == 1)
            {
                _abilityStack.Push(signal.Ability);
            }
            else //pop last element and add new elemetn
            {
                _abilityStack.Pop();
                _abilityStack.Push(signal.Ability);
            }

            if (_ability.AbilityBlueprint.IsUICastedAbility) CastAbility();
        }

        private void OnCharacterSelectedSignal(CharacterSelectedSignal signal)
        {
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
            AbilityBlueprint abilityAbilityBlueprint = _ability.AbilityBlueprint;
            AbilityCastResult abilityCastResult = AbilityCastResult.Fail;

            if (await abilityAbilityBlueprint.CanCast(_instigator))
            {
                (AbilityCastResult castResult, AbilityTarget target) target =
                    await abilityAbilityBlueprint.PreCast(_instigator);

                if (target.castResult == AbilityCastResult.Fail) return AbilityCastResult.Fail;

                await abilityAbilityBlueprint.Cast(_instigator, target.target);

                if (target.castResult == AbilityCastResult.Fail) return AbilityCastResult.Fail;

                await abilityAbilityBlueprint.PostCast(_instigator);
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