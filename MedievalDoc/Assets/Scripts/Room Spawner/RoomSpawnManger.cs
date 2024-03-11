using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnManger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> pokoje = new List<GameObject>();

    [SerializeField]
    private GameObject baseRoom;

    [SerializeField]
    private DayAndNightController dayCount;

    [SerializeField]
    private int wichDaySpawn = 3;

    private GameObject prevRoom;
    private int prevDir = 69;
    private bool callOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn base room
        prevRoom = Instantiate(baseRoom, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        bool condition = (dayCount.DayCounter + 1) % wichDaySpawn == 0;
        
        if (condition && !callOnce){
            spawnRoom();
        }

        callOnce = condition;
        
    }

    

    private void spawnRoom()
    {
        var randomRoom = Random.Range(0, pokoje.Count);
        var direction = Random.Range(0, 3);
        
        Vector3 position = prevRoom.transform.position;
        Debug.Log("Dir: " + direction + " Prev: " + prevDir);
        if (direction == prevDir)
        {
            direction = Random.Range(0, 3);
        }
        else
        {
            
            switch (direction)
            {
                case 0:
                    prevDir = 2;
                    position.x -= prevRoom.GetComponent<Collider>().bounds.size.x;
                    break;

                case 1:
                    prevDir = 3;
                    position.z += prevRoom.GetComponent<Collider>().bounds.size.z;
                    break;

                case 2:
                    prevDir = 0;
                    position.x += prevRoom.GetComponent<Collider>().bounds.size.x;
                    break;

                case 3:
                    prevDir = 1;
                    position.z -= prevRoom.GetComponent<Collider>().bounds.size.z;
                    break;
            }
            //Debug.Log(position);
            prevRoom = Instantiate(pokoje[randomRoom], new Vector3(position.x, 0f, position.z), Quaternion.identity);
        } 
    }
}
