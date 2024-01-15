using LostInSin.AbilitySystem;

namespace LostInSin.Abilities
{
    public interface IAbilityHolder
    {
        public AbilitySet AbilitySet { get; }
        public AttributeSet AttributeSet { get; }
    }
}