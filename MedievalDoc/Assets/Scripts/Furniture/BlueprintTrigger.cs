using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintTrigger : MonoBehaviour
{
    public Material blueprintRed;
    public Material blueprintBlue;
    public bool isPlacable;

    HashSet<Collider> currentlyTouching = new HashSet<Collider>();

    private void Start() {
        if (currentlyTouching.Count > 0 ) {
            isPlacable = false;
            ChangeBlueprintToRed(this.gameObject);
        } else {
            isPlacable = true;
            ChangeBlueprintToBlue(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlacable = false;
        ChangeBlueprintToRed(this.gameObject);
        currentlyTouching.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        isPlacable = true;
        ChangeBlueprintToBlue(this.gameObject);
        currentlyTouching.Remove(other);
    }

    public void ChangeBlueprintToBlue(GameObject obj) {
        if (obj.transform.childCount > 0) {// Check if object contains chilndren
            Transform firstChild = obj.transform.GetChild(0);
  
            if (firstChild.childCount > 0) // Check if children has any childrens
            {
                for (int i = 0; i < firstChild.childCount; i++) {
                    Transform secondLayerChild = firstChild.transform.GetChild(i);

                    var mats = secondLayerChild.gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                    for (int j = 0; j < mats.Length; j++) {
                        mats[j] = blueprintBlue; // Change all material to chosen
                    }

                    firstChild.transform.GetChild(i).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to child gameObject
                }
                
            }else
            {
                var mats = firstChild.gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = blueprintBlue; // Change all material to chosen
                }

                firstChild.gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to gameObject
            } 
        } else {
            GetComponent<Renderer>().material = blueprintBlue;
        }
    }

    private void ChangeBlueprintToRed(GameObject obj) {
        if (obj.transform.childCount > 0) {// Check if object contains chilndren
            Transform firstChild = obj.transform.GetChild(0);

            if (firstChild.childCount > 0) // Check if children has any childrens
            {
                for (int i = 0; i < firstChild.childCount; i++) {
                    Transform secondLayerChild = firstChild.transform.GetChild(i);

                    var mats = secondLayerChild.gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                    for (int j = 0; j < mats.Length; j++) {
                        mats[j] = blueprintRed; // Change all material to chosen
                    }

                    firstChild.transform.GetChild(i).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to child gameObject
                }

            } else {
                var mats = firstChild.gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                for (int i = 0; i < mats.Length; i++) {
                    mats[i] = blueprintRed; // Change all material to chosen
                }

                firstChild.gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to gameObject
            }
        } else {
            GetComponent<Renderer>().material = blueprintRed;
        }
    }
}
