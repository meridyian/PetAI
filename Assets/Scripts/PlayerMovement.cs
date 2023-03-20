using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Movement parameters 
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight = 1.5f;
    public float currentSpeed = 0f;

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
    public Transform targetTransform;

    public static PlayerMovement playerInstance;

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
        Vector3 movementDirection = new Vector3(horizontalInput,0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        }

        movementDirection = Quaternion.AngleAxis(mainCameraTransform.rotation.eulerAngles.y, Vector3.up) *
                            movementDirection;
        movementDirection.Normalize();

        
        // if character is not grounded he should fall
        if (isGrounded && gravityVector.y < 0)
        {
            gravityVector.y -= 2.5f;
        }


        if (movementDirection != Vector3.zero)
        {
            //spherically interpolates between a and b
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection),
                rotationSpeed);
        }

        // the speed that you wat to reach gradually
        targetSpeed = walkSpeed * inputMagnitude;

        // Run movement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed = runSpeed * inputMagnitude;
            anim.SetFloat("Speed", 1f * inputMagnitude, speedSmoothTime, Time.deltaTime);
        }

        // walk animation
        anim.SetFloat("Speed", 0.5f * inputMagnitude, speedSmoothTime, Time.deltaTime);

        // Pet the animal
        if (Input.GetKey(KeyCode.E) && movementDirection == Vector3.zero)
        {
            StartCoroutine(PetAnimal());
        }

        // add throw and jump animations
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.T) && movementDirection == Vector3.zero)
        {
            StartCoroutine(ThrowBall());
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
    }


    public IEnumerator PetAnimal()
    {
        // put the animal as target
        // after adding ball throw animation, player can pet the animal
        // after animal turns around the player pet the animal
        yield return new WaitForSeconds(1f);
        anim.SetBool("isPetting", true);
        yield return new WaitForSeconds(5f);
        anim.SetBool("isPetting", false);
    }

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

}
       
