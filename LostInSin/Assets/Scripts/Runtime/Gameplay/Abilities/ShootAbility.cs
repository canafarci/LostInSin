// Assets/Scripts/Abilities/ShootAbility.cs

using LostInSin.Runtime.Gameplay.Characters;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities
{
	[CreateAssetMenu(fileName = "ShootAbility", menuName = "LostInSin/Abilities/Shoot Ability")]
	public class ShootAbility : Ability
	{
		public int damage;

		public override void Execute(Character user, Character target)
		{
			// Implement ability logic, such as reducing target's health
			target.TakeDamage(damage);

			// Consume action points
			user.UseActionPoints(ActionPointCost);

			// Additional logic (e.g., play animations, spawn projectiles)
		}
	}
}