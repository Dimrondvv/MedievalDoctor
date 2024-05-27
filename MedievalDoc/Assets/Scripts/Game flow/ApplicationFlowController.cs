using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFlowController : MonoBehaviour
{
    private AppStateMachine stateMachine = new AppStateMachine();
    public IntroState intro = new IntroState((int)EAppState.Intro);
    public MainHubState mainHub = new MainHubState((int)EAppState.MainHub);
    public LoadingState load = new LoadingState((int)EAppState.Loading);
    public AppGameState game = new AppGameState((int)EAppState.Game);
    public SummaryState summary = new SummaryState((int)EAppState.Summary);

    private void Awake()
    {
        App.CreateInstance();
        App.Instance.Initalize();
    }

    private void OnDestroy()
    {
        App.Instance.Uninitialize();
    }

    private void Start()
    {
        stateMachine.AddState(intro);
        stateMachine.AddState(mainHub);
        stateMachine.AddState(load);
        stateMachine.AddState(game);
        stateMachine.AddState(summary);

        stateMachine.Initialize();
    }
}
