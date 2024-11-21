using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals.Animations
{
	public class AnimationBone : MonoBehaviour
	{
		[SerializeField] private AnimationBoneID AnimationBoneID;

		public AnimationBoneID BoneID => AnimationBoneID;
	}
}