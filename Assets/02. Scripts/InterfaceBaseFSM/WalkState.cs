using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : MonoBehaviour, IState
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
        Blackboard.animator.SetFloat("Speed", 1.0f);
    }

    public void UpdateState(float deltaTime)
    {
        if (Blackboard.jumpInput.triggered && Blackboard.GetComponent<Rigidbody>().velocity.y == 0.0f)
        {
            Fsm.ChangeState<JumpState>();
            return;
        }
        
        var value = Blackboard.moveInput.ReadValue<Vector2>();
        if (0 >= value.sqrMagnitude)
        {
            Fsm.ChangeState<IdleState>();
            return;
        }
        
        Blackboard.GetComponent<Rigidbody>().velocity = new Vector3(value.x * Blackboard.moveSpeed, Blackboard.GetComponent<Rigidbody>().velocity.y, value.y * Blackboard.moveSpeed);
    }

    public void Exit()
    {
    }
}
