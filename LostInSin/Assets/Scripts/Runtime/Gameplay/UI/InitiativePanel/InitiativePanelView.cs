using UnityEngine;

namespace LostInSin.Runtime.Gameplay.UI.InitiativePanel
{
	public class InitiativePanelView : MonoBehaviour
	{
		[SerializeField] private InitiativeIconView[] InitiativeIcons;

		public InitiativeIconView[] initiativeIcons => InitiativeIcons;
	}
}