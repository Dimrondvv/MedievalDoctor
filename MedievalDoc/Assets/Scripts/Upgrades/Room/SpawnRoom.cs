using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{

    private List<RoomSpawnPoint> roomSpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        roomSpawnPoints = App.Instance.GameplayCore.UpgradeManager.roomSpawnPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
