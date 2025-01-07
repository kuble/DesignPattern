using UnityEngine;

public class JumpState : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    public Blackboard_Default Blackboard { get; set; }
    
    public void InitState(IBlackboardBase blackboard)
    {
        Blackboard = blackboard as Blackboard_Default;
    }

    public void Enter()
    {
        Blackboard.animator.CrossFade("Jump", 0.1f);
        Blackboard.GetComponent<Rigidbody>().velocity = new Vector3(Blackboard.GetComponent<Rigidbody>().velocity.x, Blackboard.JumpForce, Blackboard.GetComponent<Rigidbody>().velocity.z);
    }

    public void UpdateState(float deltaTime)
    {
        if (Blackboard.GetComponent<Rigidbody>().velocity.y == 0.0f)
        {
            Fsm.ChangeState<IdleState>();
        }
    }

    public void Exit()
    {
    }
}