using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int picUpRange = 1;
    public GameObject pickedItem;
    // Bit shift the index of the layer (6) to get a bit mask
    int layerMask = 1 << 6;
    bool picked = false;
    

    Transform test;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (!picked)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, picUpRange, layerMask))
                {
                    test = hit.transform;
                    picked = true;
                    hit.transform.SetParent(transform);
                }

            } else {
                test.transform.SetParent(null);
                picked = false;
            }
        }
    }
}
