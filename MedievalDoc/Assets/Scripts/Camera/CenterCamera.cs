using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCamera : MonoBehaviour
{
    [SerializeField] GameObject room;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = room.transform.position;
    }

    
}
