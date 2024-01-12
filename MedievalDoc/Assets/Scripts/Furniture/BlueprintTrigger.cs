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
        ChangeBlueprintToRed(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        isPlacable = true;
        ChangeBlueprintToBlue(this.gameObject);
    }

    public void ChangeBlueprintToBlue(GameObject obj) {
        if (obj.transform.childCount > 0) {// Check if object contains chilndren
            Transform firstChild = obj.transform.GetChild(0);
  
            if (firstChild.childCount > 0) // Check if children has any childrens
            {
                Transform firstChildOfaChild = firstChild.transform.GetChild(0);

                var mats = firstChildOfaChild.gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = blueprintBlue; // Change all material to chosen
                }

                obj.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to child gameObject
            }else
            {
                var mats = obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = blueprintBlue; // Change all material to chosen
                }

                obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to gameObject
            } 
        } else {
            GetComponent<Renderer>().material = blueprintBlue;
        }
    }

    private void ChangeBlueprintToRed(GameObject obj) {
        //if (obj.transform.childCount > 0) {// Check if object contains chilndren
        //    var mats = obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

        //    for (int i = 0; i < mats.Length; i++) {
        //        mats[i] = blueprintBlue; // Change all material to chosen
        //    }

        //    obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to gameObject
        //} else {
        //    GetComponent<Renderer>().material = blueprintBlue;
        //}
        if (obj.transform.childCount > 0)
        {// Check if object contains chilndren
            Transform firstChild = obj.transform.GetChild(0);

            if (firstChild.childCount > 0) // Check if children has any childrens
            {
                Transform firstChildOfaChild = firstChild.transform.GetChild(0);

                var mats = firstChildOfaChild.gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = blueprintRed; // Change all material to chosen
                }

                obj.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to child gameObject
            }
            else
            {
                var mats = obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials; // Create list of materials to change

                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = blueprintRed; // Change all material to chosen
                }

                obj.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials = mats; // Assign materials to gameObject
            }
        }
        else
        {
            GetComponent<Renderer>().material = blueprintRed;
        }
    }
}
