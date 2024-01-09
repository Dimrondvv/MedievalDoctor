using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintTrigger : MonoBehaviour
{
    public Material blueprintRed;
    public Material blueprintBlue;
    public bool isPlacable;
    private void OnTriggerEnter(Collider other)
    {
        isPlacable = false;
        GetComponent<MeshRenderer>().material = blueprintRed;
    }
    private void OnTriggerExit(Collider other)
    {
        isPlacable = true;
        GetComponent<MeshRenderer>().material = blueprintBlue;
    }
}
