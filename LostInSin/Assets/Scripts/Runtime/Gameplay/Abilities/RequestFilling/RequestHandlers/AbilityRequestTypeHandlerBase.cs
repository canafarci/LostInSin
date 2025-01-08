using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	/// <summary>
	/// Base class implementing the chaining pattern.
	/// If this handler applies, it processes the request;
	/// then, to let other handlers also process the request,
	/// it calls the next handler in the chain.
	/// </summary>
	public abstract class AbilityRequestTypeHandlerBase : IAbilityRequestTypeHandler
	{
		private IAbilityRequestTypeHandler _nextHandler;

		public IAbilityRequestTypeHandler SetNext(IAbilityRequestTypeHandler next)
		{
			_nextHandler = next;
			return next;
		}

		public void Handle(AbilityRequest abilityRequest, PlayerAbilityRequestFiller context)
		{
			// 1) Check if this handler applies to the request type
			if (AppliesTo(abilityRequest.RequestType))
			{
				// 2) Execute specialized logic
				ProcessRequest(abilityRequest, context);
			}

			// 3) Pass the request to the next handler in the chain
			_nextHandler?.Handle(abilityRequest, context);
		}

		public abstract bool AppliesTo(AbilityRequestType requestType);

		/// <summary>
		/// Concrete classes implement their logic here.
		/// </summary>
		protected abstract void ProcessRequest(AbilityRequest abilityRequest, PlayerAbilityRequestFiller context);
	}
}