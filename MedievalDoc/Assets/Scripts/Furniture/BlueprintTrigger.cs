using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintTrigger : MonoBehaviour
{
    public Material blueprintRed;
    public Material blueprintBlue;
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<MeshRenderer>().material = blueprintRed;
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponent<MeshRenderer>().material = blueprintBlue;
    }
}
