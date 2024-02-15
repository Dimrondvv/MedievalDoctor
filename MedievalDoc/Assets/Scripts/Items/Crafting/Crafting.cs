using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes;
    [SerializeField] private List<GameObject> insertedItems;
    [SerializeField] private Transform resultLayDownPoint;

    private void Start()
    {
        PickupController.OnInteract.AddListener(CraftItem);
        PickupController.OnPickup.AddListener(RemoveItem);
        PickupController.OnPutdown.AddListener(InsertItem);
    }

    public void InsertItem(PickupController player, Transform objectPoint)
    {
        if (objectPoint == null)
            return;
        if (objectPoint.gameObject != gameObject || player.PickedItem.GetComponent<Furniture>() || player.PickedItem.GetComponent <Patient>())
            return;

        insertedItems.Add(player.PickedItem);
        insertedItems[insertedItems.Count - 1].transform.parent = null;
        insertedItems[insertedItems.Count - 1].gameObject.SetActive(false);
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
        if (station != gameObject)
            return;

        Recipe validRecipe = null;
        foreach(var item in recipes)
        {
            if (item.CheckReq(insertedItems))
                validRecipe = item;
        }

        if (validRecipe != null)
        {
            StartCoroutine(CraftingCoroutine(validRecipe));
        }
    }

    IEnumerator CraftingCoroutine(Recipe recipe)
    {
        GetComponent<CraftProgressBar>().StartProgressBar(recipe.timeRequiredToCraft);
        yield return new WaitForSeconds(recipe.timeRequiredToCraft);
        GetComponent<CraftProgressBar>().StopProgressBar();

        GameObject result = Instantiate(recipe.result);
        insertedItems.Clear();
        result.transform.position = resultLayDownPoint.position;
    }
}
