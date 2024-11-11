using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using VContainer;

namespace LostInSin.Runtime.Gameplay.UI.AbilityPanel
{
	public class AbilityPanelMediator : IAbilityPanelMediator
	{
		[Inject] private List<AbilityView> _abilityViews;

		public void SetAbilityUI(List<Ability> abilities)
		{
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
		}
	}
}