using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private List<GameObject> insertedItems;
    [SerializeField] private Transform resultLayDownPoint;
    [SerializeField] private float interactionTime;
    [SerializeField] private List<Transform> itemSlots;
    public float InteractionTime { get { return interactionTime; } }
    private bool isCrafting;

    private void Start()
    {
        PickupController.OnInteract.AddListener(CraftItem);
        PickupController.OnPickup.AddListener(RemoveItem);
        PickupController.OnPutdown.AddListener(InsertItem);
    }

    public void InsertItem(PickupController player, Transform objectPoint)
    {
        if (objectPoint == null || isCrafting)
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
        PlayerManager.Instance.PickupController.SetPickedItem(insertedItems[insertedItems.Count - 1]);
        insertedItems.RemoveAt(insertedItems.Count - 1);
    }
    public void CraftItem(GameObject station, PickupController player)
    {
        if (station != gameObject || isCrafting)
            return;

        Recipe validRecipe = null;
        foreach(var item in recipes)
        {
            if (item.CheckReq(insertedItems))
            {
                validRecipe = item;
                foreach(var i in insertedItems)
                {
                    Destroy(i);
                }
                insertedItems.Clear();
            }
        }

        if (validRecipe != null )
        {
            StartCoroutine(CraftingCoroutine(validRecipe));
        }
    }

    IEnumerator CraftingCoroutine(Recipe recipe)
    {
        isCrafting = true;
        GetComponent<ProgressBar>().StartProgressBar(recipe.timeRequiredToCraft);
        yield return new WaitForSeconds(recipe.timeRequiredToCraft);
        GetComponent<ProgressBar>().StopProgressBar();
        isCrafting = false;
        GameObject result = Instantiate(recipe.result);
        result.transform.position = resultLayDownPoint.position;
    }
}
