public interface IState
{
    StateMachine Fsm { get; set; }

    void InitState();
    
    void Enter();
    void UpdateState(float deltaTime);
    void Exit();
}
