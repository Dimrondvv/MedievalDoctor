using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private List<GameObject> insertedItems;

    private void Start()
    {
        PickupController.OnInteract.AddListener(CraftItem);
        PickupController.OnPickup.AddListener(RemoveItem);
        PickupController.OnPutdown.AddListener(InsertItem);
    }

    public void InsertItem(PickupController player, Transform objectPoint)
    {
        if (objectPoint.gameObject != gameObject)
            return;

        insertedItems.Add(player.PickedItem);
        Destroy(player.PickedItem);
        
    }
    public void RemoveItem(GameObject item, Transform objectPoint)
    {
        if (objectPoint.gameObject != gameObject || insertedItems.Count == 0)
            return;

        GameObject removedItem = Instantiate(insertedItems[insertedItems.Count - 1]);
        PlayerManager.Instance.PickupController.SetPickedItem(removedItem);
        insertedItems.RemoveAt(insertedItems.Count - 1);
    }
    public void CraftItem(GameObject station, PickupController player)
    {
        if (station != gameObject)
            return;

        Debug.Log("Craft interacted;");

        Recipe validRecipe = null;
        foreach(var item in recipes)
        {
            if (item.CheckReq(insertedItems))
                validRecipe = item;
        }

        if (validRecipe != null)
        {
            Debug.Log("Crafted");
            GameObject result = Instantiate(validRecipe.result);
            
            player.SetPickedItem(result);
        }
    }

    
}
