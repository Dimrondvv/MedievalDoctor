using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    // Vector3 velocity;
    // [SerializeField] Rigidbody rb;

    private bool isWalking;



    private void Update(){
        Vector2 inputVector = new Vector2(0,0);

        if(Input.GetKey(KeyCode.W)){
            inputVector.y = +1;
        }
        if(Input.GetKey(KeyCode.S)){
            inputVector.y = -1;
        }
        if(Input.GetKey(KeyCode.A)){
            inputVector.x = -1;
        }
        if(Input.GetKey(KeyCode.D)){
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3(inputVector.x,0f,inputVector.y);

        float playerRadius = 0.6f;
        float playerHeight = 0.1f;
        float moveDistance = Time.deltaTime*speed;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirection, moveDistance);

        // if (!canMove){
        //     Vector3 moveDirectionX = new Vector3(moveDirection.x,0,0).normalized;
        //     canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirection, moveDistance);

        //     if(canMove){
        //         moveDirection = moveDirectionX;
        //     }
        //     else{
        //         Vector3 moveDirectionZ = new Vector3(0,0,moveDirection.z).normalized;
        //         canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up *playerHeight, playerRadius, moveDirection, moveDistance);

        //         if(canMove){
        //             moveDirection = moveDirectionZ;
        //         }
        //         else{
        //             // Cant move
        //         }
        //     }

        // }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }



        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime*turnSpeed);
    }



    // void Start()
    // {
    //     characterController = GetComponent<CharacterController>();
    // }




    // void PlayerMove()
    // {
    //     float x = Input.GetAxis("Horizontal");
    //     float z = Input.GetAxis("Vertical");

    //     Vector3 move = transform.right * x + transform.forward * z;
    //     //rb.MovePosition(move * speed * Time.deltaTime);
    //     rb.MovePosition(transform.position + move * Time.deltaTime * speed);


    // }


    // void FixedUpdate()
    // {
    //     PlayerMove();
    // }






    // [SerializeField] private Rigidbody rb;
    // [SerializeField] private float speed = 5;
    // [SerializeField] private float turnSpeed = 360;
    // private Vector3 input;



    // void Update()
    // {
    //     GatherInput();
    //     Look();
    // }


    // void FixedUpdate()
    // {
    //     Move();
    // }

    // void GatherInput()
    // {
    //     float xAxis = Input.GetAxisRaw("Horizontal");
    //     float zAxis = Input.GetAxisRaw("Vertical");
    //     // (new Vector3(xAxis, 0, zAxis)
    //     input = new Vector3(xAxis, 0, zAxis);//new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    // }

    // // Check rotation
    // void Look()
    // {


    //     if (input != Vector3.zero)
    //     {
    //         var relative = (transform.position + input) - transform.position;
    //         var rotation = Quaternion.LookRotation(relative, Vector3.up);
    //        // Debug.Log(Input.GetAxis("Horizontal"));

    //         transform.rotation = rotation;//Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    //     }
    // }

    // // leMovement leTroll
    // void Move()
    // {

       
    //     if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") < 0 & Input.GetAxis("Horizontal") == 0)
    //     {
    //         rb.MovePosition(transform.position + (transform.forward * input.normalized.magnitude) * speed *1.35f* Time.deltaTime);
    //     }
    //     else
    //     {
    //         rb.MovePosition(transform.position + (transform.forward * input.normalized.magnitude) * speed * Time.deltaTime);
    //     }
    // }
}
