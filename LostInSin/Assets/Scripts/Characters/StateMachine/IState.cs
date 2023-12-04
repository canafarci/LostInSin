namespace LostInSin.Characters.StateMachine
{
    public interface IState
    {
        public void Tick();
        public void Enter();
        public void Exit();
    }
}
