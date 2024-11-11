using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;

namespace LostInSin.Runtime.Gameplay.UI.AbilityPanel
{
	public interface IAbilityPanelMediator
	{
		void SetAbilityUI(List<Ability> abilities);
	}
}