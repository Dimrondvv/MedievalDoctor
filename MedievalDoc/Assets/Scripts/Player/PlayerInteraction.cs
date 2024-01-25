using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    private PlayerInputActions playerInputActions;
    private PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += PlayerInteract;
    }



    void PlayerInteract(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    {
        
        Collider[] hitColliders = Physics.OverlapBox(transform.rotation * (Vector3.forward - new Vector3(0, 0, 0.25f)) + transform.position + new Vector3(0, 1f, 0), new Vector3(0.25f, 1f, 0.25f), transform.rotation);

        Collider highestCollider = hitColliders[0];

        foreach(Collider collider in hitColliders)
        {                
            if(collider.transform.position.y > highestCollider.transform.position.y)
                highestCollider = collider;
        }
        if (UIManager.Instance.IsNotebookEnabled)
        {
            UIManager.Instance.ChangeNotebookState(null);
        }
        else if (highestCollider.GetComponent<PlayerController>() == null)
        {
            PlayerController.OnInteract.Invoke(highestCollider.gameObject, controller);
        }
        
    }

}

