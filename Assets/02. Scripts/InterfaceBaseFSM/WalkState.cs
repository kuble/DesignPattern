using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : MonoBehaviour, IState
{
    [SerializeField] private float moveSpeed = 3.0f;
    public StateMachine Fsm { get; set; }
    private Animator _animator;
    private Rigidbody _rigidbody;
    private InputAction _moveInput;
    private InputAction _jumpInput;
    public void InitState()
    {
        _animator = Fsm.GetComponent<Animator>();
        _rigidbody = Fsm.GetComponent<Rigidbody>();
        _moveInput = Fsm.GetComponent<PlayerInput>().actions["Move"];
        _jumpInput = Fsm.GetComponent<PlayerInput>().actions["Jump"];
    }

    public void Enter()
    {
        _animator.CrossFade("Idles", 0.1f);
        _animator.SetFloat("Speed", 1.0f);
    }

    public void UpdateState(float deltaTime)
    {
        if (_jumpInput.triggered && _rigidbody.velocity.y == 0.0f)
        {
            Fsm.ChangeState<JumpState>();
            return;
        }
        
        var value = _moveInput.ReadValue<Vector2>();
        if (0 >= value.sqrMagnitude)
        {
            Fsm.ChangeState<IdleState>();
            return;
        }
        
        _rigidbody.velocity = new Vector3(value.x * moveSpeed, _rigidbody.velocity.y, value.y * moveSpeed);
    }

    public void Exit()
    {
    }
}
