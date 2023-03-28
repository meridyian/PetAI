using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Movement parameters 
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight = 1.5f;
    public float currentSpeed = 0f;
    public bool throwBall;
    public bool playerHasBall =true;
    
    
    //parentBone u burda alman gerekebilir
    public Transform parentBone;


    // rotation control
    [SerializeField] private float speedSmoothVelocity = 0f;
    [SerializeField] private float speedSmoothTime = 0.1f;
    [SerializeField] private float rotationSpeed = 0.01f;


    //jumping parameters and ground check
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    public Transform groundCheck;
    public float gravity = -9.81f;
    private float targetSpeed = 0f;
    public Vector3 gravityVector;

    // camera and controllers
    public Transform mainCameraTransform = null;
    private CharacterController charController = null;
    private Animator anim = null;
    public static PlayerMovement playerInstance;
    
    public void Awake()
    {
        if(playerInstance != null) return;
        playerInstance = this;
    }

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        
        // inputMagnitude is in range [0,1]
        Vector3 movementDirection = new Vector3(horizontalInput,0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        
        movementDirection = Quaternion.AngleAxis(mainCameraTransform.rotation.eulerAngles.y, Vector3.up) *
                            movementDirection;
        movementDirection.Normalize();

        
        // if character is not grounded he should fall
        if (!isGrounded && gravityVector.y < 0)
        {
            gravityVector.y -= 2.5f;
        }
        
        //if character is moving, face towards movement direction
        if (movementDirection != Vector3.zero)
        {
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection),
                rotationSpeed);
        }

        // the speed that you want to reach gradually
        targetSpeed = walkSpeed * inputMagnitude;

        // RUN
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // targetSpeed should be updated if you want to run
            targetSpeed = runSpeed * inputMagnitude;
            anim.SetFloat("Speed", 1f * inputMagnitude, speedSmoothTime, Time.deltaTime);
        }

        // Walking should be the root target motion
        anim.SetFloat("Speed", 0.5f * inputMagnitude, speedSmoothTime, Time.deltaTime);
        

        // Jump animation
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        

        if (Input.GetMouseButtonUp(0) && movementDirection == Vector3.zero && playerHasBall)
        {

            StartCoroutine(ThrowBall());
            playerHasBall = false;

        }
        
        // to adjust speed changes

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        charController.Move(movementDirection * currentSpeed * Time.deltaTime);
        gravityVector.y += gravity * Time.deltaTime * 1.2f;
        charController.Move(gravityVector * Time.deltaTime);

    }
    

    public IEnumerator ThrowBall()
    {
        anim.SetBool("isBallThrown", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isBallThrown", false);
        throwBall = true;
    }
    
    /*
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    */

}
       
