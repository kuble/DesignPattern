using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    private Animator _animator;
    private Rigidbody _rigidbody;
    private InputAction _moveInput;
    private InputAction _jumpInput;
    public void InitState()
    {
        _animator = Fsm.GetComponent<Animator>();
        _moveInput = Fsm.GetComponent<PlayerInput>().actions["Move"];
        _jumpInput = Fsm.GetComponent<PlayerInput>().actions["Jump"];
        _rigidbody = Fsm.GetComponent<Rigidbody>();
    }

    public void Enter()
    {
        _animator.CrossFade("Idles", 0.1f);
        _animator.SetFloat("Speed", 0.0f);
    }

    public void UpdateState(float deltaTime)
    {
        if (_jumpInput.triggered && GetComponent<Rigidbody>().velocity.y == 0.0f)
        {
            Fsm.ChangeState<JumpState>();
            return;
        }
        
        var value = _moveInput.ReadValue<Vector2>();
        if (value.sqrMagnitude > 0)
        {
            Fsm.ChangeState<WalkState>();
        }
    }

    public void Exit()
    {
    }
}