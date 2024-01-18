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

    private void PickupPatient(GameObject pickedPatient, Transform objectPoint)
    {
        if (pickedPatient == this.gameObject)
        {
            if (objectPoint != null) {
                objectPoint.GetComponentInChildren<PatientLayDownPoint>().IfOccupied = false;
            }
            
            playerController.SetPickedItem(pickedPatient);
            pickedPatient.GetComponent<Collider>().enabled = false;
            pickedPatient.transform.position = toolPickupPoint.position;
            pickedPatient.transform.SetParent(player.transform);
            var lastChild = player.transform.childCount - 1;

            player.transform.GetChild(lastChild).localEulerAngles = Vector3.zero;
        }
    }

    private void PutdownPatient(PlayerController pickedPatient, Transform pickupPoint)
    {
        // TO ADD PUTING PATIENT ON FLOOR 
        if (pickedPatient.PickedItem != null && playerController.PickedItem.GetComponent<PatientPickup>() != null && pickupPoint && pickupPoint.GetComponentInChildren<PatientLayDownPoint>() && !pickupPoint.GetComponentInChildren<PatientLayDownPoint>().IfOccupied)
        {
            GameObject putDownFurniture = pickedPatient.PickedItem;
            //Quaternion rotation = pickupPoint.GetComponentInChildren<PatientLayDownPoint>().transform.rotation;
            pickupPoint.GetComponentInChildren<PatientLayDownPoint>().IfOccupied = true;
            putDownFurniture.transform.position = pickupPoint.GetComponentInChildren<PatientLayDownPoint>().transform.position;
            putDownFurniture.transform.rotation = pickupPoint.GetComponentInChildren<PatientLayDownPoint>().transform.rotation;
            putDownFurniture.transform.SetParent(pickupPoint);
            putDownFurniture.GetComponent<Collider>().enabled = true;
            playerController.SetPickedItem(null);
        }
    }
}
