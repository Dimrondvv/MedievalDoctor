using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;

    [SerializeField] SicknessScriptableObject sickness;

    List<GameObject> usedItems;

    public void Interact()
    {
        GameObject playerItem = player.GetComponent<PickUpItem>().pickedItem;
        if(playerItem == null)
        {
            Debug.Log("Player holds no item");
        }
        else //Add the item to usedItems list and then compare it with tools required to cure the patient
        {
            usedItems.Add(playerItem);
            if(CompareItems() == 3)
            {
                Debug.Log("Patient cured");
            }
            else if(CompareItems() == 1)
            {
                Debug.Log("Correct item");
            }
            else if(CompareItems() == 0)
            {
                Debug.Log("Wrong item");
            }
        }
    }

    private int CompareItems() //Compares items used on a patient to the items needed to cure, returns 0 if wrong item is used, 1 if
    {                          //the items so far are correct, and 3 if all the items are correct and requirements for curement are met
        for (int i = 0; i < usedItems.Count; i++)
        {
            if (usedItems[i] != sickness.toolsRequired[i])
                return 0;
            else if(i == sickness.toolsRequired.Count - 1 && usedItems[i] == sickness.toolsRequired[i])
            {
                return 2;
            }
        }
        return 1;
    }
}
