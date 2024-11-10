using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Infrastructure.Commands;
using UnityEngine;
using UnityEngine.Serialization;

namespace LostInSin.Runtime.Gameplay.Abilities
{
	public abstract class Ability : ScriptableObject, IAbilityCommand
	{
		public string AbilityName;
		public int ActionPointCost;
		public Sprite Icon;

		protected Character _user;
		protected Character _target;

		public void Initialize(Character user, Character target)
		{
			_user = user;
			_target = target;
		}

		/// <summary>
		/// Executes the ability command.
		/// </summary>
		/// <param name="user">The character using the ability.</param>
		/// <param name="target">The target of the ability.</param>
		public abstract void Execute(Character user, Character target);
	}
}