using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters.Scope
{
	public class PlayerCharacterScope : LifetimeScope
	{
		[SerializeField] private CharacterData CharacterData;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(CharacterData);

			builder.RegisterEntryPoint<Character>().AsSelf();
		}
	}
}