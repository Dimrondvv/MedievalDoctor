using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBlueprint : MonoBehaviour
{
    private GameObject blueprint;
    private Vector3 storedPosition; //Variable that stores the position of the picked up item to change blueprint position on ints
    [SerializeField] PickUpItem playerItem;
    [SerializeField] Material blueprintBlue;
    [SerializeField] Material blueprintRed;

    public GameObject Blueprint
    {
        get { return blueprint; }
    }

    public void CreateBlueprint()
    {
        blueprint = Instantiate(playerItem.pickedItem, playerItem.transform.rotation * Vector3.forward + playerItem.pickedItem.transform.position, playerItem.transform.rotation);
        blueprint.GetComponent<MeshRenderer>().material = blueprintBlue;
        blueprint.GetComponent<Collider>().isTrigger = true;
        blueprint.GetComponent<Collider>().enabled = true;
        //blueprint.AddComponent<Rigidbody>();
        blueprint.AddComponent<BlueprintTrigger>();
        blueprint.GetComponent<BlueprintTrigger>().blueprintBlue = blueprintBlue;
        blueprint.GetComponent<BlueprintTrigger>().blueprintRed = blueprintRed;
    }
    public void DestroyBlueprint()
    {
        Destroy(blueprint);
    }

    Vector3 RoundPosition(Vector3 position)
    {
        float roundX = Mathf.RoundToInt(position.x);
        float roundZ = Mathf.RoundToInt(position.z);
        Vector3 roundedPos = new Vector3(roundX, 0.5f, roundZ);
        return roundedPos;
    }
    
    bool CheckIfInt(Vector3 position)
    {
        return position.x == Mathf.RoundToInt(position.x) && position.z == Mathf.RoundToInt(position.z);
    }
    private void Update()
    {
        if (blueprint == null)
            return;
        blueprint.transform.position = RoundPosition(playerItem.transform.rotation * Vector3.forward + playerItem.pickedItem.transform.position);

    }
}
