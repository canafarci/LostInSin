using LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling
{
	public interface IPlayerAbilityRequestFillerModel
	{
		public IAbilityRequestTypeHandler updateAbilityRequestTypeChain { get; set; }
		public IAbilityRequestTypeHandler fixedUpdateAbilityRequestTypeChain { get; set; }
	}

	public class PlayerAbilityRequestFillerModel : IPlayerAbilityRequestFillerModel
	{
		public IAbilityRequestTypeHandler updateAbilityRequestTypeChain { get; set; }
		public IAbilityRequestTypeHandler fixedUpdateAbilityRequestTypeChain { get; set; }
	}
}