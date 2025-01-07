using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Statmachine를 가지고, 구현됮 않은 시그니처 함수들을 가지고 있는 인터페이스 구조
public interface IState
{
    StateMachine Fsm { get; set; }
    
    void Enter();
    void UpdateState(float deltaTime);
    void Exit();
}

//인터페이스를 상속받은 클래스는 하위 함수, 변수가 모두 정의되어 있어야 에러가 나지 않음
public class IdleState : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    public void Enter()
    {
    }

    public void UpdateState(float deltaTime)
    {
    }

    public void Exit()
    {
    }
}

public class JumpState : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    public void Enter()
    {
    }

    public void UpdateState(float deltaTime)
    {
    }

    public void Exit()
    {
    }
}

public class WalkState : MonoBehaviour, IState
{
    public StateMachine Fsm { get; set; }
    public void Enter()
    {
    }

    public void UpdateState(float deltaTime)
    {
    }

    public void Exit()
    {
    }
}

public class StateMachine : MonoBehaviour
{
    private IState currentState;
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    public void AddState(IState state)
    {
        state.Fsm = this;
        states.Add(typeof(IState), state);
    }
    
    public void ChangeState(System.Type stateType)
    {
        // ?. : 해당 State가 Null인지 확인하여 뒤의 함수를 호출할 지 결정
        currentState?.Exit();

        if (!states.TryGetValue(stateType, out currentState)) return;
        currentState?.Enter();
    }

    void UpdateState()
    {
        if (currentState != null)
            currentState.UpdateState(Time.deltaTime);
    }
}

public class MyCharacterFsm_I : MonoBehaviour
{
    private StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        IState[] states = GetComponents<IState>();
        foreach (var state in states)
        {
            stateMachine.AddState(state);
        }
    }
}
