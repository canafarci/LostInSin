using System.Collections.Generic;
using LostInSin.Abilities.AbilityData.Abstract;
using LostInSin.Characters;
using LostInSin.Identifiers;
using LostInSin.Raycast;
using UnityEngine;
using Zenject;

namespace LostInSin.Abilities.AbilityData.TargetGetter
{
    [CreateAssetMenu(fileName = "Enemy Target Selector",
                     menuName = "LostInSin/Abilities/Target Selector/Enemy Target Selector",
                     order = 0)]
    public class CharacterTargetSelector : AbilityTargetSelector
    {
        public int CountOfTargets;

        [Inject] private readonly IComponentRaycaster<Character> _characterRaycaster;
        private const int CHARACTER_LAYER_MASK = 1 << 6;
        private List<AbilityTarget> _abilityTargets = new();

        public override List<AbilityTarget> GetTarget(Character instigator)
        {
            if (_characterRaycaster.RaycastComponent(out Character character, CHARACTER_LAYER_MASK) &&
                character.CharacterPersistentData.CharacterTeam == CharacterTeam.Enemy)
            {
                AbilityTarget target = new() { Character = character };
                _abilityTargets.Add(target);

                if (_abilityTargets.Count == CountOfTargets)
                {
                    List<AbilityTarget> abilityTargets = _abilityTargets;
                    _abilityTargets.Clear();

                    return abilityTargets;
                }
            }

            return null;
        }
    }
}