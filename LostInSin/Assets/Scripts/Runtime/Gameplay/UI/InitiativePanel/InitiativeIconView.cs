using UnityEngine;
using UnityEngine.UI;

namespace LostInSin.Runtime.Gameplay.UI.InitiativePanel
{
	public class InitiativeIconView : MonoBehaviour
	{
		[SerializeField] private Image Icon;
		[SerializeField] private Image HealthBarFillImage;

		public Image icon => Icon;
		public Image healthBarFillImage => HealthBarFillImage;
	}
}