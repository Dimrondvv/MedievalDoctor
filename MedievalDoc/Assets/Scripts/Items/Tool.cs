using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    //Key - bool which defines if symptom action is valid
    [SerializeField] private SymptomOptions symptomAdded;
    [SerializeField] private SymptomOptions symptomRemoved;
    [SerializeField] private SymptomOptions symptomChecked;
    [SerializeField] private Sprite itemIcon;

    //PickupTool - Later add to antoher script
    [SerializeField] GameObject player;
    [SerializeField] Transform toolPickupPoint;
    [SerializeField] private PlayerController playerController;
    private PlayerInputActions playerInputActions;

    private void AddSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || !symptomAdded.isValid)
            return;

        patient.sickness.AddSymptom(symptomAdded.symptom);
        PatientEventManager.Instance.OnAddSymptom.Invoke(tool, patient);

    }
    private void RemoveSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || !symptomRemoved.isValid)
            return;

        patient.sickness.RemoveSymptom(symptomAdded.symptom);

        if(patient.sickness.CheckIfCured())
            PatientEventManager.Instance.OnCureDisease.Invoke(patient);
        else
            PatientEventManager.Instance.OnRemoveSymptom.Invoke(tool, patient);

    }
    private void CheckSymptom(GameObject tool, Patient patient)
    {
        if (tool != this.gameObject || !symptomChecked.isValid)
            return;

        bool isPresent = patient.sickness.CheckSymptom(symptomChecked.symptom);
        if (isPresent)
        {
            PatientEventManager.Instance.OnCheckSymptom.Invoke(symptomChecked.symptom, patient);
        }

    }

    private void Start()
    {
        PatientEventManager.Instance.OnToolInteract.AddListener(CheckSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(RemoveSymptom);
        PatientEventManager.Instance.OnToolInteract.AddListener(AddSymptom);

        // Pickup tool event Listener
        PlayerController.OnPickup.AddListener(PickupTool);
        PlayerController.OnPutdown.AddListener(PutdownTool);
    }

    private void PickupTool(GameObject pickedTool)
    {
        if (pickedTool == this.gameObject)
        {
            playerController.SetPickedItem(pickedTool);
            Debug.Log("Podnieœ");
            pickedTool.GetComponent<Collider>().enabled = false;
            pickedTool.transform.position = toolPickupPoint.position;
            pickedTool.transform.SetParent(player.transform);

            var lastChild = player.transform.childCount - 1;
            //Debug.Log(picked);

            player.transform.GetChild(lastChild).localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void PutdownTool(PlayerController pickedTool, Transform pickupPoint)
    {
        
        if (pickedTool.PickedItem != null && playerController.PickedItem.GetComponent<Tool>() != null && pickupPoint)
        { 

            GameObject putDownFurniture = pickedTool.PickedItem;
            putDownFurniture.transform.position = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.position;
            putDownFurniture.transform.rotation = pickupPoint.GetComponentInChildren<ItemLayDownPoint>().transform.rotation;
            putDownFurniture.transform.SetParent(pickupPoint);
            putDownFurniture.GetComponent<Collider>().enabled = true;
            playerController.SetPickedItem(null);
            
        }
    }

    [System.Serializable]
    struct SymptomOptions
    {
        public bool isValid;
        public Symptom symptom;
    }
}
