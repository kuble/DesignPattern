using UnityEngine;

public class JumpState : MonoBehaviour, IState
{
    [SerializeField]private float JumpForce = 3f;
    public StateMachine Fsm { get; set; }
    private Animator _animator;
    private Rigidbody _rigidbody;
    public void InitState()
    {
        _animator = Fsm.GetComponent<Animator>();
        _rigidbody = Fsm.GetComponent<Rigidbody>();
    }

    public void Enter()
    {
        _animator.CrossFade("Jump", 0.1f);
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, JumpForce, _rigidbody.velocity.z);
    }

    public void UpdateState(float deltaTime)
    {
        if (_rigidbody.velocity.y == 0.0f)
        {
            Fsm.ChangeState<IdleState>();
        }
    }

    public void Exit()
    {
    }
}