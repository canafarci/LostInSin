using LostInSin.Signals.Combat;
using Zenject;

namespace LostInSin.UI.EndTurnPanel
{
    public class EndTurnVM : ViewModelBase, IInitializable
    {
        public void Initialize()
        {
        }

        public void EndTurnClickedHandler()
        {
            _signalBus.Fire(new EndTurnSignal());
        }
    }
}