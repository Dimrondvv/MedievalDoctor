using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

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

    private IInteract GetClosestInteractable()
    {
        IInteract closestInteractable = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < _numFound; i++)
        {
            float distance = Vector3.Distance(_interactionPoint.position, _colliders[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = _colliders[i].GetComponent<IInteract>();
            }
        }

        return closestInteractable;
    }

    private void OnInteractPressPerformed(InputAction.CallbackContext context)
    {
        if (_numFound > 0)
        {
            var closestInteractable = GetClosestInteractable();
            if (closestInteractable != null && !(closestInteractable is ToolInteraction))
            {
                closestInteractable.Interact(this);
                isInteracting = true;
                Debug.Log("Interaction started with " + closestInteractable);
            }
        }
    }

    private void OnInteractPressCanceled(InputAction.CallbackContext context)
    {
        isInteracting = false;
        if (_numFound > 0)
        {
            var closestInteractable = GetClosestInteractable();
            if (closestInteractable != null)
            {
                Debug.Log("Interaction stopped with " + closestInteractable);
            }
        }
    }

    private void OnPickupPerformed(InputAction.CallbackContext context)
    {
        if (_numFound > 0)
        {
            var closestInteractable = GetClosestInteractable();
            if (closestInteractable != null)
            {
                closestInteractable.Pickup(this);
                Debug.Log("Picked up " + closestInteractable);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
