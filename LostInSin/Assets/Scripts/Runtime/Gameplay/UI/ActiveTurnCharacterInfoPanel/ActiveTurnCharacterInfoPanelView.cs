using TMPro;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.UI.ActiveTurnCharacterInfoPanel
{
	public class ActiveTurnCharacterInfoPanelView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI CharacterNameText;
		[SerializeField] private TextMeshProUGUI CharacterAPText;

		public TextMeshProUGUI characterAPText => CharacterAPText;
		public TextMeshProUGUI characterNameText => CharacterNameText;
	}
}