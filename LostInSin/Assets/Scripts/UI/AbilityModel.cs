using System;
using LostInSin.Abilities;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityModel : IInitializable
    {
        private AbilityInfo[] _abilities = new[]
                                           {
                                               new AbilityInfo() { Name = "Move" },
                                               new AbilityInfo() { Name = "Potion" },
                                               new AbilityInfo() { Name = "Attack" },
                                               new AbilityInfo() { Name = "Riposte" }
                                           };

        public event Action<AbilityInfo[]> OnAbilitiesSet;

        [Inject] private AbilityViewModel _viewModel;

        public void Initialize()
        {
            OnAbilitiesSet?.Invoke(_abilities);
        }
    }
}