using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCentering : MonoBehaviour
{
    [SerializeField] GameObject room;
    void Start()
    {
        transform.position = room.transform.position;   
    }
}
