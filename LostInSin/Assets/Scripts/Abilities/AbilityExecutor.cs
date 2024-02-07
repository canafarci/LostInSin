using LostInSin.Characters;
using LostInSin.Identifiers;
using Zenject;

namespace LostInSin.Abilities
{
    public class AbilityExecutor
    {
        [Inject] private Character _character;

        private AbilityInfo _currentAbilityInfo;

        public AbilityCastResult Tick() => default;
    }
}