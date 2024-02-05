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

        public override void StartCasting(Character instigator)
        {
            ArcherAnimationReference animationReference = instigator.AnimationReference as ArcherAnimationReference;
            Transform spawnTransform = animationReference.ArrowSpawnPoint;
            
            if (_arrow == null)
                CreateArrow(spawnTransform, animationReference);
            else
                _arrow.SetActive(true);
        }
        
        private void CreateArrow(Transform spawnTransform, ArcherAnimationReference animationReference)
        {
            _arrow = Instantiate(ArrowPrefab, spawnTransform);
            _arrow.transform.localPosition = animationReference.ArrowSpawnPosition;
            _arrow.transform.localRotation = animationReference.ArrowSpawnRotation;
            _arrow.transform.localScale = animationReference.ArrowSpawnScale;
        }
    }
}