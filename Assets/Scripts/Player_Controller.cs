using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float movementSpeed;
    float gravity = 8f;
    float rotSpeed = 80f;
    float rot = 0f;

    private Vector3 direction;
    private CharacterController controller;
    private Animator anim;

    void Start()
    {
        movementSpeed = 5.0f;
        direction = Vector3.zero;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {

    }

    void Update()
    {

        if (controller.isGrounded)
        {

            if (Input.GetKey(KeyCode.W))
            {
                direction = new Vector3(0, 0, 1);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction = new Vector3(0, 0, -1);
                direction *= movementSpeed;
                direction = transform.TransformDirection(direction);
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                direction = new Vector3(0, 0, 0);
            }
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
    }
}
