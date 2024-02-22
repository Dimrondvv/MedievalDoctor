using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupController : MonoBehaviour {
    private PlayerInputActions playerInputActions;

    private GameObject pickedItem;

    public static UnityEvent<GameObject, PickupController> OnInteract = new UnityEvent<GameObject, PickupController>();

    public static UnityEvent<GameObject, PickupController> OnInteractEnter = new UnityEvent<GameObject, PickupController>();

    public static UnityEvent<GameObject, Transform> OnPickup = new UnityEvent<GameObject, Transform>();
    public static UnityEvent<PickupController, Transform> OnPutdown = new UnityEvent<PickupController, Transform>();
    public GameObject PickedItem {
        get { return pickedItem; }
        set { pickedItem = value; }
    }


    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.RotateBlueprint.performed += RotateBlueprint_performed;
    }

    

    public Vector3 GetPlayerRoundedRotation() {
        var vec = transform.eulerAngles;
        vec.x = 0;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = 0;

        return vec;
    }

    public void SetPickedItem(GameObject pickedObject) {
        pickedObject.transform.position = GetToolPickupPoint().position;
        pickedObject.transform.SetParent(GetFingerObject().transform);
        pickedObject.transform.localEulerAngles = new Vector3(0, -60f, 0);//GetToolPickupPoint().rotation * Vector3.forward;
        pickedObject.GetComponent<Collider>().enabled = false;
        PickedItem = pickedObject;
    }

    public Transform GetToolPickupPoint() {
        return this.GetComponentInChildren<FindToolPickupPoint>().transform;
    }

    public Transform GetFurniturePickupPoint() {
        return this.GetComponentInChildren<FindFurniturePickupPoint>().transform;
    }

    public GameObject GetFingerObject() {
        return this.GetComponentInChildren<FindToolPickupPoint>().gameObject;
    }

    public GameObject GetPlayerGameObject() {
        return this.gameObject;
    }

    public PickupController GetPickupController() {
        return this;
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
}
