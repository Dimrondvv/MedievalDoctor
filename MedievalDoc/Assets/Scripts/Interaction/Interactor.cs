using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    private GameObject currentOutlinedObject;
    private PickupController pickupController;
    private ProgressBar progressBar;
    private AudioSource interactionAudio;
    private Coroutine interactionCoroutine;

    private static Collider closestInteractable;
    public static bool isOpen;
    public static bool IsOpen { get { return isOpen; } }
    public static Collider InteractableCollider { get { return closestInteractable; } }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.InteractPress.started += OnInteractPressPerformed;
        playerInputActions.Player.InteractPress.canceled += OnInteractPressCanceled;
        playerInputActions.Player.Pickup.performed += OnPickupPerformed;

        // Initialize pickupController and progressBar
        pickupController = GetComponent<PickupController>();
        progressBar = GetComponentInChildren<ProgressBar>();
        interactionAudio = GetComponent<AudioSource>();

        if (pickupController == null)
        {
            Debug.LogError("PickupController not found on the GameObject");
        }
        if (progressBar == null)
        {
            Debug.LogError("ProgressBar not found on the GameObject");
        }
        if (interactionAudio == null)
        {
            Debug.LogError("AudioSource not found on the GameObject");
        }
    }

    private void OnDestroy()
    {
        playerInputActions.Player.InteractPress.started -= OnInteractPressPerformed;
        playerInputActions.Player.InteractPress.canceled -= OnInteractPressCanceled;
        playerInputActions.Player.Pickup.performed -= OnPickupPerformed;
    }

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        _numLaydownFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _laydownColliders, _laydownMask);

        if (_numFound > 0)
        {
            closestInteractable = GetClosestInteractable();
            if (closestInteractable != null)
            {
                // If there is a current outlined object, remove its outline if it's different from the closest interactable
                if (currentOutlinedObject != null && currentOutlinedObject != closestInteractable.gameObject)
                {
                    RemoveOutline(currentOutlinedObject);
                    isOpen = false;
                }

                currentOutlinedObject = closestInteractable.gameObject;

                if (currentOutlinedObject.GetComponent<Outline>() == null)
                {
                    AddOutline(currentOutlinedObject);
                }
            }
        }
        else if (currentOutlinedObject != null)
        {
            RemoveOutline(currentOutlinedObject);
            currentOutlinedObject = null;
            closestInteractable = null;
            if (interactionCoroutine != null)
            {
                StopCoroutine(interactionCoroutine);
                interactionCoroutine = null;
                if (progressBar != null) progressBar.StopProgressBar();
                if (interactionAudio != null) interactionAudio.Stop();
            }
        }
    }

    private void AddOutline(GameObject obj)
    {
        var outline = obj.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
    }

    private void RemoveOutline(GameObject obj)
    {
        var outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            Destroy(outline);
        }
    }

    private Collider GetClosestInteractable()
    {
        Collider closestCollider = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < _numFound; i++)
        {
            float distance = Vector3.Distance(_interactionPoint.position, _colliders[i].transform.position);
            if (distance < closestDistance && (_colliders[i].GetComponent<IInteract>() != null || _colliders[i].GetComponent<IInteractable>() != null || _colliders[i].CompareTag("IsInteractable")))
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
                if (!isOccupied || _laydownColliders[i].CompareTag("Crafting"))
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
            interactionCoroutine = StartCoroutine(InteractionCoroutine(closestInteractable));
        }
    }

    private void OnInteractPressCanceled(InputAction.CallbackContext context)
    {
        if (interactionCoroutine != null)
        {
            StopCoroutine(interactionCoroutine);
            interactionCoroutine = null;
            if (progressBar != null) progressBar.StopProgressBar();
            if (interactionAudio != null) interactionAudio.Stop();
        }
    }

    private IEnumerator InteractionCoroutine(Collider interactable)
    {
        float interactTime = 0f; // Default interaction time
        var pickedItemComponent = pickupController.PickedItem?.GetComponent<IInteractable>();
        var interactableComponent = interactable.GetComponent<IInteractable>();

        if (pickedItemComponent != null)
        {
            if (interactableComponent != null)
            {
                interactTime = interactableComponent.InteractionTime;
            } else
            {
                interactTime = pickedItemComponent.InteractionTime;
            }
        } else if (interactableComponent != null)
        {
            interactTime = interactableComponent.InteractionTime;
        }

        if (interactTime > 0f)
        {
            if (progressBar != null) progressBar.StartProgressBar(interactTime);
            if (interactionAudio != null) interactionAudio.Play();

            yield return new WaitForSeconds(interactTime);

            // Interaction completed
            if (progressBar != null) progressBar.StopProgressBar();
            if (interactionAudio != null) interactionAudio.Stop();
        }

        PickupController.OnInteract.Invoke(interactable.gameObject, pickupController);
    }

    private void OnPickupPerformed(InputAction.CallbackContext context)
    {
        var playerController = App.Instance.GameplayCore.PlayerManager.PickupController.GetPickupController();

        if (playerController.PickedItem == null)
        {
            // Try to find the closest interactable object to pick up
            var closestInteractable = GetClosestInteractable();
            if (closestInteractable != null && closestInteractable.HasComponent<IInteract>())
            {
                closestInteractable.GetComponent<IInteract>().Pickup(this);
                Debug.Log("Picked up " + closestInteractable.name);
            }
        }
        else if (playerController.PickedItem != null)
        {
            // Try to find the closest valid laydown point to put down the item
            var closestLaydownPoint = GetClosestLaydownPoint();
            if (closestLaydownPoint != null)
            {
                playerController.PutDownItemAt(closestLaydownPoint.transform);
                Debug.Log("Put down item at " + closestLaydownPoint.name);
            }
            else if (closestLaydownPoint.CompareTag("Crafting"))
            {

                PickupController.OnPutdown?.Invoke(playerController, closestLaydownPoint.transform);
                Debug.Log("Put down item at " + closestLaydownPoint.name);
            }
            else if (closestLaydownPoint == null)
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
