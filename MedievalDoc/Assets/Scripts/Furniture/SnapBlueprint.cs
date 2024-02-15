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
    [SerializeField] private PickupController player;


    private void Start() {
        player = PlayerManager.Instance.PickupController.GetPickupController();
    }
    public GameObject Blueprint
    {
        get { return blueprint; }
    }

    public void CreateBlueprint(GameObject pickedObject)
    {
        blueprint = Instantiate(pickedObject, pickedObject.transform.rotation * Vector3.forward + pickedObject.transform.position, Quaternion.Euler(player.GetPlayerRoundedRotation()));
        Collider collider = blueprint.GetComponent<Collider>();
        collider.isTrigger = true;
        collider.enabled = true;

        Rigidbody rb = blueprint.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition; // Freeze rigid body position
        BlueprintTrigger bpTrigger = blueprint.AddComponent<BlueprintTrigger>();
        bpTrigger.blueprintBlue = blueprintBlue;
        bpTrigger.blueprintRed = blueprintRed;
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
