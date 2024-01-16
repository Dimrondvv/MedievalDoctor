using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientPickup : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] GameObject player;
    [SerializeField] Transform toolPickupPoint;
    private void Start()
    {
        PlayerController.OnPickup.AddListener(PickupPatient);
        PlayerController.OnPutdown.AddListener(PutdownPatient);
    }

    private void PickupPatient(GameObject pickedPatient)
    {
        if (pickedPatient == this.gameObject)
        {
            playerController.SetPickedItem(pickedPatient);
            Debug.Log("Podnieœ");
            pickedPatient.GetComponent<Collider>().enabled = false;
            pickedPatient.transform.position = toolPickupPoint.position;
            pickedPatient.transform.SetParent(player.transform);

            var lastChild = player.transform.childCount - 1;

            player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void PutdownPatient(PlayerController pickedTool, Transform pickupPoint)
    {
        // TO ADD PUTING PATIENT ON FLOOR 
        if (pickedTool.PickedItem != null && playerController.PickedItem.GetComponent<PatientPickup>() != null && pickupPoint)
        {
            GameObject putDownFurniture = pickedTool.PickedItem;
            Quaternion rotation = pickupPoint.GetComponentInChildren<PatientLayDownPoint>().transform.rotation;
            putDownFurniture.transform.position = pickupPoint.GetComponentInChildren<PatientLayDownPoint>().transform.position;
            putDownFurniture.transform.eulerAngles = new Vector3(-90f, rotation.y, rotation.z);
            putDownFurniture.transform.SetParent(pickupPoint);
            putDownFurniture.GetComponent<Collider>().enabled = true;
            playerController.SetPickedItem(null);
        }
    }
}
