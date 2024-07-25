using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionTime;
    [SerializeField] private NewsPaper newspaper;


    private bool isPaperOn = false;
    public float InteractionTime { get { return interactionTime; } set { interactionTime = value; } }


    // Start is called before the first frame update
    void Start()
    {
        PickupController.OnInteract.AddListener(InteractWithNewspaper);
    }


    private void Update()
    {
        if (isPaperOn && !Interactor.InteractableCollider)
        {
            App.Instance.GameplayCore.UIManager.DisableNewspaper();
            isPaperOn = false;
        }
    }

    private void InteractWithNewspaper(GameObject interactionObject, PickupController player)
    {
        if (interactionObject != gameObject)
            return;

        App.Instance.GameplayCore.UIManager.ChangeNewspaperState();
        isPaperOn = !isPaperOn;
    }
}
