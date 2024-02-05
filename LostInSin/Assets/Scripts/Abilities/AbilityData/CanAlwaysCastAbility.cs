using LostInSin.Abilities.AbilityData.Abstract;
using LostInSin.Characters;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData
{
    [CreateAssetMenu(fileName = "Ability Requirements", menuName = "LostInSin/Abilities/Requirements", order = 0)]
    public class CanAlwaysCastAbility : AbilityRequirements
    {
        public override bool CanCast(Character instigator) => true;
    }
}