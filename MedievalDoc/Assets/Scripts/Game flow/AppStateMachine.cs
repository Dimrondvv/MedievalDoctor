using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppStateMachine
{
    public UnityEvent<int> OnStateChanged = new UnityEvent<int>();

    private Dictionary<int, IBaseState> states = new Dictionary<int, IBaseState>();
    private IBaseState currentState = null;

    public void Initialize()
    {
        MakeTransition(0);
    }

    public void AddState(IBaseState state)
    {
        states.Add(state.Id, state);
        state.SetParent(this);
    }

    public void MakeTransition(int state)
    {
        int prevState = -1;

        if (currentState != null)
        {
            prevState = currentState.Id;
            currentState.OnExit(state);
        }
        currentState = states[state];
        currentState.OnEnter(prevState);
        OnStateChanged.Invoke(state);
    }
}
