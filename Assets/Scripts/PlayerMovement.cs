using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    // Movement parameters 
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 15f;
    [SerializeField] private float jumpHeight = 1.5f;
    private float currentSpeed = 0f;
    
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
    private Transform mainCameraTransform = null;
    private CharacterController charController = null;
    private Animator anim = null;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        // get input from player
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // camera should follow the player
        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        
        Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;
        
        // if character is not grounded he should fall
        if (isGrounded && gravityVector.y < 0)
        {
            gravityVector.y -= 2f;
        }


        if (desiredMoveDirection != Vector3.zero)
        {
            //spherically interpolates between a and b
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection),
                rotationSpeed);
        }

        // the speed that you use as threshold
        targetSpeed = walkSpeed * movementInput.magnitude;


        // if left shift is pressed player should run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed = runSpeed * movementInput.magnitude;

        }

        // walk animation
        anim.SetFloat("Speed", 0.5f * movementInput.magnitude, speedSmoothTime, Time.deltaTime);

        // run animation 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", 1f * movementInput.magnitude, speedSmoothTime, Time.deltaTime);
        }

        // pet the animal
        if (Input.GetKey(KeyCode.E) && desiredMoveDirection == Vector3.zero)
        {

            StartCoroutine(PetAnimal());
        }

        // add throw and jump animations
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVector.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // to adjust speed changes
        
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        charController.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);
        gravityVector.y += gravity * Time.deltaTime;
        charController.Move(gravityVector * Time.deltaTime);


            
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

   
    
}
