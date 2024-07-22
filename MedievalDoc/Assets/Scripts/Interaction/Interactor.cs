using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private LayerMask _laydownMask;

    private readonly Collider[] _colliders = new Collider[10];
    private readonly Collider[] _laydownColliders = new Collider[10];
    [SerializeField] private int _numFound;
    [SerializeField] private int _numLaydownFound;

    private PlayerInputActions playerInputActions;
    private bool isInteracting;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.InteractPress.performed += OnInteractPressPerformed;
        playerInputActions.Player.InteractPress.canceled += OnInteractPressCanceled;
        playerInputActions.Player.Pickup.performed += OnPickupPerformed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.InteractPress.performed -= OnInteractPressPerformed;
        playerInputActions.Player.InteractPress.canceled -= OnInteractPressCanceled;
        playerInputActions.Player.Pickup.performed -= OnPickupPerformed;
    }

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        _numLaydownFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _laydownColliders, _laydownMask);

        if (isInteracting && _numFound > 0)
        {
            var closestInteractable = GetClosestInteractable();
            if (closestInteractable != null)
            {
                // Handle continuous interaction logic here
                // For example, closestInteractable.ContinueInteract(this);
            }
        }
    }

    private Collider GetClosestInteractable()
    {
        Collider closestCollider = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < _numFound; i++)
        {
            float distance = Vector3.Distance(_interactionPoint.position, _colliders[i].transform.position);
            if (distance < closestDistance && _colliders[i].GetComponent<IInteract>() != null)
            {
                closestDistance = distance;
                closestCollider = _colliders[i];
            }
        }

        return closestCollider;
    }

    private Collider GetClosestLaydownPoint()
    {
        Collider closestCollider = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < _numLaydownFound; i++)
        {
            float distance = Vector3.Distance(_interactionPoint.position, _laydownColliders[i].transform.position);
            if (distance < closestDistance && _laydownColliders[i].GetComponent<IInteract>() == null)
            {
                // Check if the laydown point is already occupied by another interactable object
                bool isOccupied = Physics.CheckSphere(_laydownColliders[i].transform.position, 0.1f, _interactableMask);
                if (!isOccupied)
                {
                    closestDistance = distance;
                    closestCollider = _laydownColliders[i];
                }
            }
        }

        return closestCollider;
    }

    private void OnInteractPressPerformed(InputAction.CallbackContext context)
    {
        var closestInteractable = GetClosestInteractable();
        if (closestInteractable != null)
        {
            closestInteractable.GetComponent<IInteract>().Interact(this);
            isInteracting = true;
            Debug.Log("Interaction started with " + closestInteractable.name);
        }
    }

    private void OnInteractPressCanceled(InputAction.CallbackContext context)
    {
        isInteracting = false;
        var closestInteractable = GetClosestInteractable();
        if (closestInteractable != null)
        {
            Debug.Log("Interaction stopped with " + closestInteractable.name);
        }
    }

    private void OnPickupPerformed(InputAction.CallbackContext context)
    {
        var playerController = App.Instance.GameplayCore.PlayerManager.PickupController.GetPickupController();

        if (playerController.PickedItem == null)
        {
            // Try to find the closest interactable object to pick up
            var closestInteractable = GetClosestInteractable();
            if (closestInteractable != null)
            {
                closestInteractable.GetComponent<IInteract>().Pickup(this);
                Debug.Log("Picked up " + closestInteractable.name);
            }
        }
        else
        {
            // Try to find the closest valid laydown point to put down the item
            var closestLaydownPoint = GetClosestLaydownPoint();
            if (closestLaydownPoint != null)
            {
                playerController.PutDownItemAt(closestLaydownPoint.transform);
                Debug.Log("Put down item at " + closestLaydownPoint.name);
            }
            else
            {
                Debug.LogWarning("No valid laydown point found");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
