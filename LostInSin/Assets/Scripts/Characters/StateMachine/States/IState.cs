namespace LostInSin.Characters.StateMachine.States
{
    public interface IState
    {
        public void Tick();
        public void Enter();
        public void Exit();
    }
}