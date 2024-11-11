using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class Character : IInitializable
	{
		[Inject] private CharacterData _characterData;

		public string characterName { get; private set; }
		public int maxHealth { get; private set; }
		public int currentHealth { get; private set; }
		public int maxActionPoints { get; private set; }
		public int currentActionPoints { get; private set; }
		public bool isPlayerCharacter { get; private set; }
		public int initiative { get; private set; }
		public List<Ability> Abilities;

		public void Initialize()
		{
			characterName = _characterData.CharacterName;
			maxHealth = _characterData.MaxHealth;
			currentHealth = maxHealth;
			maxActionPoints = _characterData.MaxActionPoints;
			initiative = _characterData.Initiative;
			isPlayerCharacter = _characterData.IsPlayerCharacter;
			Abilities = new List<Ability>(_characterData.Abilities);
		}

		public void UseActionPoints(int amount)
		{
			currentActionPoints -= amount;
			if (currentActionPoints < 0)
				currentActionPoints = 0;
		}

		public void ResetActionPoints()
		{
			currentActionPoints = maxActionPoints;
		}

		public void TakeDamage(int amount)
		{
			currentHealth -= amount;
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Die();
			}
		}

		private void Die()
		{
			// Handle death (e.g., remove from game, play animation)
		}
	}
}