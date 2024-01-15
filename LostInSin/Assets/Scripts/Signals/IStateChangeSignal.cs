using LostInSin.Characters.StateMachine.States;

namespace LostInSin.Signals
{
    public interface IStateChangeSignal
    {
        public IState TargetState { get; }
    }
}