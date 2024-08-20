using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] float interactionTime;
    private bool isCardOn = false;
    public float InteractionTime { get { return interactionTime; } set { interactionTime = value; } }
    private AudioSource cardOpen;

    void Start()
    {
        PickupController.OnInteract.AddListener(InteractWithCard);
        cardOpen = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isCardOn && (!Interactor.InteractableCollider || !Interactor.IsOpen))
        {
            App.Instance.GameplayCore.UIManager.DisablePatientCard();
            isCardOn = false;
        }
    }
    private void InteractWithCard(GameObject interactionObject, PickupController player)
    {
        if (interactionObject != gameObject)
            return;
        cardOpen.Play();
        App.Instance.GameplayCore.UIManager.ChangePatientCardState();
        isCardOn = !isCardOn;
        Interactor.isOpen = true;
    }


}
