using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.UI.AbilityPanel
{
	public class AbilityPanelMediator : IAbilityPanelMediator, IDisposable
	{
		[Inject] private List<AbilityView> _abilityViews;

		public event Action<Ability> OnAbilityClicked;

		public void SetAbilityUI(List<Ability> abilities)
		{
			RemoveViewClickListeners();

			for (int i = 0; i < abilities.Count; i++)
			{
				SetupView(_abilityViews[i], abilities[i]);
			}

			for (int i = abilities.Count; i < _abilityViews.Count; i++)
			{
				_abilityViews[i].gameObject.SetActive(false);
			}
		}

		private void SetupView(AbilityView abilityView, Ability ability)
		{
			abilityView.gameObject.SetActive(true);

			abilityView.abilityIcon.sprite = ability.Icon;
			abilityView.abilityText.text = ability.AbilityName;

			abilityView.abilityButton.onClick.AddListener(() => OnAbilityClicked?.Invoke(ability));
		}

		private void RemoveViewClickListeners()
		{
			foreach (AbilityView view in _abilityViews)
			{
				view.abilityButton.onClick.RemoveAllListeners();
			}
		}

		public void Dispose()
		{
			RemoveViewClickListeners();
		}
	}
}