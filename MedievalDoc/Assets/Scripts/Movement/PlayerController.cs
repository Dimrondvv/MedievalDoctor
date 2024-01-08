using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
     Vector3 velocity;

    //CharacterController characterController;

    private PlayerInputActions playerInputActions;

    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    // Dobry movement ale nie działa rotacja
    // private void Start(){
    //     characterController = GetComponent<CharacterController>();
    // }

    // private void Update(){
    //     PlayerMove();
    // }

    // private void PlayerMove(){
    //     float x = Input.GetAxis("Horizontal");
    //     float z = Input.GetAxis("Vertical");

    //     Vector3 move = transform.right * x + transform.forward * z;
    //     characterController.Move(move*speed*Time.deltaTime);

    //     // Vector3 rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime, 0);
    //     // this.transform.Rotate(rotation);
    //     Vector3 moveDirection = new Vector3(x,0f,z);
    //     transform.rotation= Quaternion.Euler(Vector3.Slerp(transform.forward, move, Time.deltaTime*turnSpeed));
    // }


    private void FixedUpdate(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
  
        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3(inputVector.x,0f,inputVector.y);

        float playerRadius = GetComponent<CapsuleCollider>().radius;
        float playerHeight = GetComponent<CapsuleCollider>().height;

        float moveDistance = Time.deltaTime*speed;

        bool canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirection, moveDistance);




        if (!canMove){

            Vector3 moveDirectionX = new Vector3(moveDirection.x,0,0).normalized;
            canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirectionX, moveDistance);

            if(canMove){
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


    }
}
