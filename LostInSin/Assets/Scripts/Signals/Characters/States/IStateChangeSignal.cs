using LostInSin.Characters.StateMachine.States;

namespace LostInSin.Signals.Characters.States
{
    public interface IStateChangeSignal
    {
        public IState TargetState { get; }
    }
}