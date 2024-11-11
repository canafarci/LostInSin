using UnityEngine;
using UnityEngine.UI;

namespace LostInSin.Runtime.Gameplay.Turns
{
	public class TurnView : MonoBehaviour
	{
		[SerializeField] private Button EndTurnButton;

		public Button endTurnButton => EndTurnButton;
	}
}