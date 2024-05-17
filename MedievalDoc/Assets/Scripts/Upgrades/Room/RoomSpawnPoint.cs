using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnPoint : MonoBehaviour 
{
    [SerializeField] List<RoomSpawnPoint> neighbors;
    [SerializeField] private bool isTaken;


    public bool IsTaken { get { return isTaken; } }

    public bool CheckSpawnConditions()
    {
        foreach(RoomSpawnPoint p in neighbors)
        {
            if (p.IsTaken)
            {
                isTaken = true;
                return true;
            }
        }
        return false;
    }
}
