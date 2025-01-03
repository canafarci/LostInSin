using System.Collections.Generic;
using LostInSin.Runtime.CrossScene.Enums;
using LostInSin.Runtime.CrossScene.Signals;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine.Assertions;

namespace LostInSin.Runtime.CrossScene.Currency
{
	public class CurrencyModel : ICurrencyModel
	{
		private readonly SignalBus _signalBus;
		private const string PERSISTENT_DATA_PATH = "PERSISTENT_DATA";
		private const string CURRENCY_KEY = "CURRENCY_KEY";

		private readonly Dictionary<CurrencyID, int> _currencyLookup;

		public CurrencyModel(SignalBus signalBus, CurrencyConfig currencyConfig)
		{
			_signalBus = signalBus;
			_currencyLookup = ES3.Load(CURRENCY_KEY, PERSISTENT_DATA_PATH, currencyConfig.currencyDefaultValues);
		}

		public int GetCurrencyValue(CurrencyID currencyID)
		{
			Assert.IsTrue(_currencyLookup.ContainsKey(currencyID),
				$"CurrencyID {currencyID} does not exist in persistent data!");
			return _currencyLookup[currencyID];
		}

		public void AddCurrency(CurrencyID currencyID, int change)
		{
			Assert.IsTrue(change >= 0, "Trying to Add negative change! use TryPayCost() instead!");
			Assert.IsTrue(_currencyLookup.ContainsKey(currencyID),
				$"CurrencyID {currencyID} does not exist in persistent data!");

			_currencyLookup[currencyID] += change;
			ES3.Save(CURRENCY_KEY, _currencyLookup, PERSISTENT_DATA_PATH);
			_signalBus.Fire(new CurrencyChangedSignal());
		}

		public bool TryPayCost(CurrencyID currencyID, int cost)
		{
			Assert.IsTrue(_currencyLookup.ContainsKey(currencyID),
				$"CurrencyID {currencyID} does not exist in persistent data!");

			if (_currencyLookup[currencyID] >= cost)
			{
				_currencyLookup[currencyID] -= cost;
				ES3.Save(CURRENCY_KEY, _currencyLookup, PERSISTENT_DATA_PATH);
				_signalBus.Fire(new CurrencyChangedSignal());
				return true;
			}

			return false;
		}
	}
}