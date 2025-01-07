public interface IState
{
    StateMachine Fsm { get; set; }
    public Blackboard_Default Blackboard { get; set; }

    void InitState(IBlackboardBase blackboard);
    
    void Enter();
    void UpdateState(float deltaTime);
    void Exit();
}
