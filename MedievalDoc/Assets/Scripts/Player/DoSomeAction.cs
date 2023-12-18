using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomeAction : MonoBehaviour
{
    int layerMask = 1 << 6;
    public int picUpRange = 1;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, picUpRange, layerMask))
            {
                hit.transform.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
    
}
