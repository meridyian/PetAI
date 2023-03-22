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
    private float timer = 0.0f;
    public Transform foxMouth;
    public Vector3 ballPosition;
    public bool collided;
    public bool canPetAnimal;
    public bool hasBall;


    public static AIFollow AInstance;


    // Start is called before the first frame update
    void Start()
    {
        
        AInstance = this;
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
            if (agent.velocity.magnitude / agent.speed < 0.1f)
            {
                RandomMovement();
                if (hasBall)
                {
                    BallScript.ballInstance.transform.parent = null;
                    BallScript.ballInstance.rigid.isKinematic = true;
                    BallScript.ballInstance.transform.parent = BallScript.ballInstance.parentBone;
                    
                }
            }
            
        }
        // follow player
        
        if (sqDistance > minDistance * minDistance)
        {
            agent.destination = playerTransform.position;
            
        }
        
        foxAnimator.SetFloat("AISpeed", agent.velocity.magnitude);
        
        //BallScript.ballInstance.ballisGrounded
        if (BallScript.ballInstance.ballisGrounded && !BallScript.ballInstance.outofBounds &&  PlayerMovement.playerInstance.throwBall)
        {
            agent.destination = BallScript.ballInstance.transform.position;
            if (collided)
            {
                foxAnimator.SetFloat("AISpeed", 0f);
                //BallScript.ballInstance.ballisGrounded = false;
                hasBall = true;
            }
        }
    }
   

    public void RandomMovement()
    {
        foxAnimator.SetTrigger("Jump");
        foxAnimator.SetBool("Sit", true);
        

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            collided = true;
            BallScript.ballInstance.ballisGrounded = false;
            /*
            bunu çalıştırınca top havada geliyor 
            Rigidbody ballRigid = other.gameObject.GetComponent<BallScript>().rigid;
            ballRigid.isKinematic = true;
            //ballRigid.MovePosition(foxMouth.position);
            */
            //other.transform.parent = transform;
            //BallScript.ballInstance.rigid.isKinematic = true;

        }
    }
}