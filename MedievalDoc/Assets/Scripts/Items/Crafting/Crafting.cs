using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private List<GameObject> insertedItems;

    public void InsertItem(GameObject item)
    {
        insertedItems.Add(item);
        //TODO - Remove item from player hand
    }
    public void RemoveItem()
    {
        insertedItems.RemoveAt(insertedItems.Count - 1);
        //TODO - Give item to player hand
    }



}
