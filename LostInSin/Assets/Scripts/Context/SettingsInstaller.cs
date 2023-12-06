using System.Collections.Generic;
using LostInSin.Animation;
using LostInSin.Identifiers;
using LostInSin.Movement;
using UnityEngine;
using Zenject;

namespace LostInSin.Context
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public Mover.Settings Mover;
        public AnimationHashes.Data AnimationData;
        public override void InstallBindings()
        {
            Container.BindInstances(Mover, AnimationData);
        }
    }
}