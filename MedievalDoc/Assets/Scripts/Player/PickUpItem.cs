using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int picUpRange = 1;
    public GameObject pickedItem;
    public float p_MaxDistance = 2f;

    int layerMask = 1 << 6; // Bit shift the index of the layer (6) to get a bit mask
    bool picked = false;
    bool m_HitDetect;
    


    Transform test;
    Collider p_Collider;

    // test
    RaycastHit hit;

    private void Start()
    {
        p_Collider = GetComponent<Collider>();
    }


    // Update is called once per frame
    void Update()
    {
        m_HitDetect = Physics.BoxCast(p_Collider.bounds.center, transform.localScale * 0.5f, transform.forward, out hit, transform.rotation, p_MaxDistance);
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (!picked)
            {
                RaycastHit hit;
                
                if (Physics.BoxCast(p_Collider.bounds.center, transform.localScale * 0.5f, transform.forward, out hit, transform.rotation, p_MaxDistance))
                {
                    test = hit.transform;
                    pickedItem = hit.transform.gameObject;
                    pickedItem.GetComponent<Collider>().enabled = false;
                    picked = true;
                    hit.transform.SetParent(transform);
                }

            } else {
                test.transform.SetParent(null);
                picked = false;
                pickedItem.GetComponent<Collider>().enabled = true;
                pickedItem = null;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, picUpRange, layerMask))
            {
               
            }
        }
    }

    // Draw Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * p_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * p_MaxDistance, transform.localScale);
        }
    }
}
