using LostInSin.Abilities.AbilityData.Abstract;
using LostInSin.Animation;
using LostInSin.Characters;
using UnityEngine;

namespace LostInSin.Abilities.AbilityData.CastingStarter
{
    [CreateAssetMenu(fileName = "Spawn Arrow Casting Start", menuName = "LostInSin/Abilities/Casting Start/Spawn Arrow Casting Start", order = 0)]
    public class SpawnArrowCastingStarter : AbilityCastingStarter
    {
        public GameObject ArrowPrefab;

        private GameObject _arrow;
        public GameObject Arrow => _arrow;

        public override void StartCasting(Character instigator)
        {
            ArcherAnimationReference animationReference = instigator.AnimationReference as ArcherAnimationReference;
            Transform spawnTransform = animationReference.ArrowSpawnPoint;
            
            if (Arrow == null)
                CreateArrow(spawnTransform, animationReference);
            else
                Arrow.SetActive(true);
        }
        
        private void CreateArrow(Transform spawnTransform, ArcherAnimationReference animationReference)
        {
            _arrow = Instantiate(ArrowPrefab, spawnTransform);
            Arrow.transform.localPosition = animationReference.ArrowSpawnPosition;
            Arrow.transform.localRotation = animationReference.ArrowSpawnRotation;
            Arrow.transform.localScale = animationReference.ArrowSpawnScale;
        }
    }
}