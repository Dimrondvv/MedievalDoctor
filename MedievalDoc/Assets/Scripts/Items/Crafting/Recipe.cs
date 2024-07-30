using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 2)]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public string recipeDescription;
    public GameObject result;
    public List<RequiredItem> requiredItems;
    public float timeRequiredToCraft;
    public bool isOrderSensitive;
    public Sprite icon;

    [System.Serializable]
    public class RequiredItem
    {
        public GameObject item;
        public bool isConsumed = true;
    }

    public bool CheckReq(List<GameObject> itemList)
    {
        if (itemList.Count == 0 || itemList.Count != requiredItems.Count)
            return false;


        bool doesContain = false;
        for(int i = 0; i < requiredItems.Count; i++)
        {
            string reqName = requiredItems[i].item.GetComponent<Item>() != null ? requiredItems[i].item.GetComponent<Item>().ItemName : requiredItems[i].item.GetComponent<InteractionTool>().ToolName;
            if (isOrderSensitive)
            {
                string itemName = itemList[i].GetComponent<Item>() != null ? itemList[i].GetComponent<Item>().ItemName : itemList[i].GetComponent<InteractionTool>().ToolName;

                if (reqName != itemName)
                    return false;
            }
            else
            {
                foreach(var item in itemList)
                {
                    string itemName = item.GetComponent<Item>() != null ? item.GetComponent<Item>().ItemName : item.GetComponent<InteractionTool>().ToolName;
                    if (itemName == reqName)
                        doesContain = true;
                }
                if (doesContain == false)
                    return false;
                doesContain = false;
            }
        }
        return true;
    }
}
