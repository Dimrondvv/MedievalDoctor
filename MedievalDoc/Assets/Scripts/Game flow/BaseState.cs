using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseState : IBaseState
{
    private AppStateMachine parent = null;
    private int id = -1;
    public AppStateMachine Parent { get => parent; }
    int IBaseState.Id { get => id; }

    public BaseState(int appState)
    {
        id = (int)appState;
    }

    public virtual void Initialize()
    {

    }

    public virtual void OnEnter(int prev)
    {
        Initialize();
        Debug.LogFormat("Entering State {0}", id);
    }

    public virtual void UnInitialize()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnExit(int next)
    {

    }

    public void SetParent(AppStateMachine parent)
    {
        this.parent = parent;
    }
}

