using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;

    public enum Sickness
    {
        Healthy,
        Cold,
        Gangrene
    }

    public void Interact()
    {
        GameObject playerItem = player.GetComponent<PickUpItem>().pickedItem;
        if(playerItem == null)
        {
            Debug.Log("Player holds no item");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
