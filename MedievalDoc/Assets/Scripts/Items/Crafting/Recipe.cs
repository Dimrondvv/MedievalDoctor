using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 2)]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public GameObject result;
    public List<GameObject> requiredItems;
    public float timeRequiredToCraft;

    public bool CheckReq(List<GameObject> itemList)
    {
        if (itemList.Count == 0 || itemList.Count != requiredItems.Count)
            return false;

        for(int i = 0; i < requiredItems.Count; i++)
        {
            if (requiredItems[i].name != itemList[i].name)
                return false;
        }
        return true;
    }
}
