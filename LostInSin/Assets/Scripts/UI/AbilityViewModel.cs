using System;
using LostInSin.Characters.Abilities;
using UniRx;
using Zenject;

namespace LostInSin.UI
{
    public class AbilityViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveCommand<AbilityInfo[]> OnAbilityInfoReceived = new();
        [Inject] private readonly AbilityModel _model;

        private AbilityInfo[] _abilities;

        public void Initialize()
        {
            _model.OnAbilitiesSet += AbilitiesSetHandler;
        }

        public void AbilitiesSetHandler(AbilityInfo[] abilities)
        {
            _abilities = abilities;
            OnAbilityInfoReceived.Execute(abilities);
        }

        public void Dispose()
        {
            _model.OnAbilitiesSet -= AbilitiesSetHandler;
        }
    }
}