using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class App
{

    public GameplayCore GameplayCore { get; private set; } = new GameplayCore();

    public void Initalize()
    {
        GameplayCore.Initialize();
    }
    public void Uninitialize()
    {
        GameplayCore.UnInitialize();
    }


    #region Singleton
    private static App instance;
    public static App Instance
    {
        get
        {
            if (instance == null)
            {
                CreateInstance();
            }
            return instance;
        }
    }

    public static void CreateInstance()
    {
        if (instance == null)
        {
            instance = new App();
        }
    }
    #endregion
}
