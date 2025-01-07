using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    public Blackboard_Default Blackboard { get; set; }
    public void InitState(IBlackboardBase blackboard)
    {
        Blackboard = blackboard as Blackboard_Default;
    }

    public void Enter()
    {
        Blackboard.animator.CrossFade("Idles", 0.1f);
        Blackboard.animator.SetFloat("Speed", 0.0f);
    }

    public void UpdateState(float deltaTime)
    {
        if (Blackboard.jumpInput.triggered && Blackboard.GetComponent<Rigidbody>().velocity.y == 0.0f)
        {
            Fsm.ChangeState<JumpState>();
            return;
        }
        
        var value = Blackboard.moveInput.ReadValue<Vector2>();
        if (value.sqrMagnitude > 0)
        {
            Fsm.ChangeState<WalkState>();
        }
    }

    public void Exit()
    {
    }
}