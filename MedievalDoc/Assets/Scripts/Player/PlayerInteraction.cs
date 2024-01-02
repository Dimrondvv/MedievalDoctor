using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionRange))
            {
                IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.Interact();
                else
                    Debug.Log("Not a valid object");
            }
        }
    }
}
