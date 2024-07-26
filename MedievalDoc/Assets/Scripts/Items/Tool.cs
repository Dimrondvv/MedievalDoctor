using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Data;
public class Tool : MonoBehaviour, IInteractable
{
    //Key - bool which defines if symptom action is valid
    private Sprite itemIcon;
    public Data.Tool toolData;
    private float interactionTime;

    public static UnityEvent<GameObject, Patient> OnToolInteract = new UnityEvent<GameObject, Patient>(); //Invoked when interacted with a tool

    public float InteractionTime { get { return interactionTime; } }

    public Sprite ItemIcon
    {
        get { return itemIcon; }
    }

    float IInteractable.InteractionTime { get => interactionTime; set => interactionTime = value; }

    private void Start()
    {
        InstantiateTool();
        OnToolInteract.AddListener(UseTool);
    }

    private void InstantiateTool()
    {
        gameObject.name.Replace("(clone)", "").Trim();
        toolData = HelperFunctions.ToolLookup(gameObject);

        if (toolData == null)
            return;

        itemIcon = Resources.Load<Sprite>("Icons/" + toolData.toolIcon);

    }

    private void UseTool(GameObject tool, Patient patient)
    {
        if (tool != gameObject)
            return;

        if (toolData.symptomsRemoved != null)
            RemoveSymptom(patient);
        if (toolData.symptomsAdded != null)
            AddSymptom(patient);

        if (toolData.oneUse != null)
        {
            Destroy(gameObject);
            App.Instance.GameplayCore.PlayerManager.PickupController.PickedItem = null;
        }
    }

    private void AddSymptom(Patient patient)
    {
        foreach(var symptom in toolData.symptomsAdded)
            Patient.OnTryAddSymptom.Invoke(HelperFunctions.SymptomLookup(symptom), patient, this);
    }
    private void RemoveSymptom(Patient patient)
    {
        foreach (var symptom in toolData.symptomsRemoved)
        {
            Patient.OnTryRemoveSymptom.Invoke(HelperFunctions.SymptomLookup(symptom), patient, this);
        }
    }
}
