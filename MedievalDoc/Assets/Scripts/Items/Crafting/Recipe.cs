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
        return itemList == requiredItems;
    }
}
