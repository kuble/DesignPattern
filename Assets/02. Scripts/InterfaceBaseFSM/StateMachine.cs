using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private IState defaultState;
    
    private IState currentState;
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    public void Run()
    {
        IState[] states = GetComponents<IState>();
        foreach (var state in states)
        {
            AddState(state);
        }
        
        ChangeState(defaultState.GetType());
    }
    
    public void AddState(IState state)
    {
        state.Fsm = this;
        states.Add(typeof(IState), state);
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