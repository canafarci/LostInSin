using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	/// <summary>
	/// The interface for handling AbilityRequestType-based logic.
	/// Each handler can process a request if it matches certain flags.
	/// </summary>
	public interface IAbilityRequestTypeHandler
	{
		/// <summary>
		/// Checks if this handler applies to the given request type (e.g., if the type has certain flags).
		/// </summary>
		public bool AppliesTo(AbilityRequestType requestType);

		/// <summary>
		/// Handle any logic relevant to the given AbilityRequest.
		/// If your chain should stop processing once handled, you'll return a bool or omit calling Next.
		/// </summary>
		public void Handle(AbilityRequest abilityRequest);

		/// <summary>
		/// Allows chaining the next handler.
		/// </summary>
		public IAbilityRequestTypeHandler SetNext(IAbilityRequestTypeHandler next);
	}
}