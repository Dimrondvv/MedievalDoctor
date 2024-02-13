using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : BaseState
{
    public IntroState(int appState) : base(appState)
    {

    }

    public override void Initialize()
    {
        base.Initialize();
        Parent.MakeTransition((int)EAppState.MainHub);
    }
}
