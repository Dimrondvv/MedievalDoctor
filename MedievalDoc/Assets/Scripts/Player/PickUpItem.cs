using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int picUpRange = 1;
    public GameObject pickedItem;

    [SerializeField] Transform furniturePickupPoint;
    private PlayerInputActions playerInputActions;
    int layerMask = 1 << 6; // Bit shift the index of the layer (6) to get a bit mask
    bool picked = false;
    


    Transform objTransform;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        
        playerInputActions.Player.RotateBlueprint.performed += RotateBlueprint_performed;
        
    }

    private void RotateBlueprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext callback) { // Rotate Blueprint
        if (pickedItem != null) {
            float inputVector = playerInputActions.Player.RotateBlueprint.ReadValue<float>();
            if (inputVector == 1) {
                pickedItem.GetComponent<SnapBlueprint>().Blueprint.transform.eulerAngles += new Vector3(0, 90f, 0);
            } else {
                pickedItem.GetComponent<SnapBlueprint>().Blueprint.transform.eulerAngles -= new Vector3(0, 90f, 0);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (!picked)
            {
                Collider[] hitColliders = Physics.OverlapBox(transform.rotation * Vector3.forward + transform.position + new Vector3(0, 1f, 0), transform.localScale + new Vector3(0, 1f, 0), transform.rotation, layerMask); // get All objects when u click "R"
                if (hitColliders.Length == 1) // Works when ther is only one object
                { // Pick up object
                    //Debug.Log(hitColliders[0].gameObject.name);
                    objTransform = hitColliders[0].transform;
                    pickedItem = hitColliders[0].transform.gameObject;
                    pickedItem.GetComponent<Collider>().enabled = false;
                    pickedItem.transform.position = furniturePickupPoint.position; //transform.rotation * Vector3.forward + transform.position + new Vector3(0, 1f, 0);
                    pickedItem.GetComponent<SnapBlueprint>().CreateBlueprint();
                    picked = true;
                    hitColliders[0].transform.SetParent(transform);

                    var lastChild = this.transform.childCount - 1; 
                    this.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
                }

            } else { // Put down object
                if (pickedItem.GetComponent<SnapBlueprint>().Blueprint.GetComponent<BlueprintTrigger>().isPlacable)
                {
                    pickedItem.transform.position = pickedItem.GetComponent<SnapBlueprint>().Blueprint.transform.position;
                    pickedItem.transform.rotation = pickedItem.GetComponent<SnapBlueprint>().Blueprint.transform.rotation;
                    pickedItem.GetComponent<SnapBlueprint>().DestroyBlueprint();
                    objTransform.transform.SetParent(null);
                    picked = false;
                    pickedItem.GetComponent<Collider>().enabled = true;
                    pickedItem = null;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, picUpRange, layerMask))
            {
               
            }
        }
    }

    // Draw Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.rotation * Vector3.forward + transform.position + new Vector3(0, 1f, 0), transform.localScale + new Vector3(0, 1f, 0));
    }
}
