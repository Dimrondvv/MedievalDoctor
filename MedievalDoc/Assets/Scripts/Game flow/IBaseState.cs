using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseState
{
    public AppStateMachine Parent { get; }
    int Id { get; }
    void Initialize();
    void Update();
    void UnInitialize();
    void OnEnter(int prev);
    void OnExit(int next);
    void SetParent(AppStateMachine parent);
}

public enum EAppState
{
    Invalid = -1,
    Intro = 0,
    MainHub = 1,
    Loading = 2,
    Game = 3,
    Summary = 4
}

