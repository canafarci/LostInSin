using LostInSin.Runtime.Gameplay.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LostInSin.Runtime.Gameplay.UI.AbilityPanel
{
	public class AbilityView : MonoBehaviour
	{
		[SerializeField] private Button AbilityButton;
		[SerializeField] private int ButtonIndex;
		[SerializeField] private TextMeshProUGUI AbilityText;
		[SerializeField] private Image AbilityIcon;

		public Button abilityButton => AbilityButton;
		public int buttonIndex => ButtonIndex;
		public TextMeshProUGUI abilityText => AbilityText;
		public Image abilityIcon => AbilityIcon;
	}
}