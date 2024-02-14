using LostInSin.Abilities.AbilityData.Abstract;
using LostInSin.Characters;
using LostInSin.Raycast;
using UnityEngine;
using Zenject;

namespace LostInSin.Abilities.AbilityData.TargetGetter.Visual
{
    [CreateAssetMenu(fileName = "Turn Character Towards Pointer",
                     menuName = "LostInSin/Abilities/Target Getter/Visual/Turn Character Towards Pointer", order = 0)]
    public class TurnCharacterTowardsPointer : AbilityTargetSelectorVisual
    {
        [Inject] private readonly IPositionRaycaster _positionRaycaster;

        public override void TickVisual(Character instigator)
        {
            if (_positionRaycaster.GetWorldPosition(out Vector3 position))
            {
                Vector3 direction = CalculateNormalizedDirection(position, instigator.transform);
                TurnTowards(direction, instigator.transform);
            }
        }

        private Vector3 CalculateNormalizedDirection(Vector3 target, Transform instigator)
        {
            Vector3 direction = target - instigator.position;
            direction.y = instigator.transform.position.y;
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
    }
}