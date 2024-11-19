using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Data;
using UnityEngine;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals
{
	public class AnimationConstants : IInitializable
	{
		private static CharacterAnimationData _characterAnimationData;

		public AnimationConstants(CharacterAnimationData characterAnimationData)
		{
			_characterAnimationData = characterAnimationData;
		}

		public static float movementSpeed => _characterAnimationData.MovementSpeed;
		public static float rotationSpeed => _characterAnimationData.RotationSpeed;

		public void Initialize() //this is here so that constructor gets called
		{
		}
	}
}