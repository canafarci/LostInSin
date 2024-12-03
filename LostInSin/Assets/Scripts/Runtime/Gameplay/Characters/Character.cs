using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Gameplay.Grid.Data;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class Character : IInitializable
	{
		[Inject] private CharacterData _characterData;

		public Dictionary<StatID, int> currentStats { get; private set; }
		public Dictionary<StatID, int> maxStats { get; } = new();
		public string characterName { get; private set; }
		public GridCell currentCell { get; set; }
		public CharacterData characterData => _characterData;


		public List<Ability> Abilities;

		public void Initialize()
		{
			characterName = characterData.CharacterName;

			maxStats[StatID.Health] = characterData.MaxHealth;
			maxStats[StatID.ActionPoint] = characterData.MaxActionPoints;
			maxStats[StatID.Initiative] = characterData.Initiative;

			currentStats = new(maxStats);

			Abilities = new(characterData.Abilities);
		}
	}
}