using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters.Scope
{
	public class CharacterScope : LifetimeScope
	{
		[SerializeField] private CharacterData CharacterData;
		[SerializeField] private AnimancerComponent AnimancerComponent;
		[SerializeField] private Avatar CharacterAvatar;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(CharacterData);
			builder.RegisterInstance(AnimancerComponent);
			builder.RegisterInstance(CharacterAvatar);

			builder.RegisterEntryPoint<Character>().AsSelf();
			builder.RegisterEntryPoint<CharacterAnimationPlayer>().AsSelf();
		}
	}
}