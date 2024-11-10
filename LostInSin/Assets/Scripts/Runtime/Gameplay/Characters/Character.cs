using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class Character : IInitializable
	{
		[Inject] private CharacterData _characterData;

		public string CharacterName;
		public int MaxHealth;
		public int CurrentHealth;
		public int MaxActionPoints = 8;
		public int CurrentActionPoints;
		public int Initiative;
		public List<Ability> Abilities;

		public void Initialize()
		{
			CharacterName = _characterData.CharacterName;
			MaxHealth = _characterData.MaxHealth;
			CurrentHealth = MaxHealth;
			MaxActionPoints = _characterData.MaxActionPoints;
			Initiative = _characterData.Initiative;
			Abilities = new List<Ability>(_characterData.Abilities);
		}

		public void UseActionPoints(int amount)
		{
			CurrentActionPoints -= amount;
			if (CurrentActionPoints < 0)
				CurrentActionPoints = 0;
		}

		public void ResetActionPoints()
		{
			CurrentActionPoints = MaxActionPoints;
		}

		public void TakeDamage(int amount)
		{
			CurrentHealth -= amount;
			if (CurrentHealth <= 0)
			{
				CurrentHealth = 0;
				Die();
			}
		}

		private void Die()
		{
			// Handle death (e.g., remove from game, play animation)
		}
	}
}