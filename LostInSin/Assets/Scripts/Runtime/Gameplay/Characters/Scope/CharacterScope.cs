using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.UI;
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
		[SerializeField] private CharacterVisualReferences CharacterVisualReferences;
		[SerializeField] private CharacterFacade CharacterFacade;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(CharacterData);
			builder.RegisterInstance(AnimancerComponent);
			builder.RegisterInstance(CharacterAvatar);
			builder.RegisterInstance(CharacterVisualReferences);
			builder.RegisterInstance(CharacterFacade);

			builder.RegisterEntryPoint<Character>().AsSelf();
			builder.RegisterEntryPoint<CharacterAnimationPlayer>().AsSelf();
			builder.RegisterEntryPoint<CharacterUIController>().AsSelf();
		}
	}
}