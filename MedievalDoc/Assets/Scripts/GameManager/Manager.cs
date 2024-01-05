using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] int maxDeaths;
    public int deathCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(deathCounter >= maxDeaths){
	    Debug.Log("MAX DEATHS");
	}
    }
}
