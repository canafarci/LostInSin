using System.Collections.Generic;
using DG.Tweening;
using LostInSin.Abilities.AbilityData.Abstract;
using LostInSin.Abilities.AbilityData.CastingStarter;
using LostInSin.Characters;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData.Projectile
{
    [CreateAssetMenu(fileName = "Moving Arrow Projectile",
                     menuName = "LostInSin/Abilities/Projectile/Moving Arrow Projectile", order = 0)]
    public class MovingArrowProjectile : AbilityProjectile
    {
        public SpawnArrowCastingStarter SpawnArrowCastingStarter;

        public override bool MoveProjectile(Character instigator, List<AbilityTarget> targets)
        {
            Transform arrow = SpawnArrowCastingStarter.Arrow.transform;
            Vector3 target = targets[0].Character.AnimationReference.HitTarget.position;
            arrow.transform.DOMove(target, .5f).SetEase(Ease.OutExpo);

            return false;
        }
    }
}