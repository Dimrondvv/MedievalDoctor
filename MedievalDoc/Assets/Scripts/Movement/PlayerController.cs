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
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    // Check rotation
    void Look()
    {


        if (input != Vector3.zero)
        {
            var relative = (transform.position + input) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rotation;//Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
    }


    void Move()
    {
        rb.MovePosition(transform.position + (transform.forward*input.normalized.magnitude) * speed * Time.deltaTime);
    }













}
