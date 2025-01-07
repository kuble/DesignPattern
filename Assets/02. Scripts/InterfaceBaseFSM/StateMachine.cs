using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StaterType
{
    None,
    Character,
    Max
}

// 확장 메서드 + 팩토리 메서드 패턴 + 컬렉션 입니다.
public static class StateFactory
{
    public static List<IState> CreateStates(this StateMachine stateMachine, StaterType staterType)
    {
        List<IState> states = new List<IState>();
        
        switch (staterType)
        {
            case StaterType.Character:
            {
                states.Add(stateMachine.AddComponent<IdleState>());
                states.Add(stateMachine.AddComponent<WalkState>());
                states.Add(stateMachine.AddComponent<JumpState>());
            }
                break;
        }

        return states;
    }
}
public class StateMachine : MonoBehaviour
{
    [SerializeField] private string defaultState;
    
    private IState currentState;
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    public void Run()
    {
        IBlackboardBase blackboardDefault = GetComponent<IBlackboardBase>();
        blackboardDefault.InitBlackboard();
        List<IState> stateList = this.CreateStates(StaterType.Character);
        foreach (var state in stateList)
        {
            AddState(state, blackboardDefault);
        }
        
        ChangeState(Type.GetType(defaultState));
    }

    public void AddState(IState state, IBlackboardBase blackboard)
    {
    
        state.Fsm = this;
        state.InitState(blackboard);
        states.Add(state.GetType(), state);
    }
    
    public void ChangeState<T>() where T : IState
    {
        ChangeState(typeof(T));
    }
    
    private void ChangeState(Type stateType)
    {
        currentState?.Exit();

        if (!states.TryGetValue(stateType, out currentState)) return;
        currentState?.Enter();
    }

    public void UpdateState()
    {
        if (currentState != null)
            currentState.UpdateState(Time.deltaTime);
    }
}