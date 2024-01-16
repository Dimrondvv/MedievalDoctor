using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBlueprint : MonoBehaviour
{
    private GameObject blueprint;
    private Vector3 storedPosition; //Variable that stores the position of the picked up item to change blueprint position on ints
    
    //[SerializeField] PickUpItem playerItem;
    [SerializeField] Material blueprintBlue;
    [SerializeField] Material blueprintRed;
    [SerializeField] private PlayerController player;

    public GameObject Blueprint
    {
        get { return blueprint; }
    }

    public void CreateBlueprint(GameObject pickedObject)
    {
        blueprint = Instantiate(pickedObject, pickedObject.transform.rotation * Vector3.forward + pickedObject.transform.position, Quaternion.Euler(player.GetPlayerRoundedRotation()));
        blueprint.GetComponent<Collider>().isTrigger = true;
        blueprint.GetComponent<Collider>().enabled = true;
        blueprint.AddComponent<Rigidbody>();
        blueprint.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition; // Freeze rigid body position
        blueprint.AddComponent<BlueprintTrigger>();
        blueprint.GetComponent<BlueprintTrigger>().blueprintBlue = blueprintBlue;
        blueprint.GetComponent<BlueprintTrigger>().blueprintRed = blueprintRed;
        blueprint.GetComponent<BlueprintTrigger>().ChangeBlueprintToBlue(blueprint);
    }

    public void DestroyBlueprint()
    {
        Destroy(blueprint);
    }
    
    Vector3 RoundPosition(Vector3 position)
    {
        float roundX = Mathf.RoundToInt(position.x);
        float roundZ = Mathf.RoundToInt(position.z);
        Vector3 roundedPos = new Vector3(roundX, 0f, roundZ);
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

        if (player.PickedItem != null) {
            blueprint.transform.position = RoundPosition(player.PickedItem.transform.rotation * Vector3.forward + player.PickedItem.transform.position);
        }
    }
}
