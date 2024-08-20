using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookStand : MonoBehaviour, IInteractable
{
    [SerializeField] float interactionTime;
    private bool isNoteBookOn = false;
    public float InteractionTime { get { return interactionTime; } set { interactionTime = value; } }
    private AudioSource bookAudio;


    // Start is called before the first frame update
    void Start()
    {
        PickupController.OnInteract.AddListener(InteractWithStand);
        bookAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isNoteBookOn && (!Interactor.InteractableCollider || !Interactor.IsOpen))
        {
            InteractWithStand(gameObject, null);
        }
    }

    private void InteractWithStand(GameObject interactionObject, PickupController player)
    {
        if (interactionObject != gameObject)
            return;
        bookAudio.Play();
        App.Instance.GameplayCore.UIManager.ChangeNotebookState();
        isNoteBookOn = !isNoteBookOn;
        Interactor.isOpen = true;
    }



}
