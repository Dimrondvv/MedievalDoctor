using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBlueprint : MonoBehaviour
{
    private GameObject blueprint;
    private Vector3 storedPosition; //Variable that stores the position of the picked up item to change blueprint position on ints
    [SerializeField] PickUpItem playerItem;
    public void CreateBlueprint()
    {
        blueprint = Instantiate(playerItem.pickedItem, playerItem.transform.rotation * Vector3.forward + playerItem.pickedItem.transform.position, playerItem.transform.rotation);
        //TODO: Enable shader
    }
    public void DestroyBlueprint()
    {
        Destroy(blueprint);
    }

    Vector3 RoundPosition(Vector3 position)
    {
        float roundX = Mathf.Floor(position.x);
        float roundZ = Mathf.Floor(position.z);
        Vector3 roundedPos = new Vector3(roundX, 0.5f, roundZ);
        return roundedPos;
    }
    private void Update()
    {
        if (blueprint == null)
            return;

        blueprint.transform.position = playerItem.transform.rotation * Vector3.forward + playerItem.pickedItem.transform.position;

        //TODO: set blueprint color to red if can't place and blue if can

    }



}
