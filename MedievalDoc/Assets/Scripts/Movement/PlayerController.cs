using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 input;



    void Update()
    {
        GatherInput();
        Look();
    }


    void FixedUpdate()
    {
        Move();
    }

    void GatherInput()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float zAxis = Input.GetAxisRaw("Vertical");
        // (new Vector3(xAxis, 0, zAxis)
        input = new Vector3(xAxis, 0, zAxis);//new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    // Check rotation
    void Look()
    {


        if (input != Vector3.zero)
        {
            var relative = (transform.position + input) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);
           // Debug.Log(Input.GetAxis("Horizontal"));

            transform.rotation = rotation;//Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
    }

    // leMovement leTroll
    void Move()
    {

       
        if (Input.GetAxis("Vertical") > 0 & Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") < 0 & Input.GetAxis("Horizontal") == 0)
        {
            rb.MovePosition(transform.position + (transform.forward * input.normalized.magnitude) * speed *1.35f* Time.deltaTime);
        }
        else
        {
            rb.MovePosition(transform.position + (transform.forward * input.normalized.magnitude) * speed * Time.deltaTime);
        }
    }













}
