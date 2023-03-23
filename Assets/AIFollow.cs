using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCover.Framework.Model;
using TreeEditor;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private NavMeshAgent agent;
    public Animator foxAnimator;
    
    public float maxTime = .05f;
    public float minDistance = 1.0f;
    public Transform foxMouth;
    public bool collided;
    public bool hasBall;
    
    public static AIFollow AInstance;
    public GameObject collidedBall;

    public BallScript ballScript;

    public void Awake()
    {
        if (AInstance != null) return;
        AInstance = this;
    }
    
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        foxAnimator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float sqDistance = (playerTransform.position - transform.position).sqrMagnitude;
        if (sqDistance * 1.1f < minDistance * minDistance)
        {
            foxAnimator.SetFloat("AISpeed", 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(playerTransform.forward),
                Time.deltaTime * 5);

            if (agent.velocity.magnitude == 0)
            {
                RandomMovement();
                if (hasBall)
                {
                    ballScript.BallToPlayer();
                    hasBall = false;
                }
            }
            
            
        }
        // follow player
        
        if (sqDistance > minDistance * minDistance)
        {
            agent.destination = playerTransform.position;
            foxAnimator.SetBool("Sit", false);
            foxAnimator.SetBool("Jump", false);
            foxAnimator.SetBool("Roll", false);
            if (collided && foxMouth != collidedBall.transform.parent)
            {
                //collidedBall.GetComponent<SphereCollider>().isTrigger = false;
                collidedBall.GetComponent<Rigidbody>().isKinematic = false;
                collided = false;
            }
            
        }
        
        foxAnimator.SetFloat("AISpeed", agent.velocity.magnitude);

        if (ballScript.ballisGrounded && !ballScript.outofBounds &&  PlayerMovement.playerInstance.throwBall)
        {
            foxAnimator.SetBool("Sit", false);
            foxAnimator.SetBool("Jump", false);
            foxAnimator.SetBool("Roll", false);
            agent.destination = ballScript.transform.position;
            foxAnimator.SetFloat("AISpeed", agent.velocity.magnitude);
            
        }
    }

    
    
    public void RandomMovement()
    {
        foxAnimator.SetBool("Jump", true);
        foxAnimator.SetBool("Roll", true);
        foxAnimator.SetBool("Sit", true);
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {

            collidedBall = other.gameObject;
            collidedBall.GetComponent<Rigidbody>().isKinematic = true;
            collidedBall.GetComponent<SphereCollider>().isTrigger = true;
            collided = true;
            hasBall = true;
            ballScript.ballisGrounded = false;

        }
    }
    
    
}