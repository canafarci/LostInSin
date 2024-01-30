using LostInSin.Abilities;

namespace LostInSin.Signals.Abilities
{
    public readonly struct SelectedAbilityChangedSignal
    {
        private readonly AbilityInfo _ability;

        public readonly AbilityInfo Ability => _ability;

        public SelectedAbilityChangedSignal(AbilityInfo ability)
        {
            _ability = ability;
        }
    }
}