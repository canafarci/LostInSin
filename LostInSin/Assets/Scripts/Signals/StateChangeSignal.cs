using LostInSin.Characters.StateMachine.States;

namespace LostInSin.Signals
{
    public class StateChangeSignal : IStateChangeSignal
    {
        public StateChangeSignal(IState targetState)
        {
            TargetState = targetState;
        }

        public IState TargetState { get; private set; }
    }
}