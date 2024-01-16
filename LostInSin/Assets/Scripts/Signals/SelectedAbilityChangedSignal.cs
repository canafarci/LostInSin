using LostInSin.Abilities;

namespace LostInSin.Signals
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