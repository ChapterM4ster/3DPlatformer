using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement")]
    public float movementSpeed = 6f;
    public CharacterController controller;

    [Header("gravity")]
    public float gravity = -9.81f;

    Vector3 velocity;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    bool isGrounded;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UserInputMovement();
    }

    void UserInputMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move =transform.right * x + transform.forward * z;

        controller.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
