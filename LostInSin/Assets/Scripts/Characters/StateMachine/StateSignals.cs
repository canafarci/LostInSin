namespace LostInSin.Characters.StateMachine
{
    public class StateChangeSignal
    {
        public StateChangeSignal(IState targetState)
        {
            TargetState = targetState;
        }

        public IState TargetState { get; private set; }
    }
}
