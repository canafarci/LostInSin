using System;
using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals.UI
{
	public class CharacterUIController : IInitializable, IDisposable
	{
		private readonly CharacterVisualReferences _visualReferences;
		private readonly Character _character;
		private readonly CharacterFacade _characterFacade;

		public CharacterUIController(CharacterVisualReferences visualReferences,
			Character character,
			CharacterFacade characterFacade)
		{
			_visualReferences = visualReferences;
			_character = character;
			_characterFacade = characterFacade;
		}

		public void Initialize()
		{
			_characterFacade.OnCharacterHealthChanged += CharacterHealthChangedHandler;
			UpdateHealthBar();
		}

		private void CharacterHealthChangedHandler()
		{
			UpdateHealthBar();
		}

		private void UpdateHealthBar()
		{
			if (_character.currentStats[StatID.Health] <= 0)
			{
				DisableHealthBar();
				return;
			}

			_visualReferences.healthBarFillImage.fillAmount = _character.currentStats[StatID.Health] / (float)_character.maxStats[StatID.Health];
			_visualReferences.healthText.text = $"{_character.currentStats[StatID.Health]}/{_character.maxStats[StatID.Health]}";
		}

		private void DisableHealthBar()
		{
			_visualReferences.healthText.gameObject.SetActive(false);
			_visualReferences.healthText.gameObject.SetActive(false);
		}

		public void Dispose()
		{
			_characterFacade.OnCharacterHealthChanged -= CharacterHealthChangedHandler;
		}
	}
}