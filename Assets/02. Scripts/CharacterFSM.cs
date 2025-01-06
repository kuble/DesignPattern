using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//캐릭터의 유한상태기계의 상태 구분 열거형 //25-01-06
public enum CharacterFSMState
{
    Idle,
    Walk,
    Jump
}

//캐릭터의 유한상태기계 클래스
public class CharacterFSM : MonoBehaviour
{
    //애니메이터의 파라미터를 해시값으로 비교한다.
    //해시값으로 비교하는 이유는 문자열값보다 숫자값의 비교가 더 빠르기 때문
    private static readonly int speed_Hash = Animator.StringToHash("Speed");
    //유한상태기계의 상태를 Idle로 초기화 해 둔다.
    private CharacterFSMState currState = CharacterFSMState.Idle;
    //유한상태기계의 직전 상태를 Idle로 초기화 해 둔다.
    private CharacterFSMState prevState = CharacterFSMState.Idle;
    
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float jumpForce = 10.0f;

    private bool isGrounded;
    private Vector2 moveInput;
    
    //레퍼런스
    private InputAction _moveAction;
    private InputAction _jumpAction;
    
    private Animator _animator;
    
    private Rigidbody _rigidbody;
    
    //레퍼런스 캐싱
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        currState = CharacterFSMState.Idle;

        _moveAction = GetComponent<PlayerInput>().actions["Move"];
        _jumpAction = GetComponent<PlayerInput>().actions["Jump"];
    }

    // Update is called once per frame
    void Update()
    {
        // 움직임 값 확인
        moveInput =  _moveAction.ReadValue<Vector2>();
        bool bPressedJump = _jumpAction.triggered;

        GroundCheck();
        StateChange(bPressedJump);
        
        EnterState();
        UpdateState();
        ExitState();
    }

    private void GroundCheck()
    {
        //점프 상태 확인(질문요소 : 왜 velocity.y가 0.0보다 작으면 is grounded인가?)
        isGrounded = _rigidbody.velocity.y < 0.0f;
    }

    private void StateChange(bool bPressedJump)
    {
        //상태를 변경
        prevState = currState;
        switch (currState)
        {
            case CharacterFSMState.Idle:
            {
                if (moveInput.sqrMagnitude > 0.0f)
                {
                    currState = CharacterFSMState.Walk;
                }

                if (bPressedJump && isGrounded)
                {
                    currState = CharacterFSMState.Jump;
                }

                break;   
            }
            case CharacterFSMState.Walk:
            {
                if (moveInput.sqrMagnitude <= 0.0f)
                {
                    currState = CharacterFSMState.Idle;
                }
                
                if (bPressedJump && isGrounded)
                {
                    currState = CharacterFSMState.Jump;
                }
                
                break;    
            }
            case CharacterFSMState.Jump:
            {
                if (isGrounded)
                {
                    currState = CharacterFSMState.Idle;
                }
                break;
            }
        }
    }

    //이전 상태와 다른 상태가 되었다면 에니메이터의 speed_Hash값의 변화를 주거나
    //Jump애니메이션을 호출하여 애니메이션을 지정해줌
    private void EnterState()
    {
        if (prevState != currState)
        {
            switch (currState)
            {
                case CharacterFSMState.Idle:
                {
                    _animator.SetFloat(speed_Hash, 0.0f);
                    break;
                }
                case CharacterFSMState.Walk:
                {
                    _animator.SetFloat(speed_Hash, 1.0f);
                    break;
                }
                case CharacterFSMState.Jump:
                {
                    _animator.CrossFade("Jump", 0.1f);
                    _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpForce, _rigidbody.velocity.z);
                    break;
                }
            }
        }
    }

    //점프가 끝난 후 State를 Idle로 변경하기위해 애니메이터를 재설정
    private void ExitState()
    {
        if (prevState != currState)
        {
            switch (prevState)
            {
                case CharacterFSMState.Jump:
                {
                    //애니메이션이 변경될 때 부드러운 효과를 주어 변경하도록함 두번째 파라미터는 다른 애니메이션 클립으로 FadeOut되는 시간) 
                    _animator.CrossFade("Idles", 0.1f);
                    break;
                }
            }
        }
    }

    //현재 상태가 Walk라면 저장한 방향으로 이동
    private void UpdateState()
    {
        switch (currState)
        {
            case CharacterFSMState.Walk:
            {
                //Update()에서 저장한 moveInput값을 이용하여 바로 캐릭터 이동
                _rigidbody.velocity = new Vector3(moveInput.x * moveSpeed, _rigidbody.velocity.y, moveInput.y * moveSpeed);
                break;
            }
            case CharacterFSMState.Jump:
            {
                break;
            }
        }
    }
}
