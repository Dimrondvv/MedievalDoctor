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
        ChangeBlueprintToBlue(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        isPlacable = true;
        ChangeBlueprintToRed(this.gameObject);
    }

    public void ChangeBlueprintToBlue(GameObject obj) {
        if (obj.transform.childCount > 0) {// Check if object contains chilndren
            var mats = obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

            for (int i = 0; i < mats.Length; i++) {
                mats[i] = blueprintRed; // Change all material to chosen
            }

            obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to gameObject
        } else {
            GetComponent<Renderer>().material = blueprintRed;
        }
    }

    private void ChangeBlueprintToRed(GameObject obj) {
        if (obj.transform.childCount > 0) {// Check if object contains chilndren
            var mats = obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

            for (int i = 0; i < mats.Length; i++) {
                mats[i] = blueprintBlue; // Change all material to chosen
            }

            obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to gameObject
        } else {
            GetComponent<Renderer>().material = blueprintBlue;
        }
    }
}
