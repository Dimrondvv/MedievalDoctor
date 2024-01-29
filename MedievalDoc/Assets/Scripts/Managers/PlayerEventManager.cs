using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    private static PlayerEventManager instance;
    public static PlayerEventManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    private void Awake()
    {
        instance = this;
    }


}
