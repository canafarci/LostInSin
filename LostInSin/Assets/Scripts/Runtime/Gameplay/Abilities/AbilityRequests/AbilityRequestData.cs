using LostInSin.Runtime.Gameplay.Characters;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public class AbilityRequestData
	{
		public Character User;

		public void Reset()
		{
			User = null;
		}
	}
}