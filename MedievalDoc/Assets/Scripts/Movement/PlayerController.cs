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
    private Quaternion rotation;
    private Vector3 moveDirection;

    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void FixedUpdate(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
  
        inputVector = inputVector.normalized;
       
        moveDirection = new Vector3(inputVector.x,0f,inputVector.y);


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
}
