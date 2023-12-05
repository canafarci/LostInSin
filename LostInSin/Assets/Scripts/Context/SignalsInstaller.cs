using LostInSin.Characters.StateMachine;
using Zenject;

namespace LostInSin.Context
{
    public class SignalsInstaller : MonoInstaller<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}
