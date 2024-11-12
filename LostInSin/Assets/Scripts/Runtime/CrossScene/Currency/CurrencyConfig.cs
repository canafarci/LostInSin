using System.Collections.Generic;
using LostInSin.Runtime.CrossScene.Enums;
using LostInSin.Runtime.Infrastructure.EnumFieldAdder;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.CrossScene.Currency
{
	[CreateAssetMenu(fileName = "Currency Config", menuName = "Infrastructure/Currency Config")]
	public class CurrencyConfig : SerializedScriptableObject
	{
		[InfoBox("Define in-game Currencies and their initial values")] [SerializeField]
		private Dictionary<CurrencyID, int> CurrencyDefaultValues = new();

		public Dictionary<CurrencyID, int> currencyDefaultValues => CurrencyDefaultValues;

		[Space(100f)] [InfoBox("You can use the input field below to add new Currency IDs")] [ShowInInspector]
		private EnumFieldAdder<CurrencyID> _currencyIDAdder = new();
	}
}