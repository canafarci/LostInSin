using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Grid.Data;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class Character : IInitializable
	{
		[Inject] private CharacterData _characterData;

		public string characterName { get; private set; }
		public int currentHealth { get; private set; }
		public int currentActionPoints { get; private set; }
		public int initiative { get; private set; }
		public GridCell currentCell { get; set; }
		public CharacterData characterData => _characterData;

		public List<Ability> Abilities;

		public void Initialize()
		{
			characterName = characterData.CharacterName;
			currentHealth = characterData.MaxHealth;
			currentActionPoints = characterData.MaxActionPoints;
			initiative = characterData.Initiative;
			Abilities = new List<Ability>(characterData.Abilities);
		}

		public void UseActionPoints(int amount)
		{
			currentActionPoints -= amount;
		}

		public void ResetActionPoints()
		{
			currentActionPoints = characterData.MaxActionPoints;
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