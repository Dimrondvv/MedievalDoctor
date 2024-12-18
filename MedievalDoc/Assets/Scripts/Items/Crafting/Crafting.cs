using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Data;
public class Crafting : MonoBehaviour
{
    [SerializeField] private List<Recipes> recipes;
    [SerializeField] private List<GameObject> insertedItems;
    [SerializeField] private Transform resultLayDownPoint;
    [SerializeField] private float interactionTime;
    [SerializeField] private List<Transform> itemSlots;

    private string craftingName;

    public static UnityEvent<Recipes> OnCraftingCompleted = new UnityEvent<Recipes>();
    public float InteractionTime { get { return interactionTime; } }

    private void Start()
    {
        PickupController.OnInteract.AddListener(CraftItem);
        PickupController.OnPickup.AddListener(RemoveItem);
        PickupController.OnPutdown.AddListener(InsertItem);
        InitializeCrafting();
    }

    private void InitializeCrafting()
    {
        craftingName = gameObject.name.Replace("(clone)", "").Trim();
        recipes = new List<Recipes>();
        foreach (string recipeId in HelperFunctions.CraftingLookup(craftingName).recipes)
        {
            try
            {
                recipes.Add(HelperFunctions.RecipeLookup(recipeId));
            }
            catch
            {
                Debug.LogError($"Error on id: {recipeId} and {HelperFunctions.RecipeLookup(recipeId).recipeID}");
            }
        }
    }

    public void InsertItem(PickupController player, Transform objectPoint)
    {
        if (objectPoint == null)
            return;
        if (objectPoint.gameObject != gameObject || player.PickedItem.GetComponent<Furniture>() || player.PickedItem.GetComponent <Patient>())
            return;
        if (itemSlots.Count == insertedItems.Count)
            return;

        insertedItems.Add(player.PickedItem);
        insertedItems[insertedItems.Count - 1].transform.parent = itemSlots[insertedItems.Count - 1];
        insertedItems[insertedItems.Count - 1].transform.position = itemSlots[insertedItems.Count - 1].position;
        insertedItems[insertedItems.Count - 1].transform.eulerAngles = Vector3.zero;
        player.PickedItem = null;


    }
    public void RemoveItem(GameObject item, Transform objectPoint)
    {
        if (objectPoint == null)
            return;
        if (objectPoint.gameObject != gameObject || insertedItems.Count == 0)
            return;

        insertedItems[insertedItems.Count - 1].SetActive(true);
        PlayerManager playerManager = App.Instance.GameplayCore.PlayerManager;
        playerManager.PickupController.SetPickedItem(insertedItems[insertedItems.Count - 1]);
        insertedItems.RemoveAt(insertedItems.Count - 1);
    }

    private bool CheckRecipe(Recipes recipeChecked) //Check if the correct items are input into the crafting
    {
        List<string> itemNames = new List<string>();
        List<string> requiredItemNames = new List<string>(recipeChecked.itemsRequired);

        foreach(GameObject item in insertedItems)
        {
            Tool tool = item.GetComponent<Tool>();
            if(tool != null)
                itemNames.Add(tool.toolData.toolID);
            else
                itemNames.Add(item.name);
        }

        foreach(string item in recipeChecked.itemsRequired) //Check if items required are present
        {
            if (!itemNames.Contains(item))
                return false;
        }
        foreach(string item in itemNames) //Check if unnecessary items are present on crafting
        {
            if (!requiredItemNames.Contains(item))
                return false;
        }


        return true;
    }

    public void CraftItem(GameObject station, PickupController player)
    {
        if (station != gameObject)
            return;

        Recipes validRecipe = null;
        foreach(var item in recipes)
        {
            if (CheckRecipe(item))
            {
                validRecipe = item;
            }
        }

        if (validRecipe != null)
        {
            StartCoroutine(CraftingCoroutine(validRecipe));
        }
    }

    private void DestroyItems(Recipes recipe)
    {
        Debug.Log(recipe);
        foreach(string itemID in recipe.destroyedOnCraft)
        {
            foreach (var item in insertedItems)
            {
                Tool tool = item.GetComponent<Tool>();
                if(tool != null)
                {
                    if(tool.toolData.toolID == itemID)
                    {
                        Destroy(item);
                        insertedItems.Remove(item);
                        break;
                    }
                }
                else if (item.name == itemID)
                {
                    Destroy(item);
                    insertedItems.Remove(item);
                    break;
                }
            }
        }
    }

    IEnumerator CraftingCoroutine(Data.Recipes recipe)
    {
        float time;
        try
        {
            time = System.Int32.Parse(recipe.recipeTime);
        }
        catch
        {
            Debug.LogWarning($"No crafting time specified for recipe {recipe.recipeName}");
            time = 0.1f;    
        }

        GetComponent<ProgressBar>().StartProgressBar(time);
        yield return new WaitForSeconds(time);
        GetComponent<ProgressBar>().StopProgressBar();

        DestroyItems(recipe);
        GameObject result = Instantiate(Resources.Load<GameObject>("RecipeResults/" + recipe.recipeResult));
        result.transform.position = resultLayDownPoint.position;
        result.transform.parent = resultLayDownPoint;
        OnCraftingCompleted.Invoke(recipe);
    }
}
