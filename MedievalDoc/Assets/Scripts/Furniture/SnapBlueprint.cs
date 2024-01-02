using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBlueprint : MonoBehaviour
{
    private GameObject blueprint;
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


    private void Update()
    {
        if (blueprint == null)
            return;
        

    }



}
