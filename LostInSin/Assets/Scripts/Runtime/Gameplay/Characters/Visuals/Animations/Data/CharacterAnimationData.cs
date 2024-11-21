using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Data
{
	[CreateAssetMenu(fileName = "Character Animation Data", menuName = "LostInSin/Animations/Character Animation Data")]
	public class CharacterAnimationData : SerializedScriptableObject
	{
		public float MovementSpeed;
		public float RotationSpeed;
	}
}