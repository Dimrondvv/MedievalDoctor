using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;

    [SerializeField] public SicknessScriptableObject sickness;
    DeathTimer DeathTimer;

    List<GameObject> usedItems;

    public int spawnerID;
    [SerializeField] SpawnPatientTimer SpawnPatientSpawner;




    public void Death()
    {
        DeathTimer.elapsedTime = 0; // change it to 0 to give him more time to live
        SpawnPatientSpawner.SpawnPoints[spawnerID].GetComponent<Chair>().isOccupied = false;
        GameManager.Instance.deathCounter+=1;
        Debug.Log(GameManager.Instance.deathCounter);
        Destroy(this.gameObject); // if dead = destroy object
    }




    public void Interact()
    {
        GameObject playerItem = player.GetComponent<PickUpItem>().pickedItem;
        Debug.Log(playerItem);
        if(playerItem == null)
        {
            Debug.Log("Player holds no item");
        }
        else //Add the item to usedItems list and then compare it with tools required to cure the patient
        {
            usedItems.Add(playerItem);
            if(CompareItems() == 2)
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
    private void Awake()
    {
        usedItems = new List<GameObject>();
        DeathTimer = GetComponent<DeathTimer>();
    }
    private int CompareItems() //Compares items used on a patient to the items needed to cure, returns 0 if wrong item is used, 1 if
    {                          //the items so far are correct, and 3 if all the items are correct and requirements for curement are met
        for (int i = 0; i < usedItems.Count; i++)
        {
            if (usedItems[i].name != sickness.toolsRequired[i].name)
                return 0;
            else if(i == sickness.toolsRequired.Count - 1 && usedItems[i].name == sickness.toolsRequired[i].name)
            {
                return 2;
            }
        }
        return 1;
    }
}
