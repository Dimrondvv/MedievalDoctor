using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateSpawnTime : MonoBehaviour
{
    [SerializeField]
    private SpawnPatientTimer spawnPatientTimer;

    [Header("IN SECONDS")]
    [SerializeField]
    private int accelerationSpeed;

    [SerializeField]
    private float invokeTime; // How fast spawning speed should change

    private void Start()
    {
        InvokeRepeating("AccelerateSpawn", invokeTime + 0.1f, invokeTime);
    }

    private void Update()
    {
        if (spawnPatientTimer.SpawnTime < accelerationSpeed)
        {
            spawnPatientTimer.SpawnTime = accelerationSpeed;
        }
    }

    private void AccelerateSpawn()
    {
        spawnPatientTimer.SpawnTime -= accelerationSpeed;
    }
    
}
