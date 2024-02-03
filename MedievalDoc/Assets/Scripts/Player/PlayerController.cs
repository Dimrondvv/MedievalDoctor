using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;

    [SerializeField] private float turnSpeed;
    Vector3 velocity;

    private PlayerInputActions playerInputActions;
    private Quaternion rotation;
    private Vector3 moveDirection;
    private GameObject pickedItem;
    [SerializeField] private Animator animator;
    private int moveValue;

    public static UnityEvent<GameObject, PlayerController> OnInteract = new UnityEvent<GameObject, PlayerController>();

    public static UnityEvent<GameObject, PlayerController> OnInteractEnter = new UnityEvent<GameObject, PlayerController>();

    public static UnityEvent<GameObject, Transform> OnPickup = new UnityEvent<GameObject, Transform>();
    public static UnityEvent<PlayerController, Transform> OnPutdown = new UnityEvent<PlayerController, Transform>();
    public GameObject PickedItem { 
        get { return pickedItem; } 
        set { pickedItem = value; }
    }


    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.RotateBlueprint.performed += RotateBlueprint_performed;

        // Running turned off
        //playerInputActions.Player.Run.performed += Running;
        //playerInputActions.Player.Run.canceled += Walking;
    }

    // private void Running(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    // {
    //     speed = runSpeed;
    //     animator.SetInteger("moving", 2);
    // }

    // private void Walking(UnityEngine.InputSystem.InputAction.CallbackContext callback)
    // {
    //     speed = walkSpeed;
    // }

    private void FixedUpdate(){

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
  
        inputVector = inputVector.normalized;

        //Debug.Log(inputVector);

        if (inputVector.x != 0 || inputVector.y != 0)
        {
            //Debug.Log("walking");
            //animator.SetInteger("moving", 1);
            moveValue = 1;
            //animator.SetInteger("moving",1);
        }
        else
        {
            //Debug.Log("idling");
            moveValue = 0;
            //animator.SetInteger("moving",0);
        }
        //Debug.Log(moveValue);
        animator.SetInteger("moving", moveValue);

        moveDirection = new Vector3(inputVector.x,0f,inputVector.y);


        float playerRadius = GetComponent<CapsuleCollider>().radius;
        float playerHeight = GetComponent<CapsuleCollider>().height;

        float moveDistance = Time.deltaTime*speed;

        bool canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove){
            Vector3 moveDirectionX = new Vector3(moveDirection.x,0,0).normalized;
            canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirectionX, moveDistance);
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * turnSpeed);
            if (canMove){
                moveDirection = moveDirectionX;
            }
            else{
                Vector3 moveDirectionZ = new Vector3(0,0,moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if(canMove){
                    moveDirection = moveDirectionZ;
                }
                else{
                    // Cant movee
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime*turnSpeed);
        rotation = this.transform.rotation;
    }

    public Vector3 GetPlayerRoundedRotation()
    {
        var vec = transform.eulerAngles;
        vec.x = 0;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = 0;

        return vec;
    }

    public Vector3 GetPlayerMoveDirection() {
        return moveDirection;
    }

    public void SetPickedItem(GameObject pickedObject) {
        pickedItem = pickedObject;
        
    }

    public Transform GetToolPickupPoint() {
        return this.GetComponentInChildren<FindToolPickupPoint>().transform;
    }

    public Transform GetFurniturePickupPoint() {
        return this.GetComponentInChildren<FindFurniturePickupPoint>().transform;
    }

    public GameObject GetFingerObject(){
        return this.GetComponentInChildren<FindToolPickupPoint>().gameObject;
    }

    public GameObject GetPlayerGameObject() {
        return this.gameObject;
    }

    public PlayerController GetPlayerController() {
        return this;
    }

    private void RotateBlueprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext callback) { // Rotate Blueprint
        if (pickedItem != null) {
            float inputVector = playerInputActions.Player.RotateBlueprint.ReadValue<float>();
            if (inputVector == 1) {
                pickedItem.GetComponent<SnapBlueprint>().Blueprint.transform.eulerAngles += new Vector3(0, 90f, 0);
            } else {
                pickedItem.GetComponent<SnapBlueprint>().Blueprint.transform.eulerAngles -= new Vector3(0, 90f, 0);
            }
        }
    }
}
