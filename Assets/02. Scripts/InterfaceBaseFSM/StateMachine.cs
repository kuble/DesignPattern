using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private string defaultState;
    
    private IState currentState;
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    public void Run()
    {
        IState[] stateList = GetComponents<IState>();
        foreach (var state in stateList)
        {
            AddState(state);
        }
        
        ChangeState(Type.GetType(defaultState));
    }

    public void AddState(IState state)
    {
    
        state.Fsm = this;
        state.InitState();
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