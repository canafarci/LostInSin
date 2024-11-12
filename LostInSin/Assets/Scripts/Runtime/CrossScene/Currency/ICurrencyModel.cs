using LostInSin.Runtime.CrossScene.Enums;

namespace LostInSin.Runtime.CrossScene.Currency
{
	public interface ICurrencyModel
	{
		int GetCurrencyValue(CurrencyID currencyID);
		void AddCurrency(CurrencyID currencyID, int change);
		bool TryPayCost(CurrencyID currencyID, int cost);
	}
}