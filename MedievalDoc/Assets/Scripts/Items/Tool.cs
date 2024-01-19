using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    //Key - bool which defines if symptom action is valid
    [SerializeField] private Sprite itemIcon;

    [SerializeField] private List<Symptom> symptomsAdded;
    [SerializeField] private List<Symptom> symptomsRemoved;
    [SerializeField] private List<Symptom> symptomsChecked;


    //PickupTool - Later add to antoher script
    [SerializeField] GameObject player;
    [SerializeField] Transform toolPickupPoint;
    [SerializeField] private PlayerController playerController;
    private PlayerInputActions playerInputActions;
    public Sprite ItemIcon
    {
        get { return itemIcon; }
    }

    private void AddSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || symptomsAdded.Count == 0)
            return;
        Debug.Log("Add symptom");
        foreach(var symptom in symptomsAdded)
            PatientEventManager.Instance.OnAddSymptom.Invoke(symptom, patient);

    }
    private void RemoveSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || symptomsRemoved.Count == 0)
            return;

        foreach (Symptom symptom in symptomsRemoved)
             PatientEventManager.Instance.OnRemoveSymptom.Invoke(symptom, patient);
    }
    private void CheckSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || symptomsChecked.Count == 0)
            return;
        foreach (Symptom symptom in symptomsChecked)
        {
            bool isPresent = patient.sickness.CheckSymptom(symptom);
            if (isPresent)
            {
                PatientEventManager.Instance.OnCheckSymptom.Invoke(symptom, patient);
            }
        }
    }

    private void Start()
    {
        PatientEventManager.Instance.OnToolInteract.AddListener(AddSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(RemoveSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(CheckSymptom);

        // Pickup tool event Listener
        PlayerController.OnPickup.AddListener(PickupTool);
        PlayerController.OnPutdown.AddListener(PutdownTool);
    }

    private void PickupTool(GameObject pickedTool, Transform objectPoint)
    {
        if (pickedTool == this.gameObject)
        {
            if (objectPoint != null) {
                objectPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = false;
            }

            playerController.SetPickedItem(pickedTool);
            pickedTool.GetComponent<Collider>().enabled = false;
            pickedTool.transform.position = toolPickupPoint.position;
            pickedTool.transform.SetParent(player.transform);

            var lastChild = player.transform.childCount - 1;

            player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void PutdownTool(PlayerController pickedTool, Transform pickupPoint)
    {
        
        if (pickedTool.PickedItem != null && playerController.PickedItem.GetComponent<Tool>() != null && pickupPoint && pickupPoint.GetComponentInChildren<ItemLayDownPoint>() && !pickupPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied)
        { 
            GameObject putDownTool = pickedTool.PickedItem;
            pickupPoint.GetComponentInChildren<ItemLayDownPoint>().checkIfOccupied = true;
            putDownTool.transform.position = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.position;
            putDownTool.transform.rotation = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.rotation;
            putDownTool.transform.SetParent(pickupPoint);
            putDownTool.GetComponent<Collider>().enabled = true;
            playerController.SetPickedItem(null);  
        }
    }

}
