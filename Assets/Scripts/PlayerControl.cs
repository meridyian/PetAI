using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;


    private Vector3 moveDirection;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }

        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        
        else if(moveDirection == Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Idle();
        }
        
        else if (moveDirection == Vector3.zero && Input.GetKey("p"))
        {
            Pet();
        }
        
        else if (moveDirection == Vector3.zero && Input.GetMouseButtonDown(0))
        {
            ThrowBall();
        }

        moveDirection *= moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
    }
    private void Run()
    {
        moveSpeed = runSpeed;
    }
    private void Idle()
    {
        //idle 
    }
    private void Pet()
    {
        // pet animal animation
    }
    
    private void ThrowBall()
    {
        // throw the ball
    }
    
}
