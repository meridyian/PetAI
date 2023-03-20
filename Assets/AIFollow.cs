using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCover.Framework.Model;
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
            }
            
        }
        
        // follow player
        
        if (sqDistance > minDistance * minDistance)
        {
            agent.destination = playerTransform.position;
        }
        
        foxAnimator.SetFloat("AISpeed", agent.velocity.magnitude);
        
        if (BallScript.ballInstance.ballisGrounded)
        {
            ballPosition = BallScript.ballInstance.gameObject.transform.position;
            agent.destination = BallScript.ballInstance.transform.position;
            StartCoroutine(RunToBall());

        }
    }

    public IEnumerator RunToBall()
    {
        float ballDistance = (BallScript.ballInstance.transform.position - transform.position).sqrMagnitude;
        Debug.Log(ballDistance);
        if (ballDistance < 0.3f)
        {
            foxAnimator.SetBool("TurnAround", true);
            //foxAnimator.SetFloat("AISpeed", 0.2f);
            yield return new WaitForSeconds(1f);
            ballPosition = Vector3.MoveTowards(ballPosition, foxMouth.position, Time.deltaTime);
            BallScript.ballInstance.ballisGrounded = false;
            foxAnimator.SetBool("TurnAround", false);
            agent.destination = playerTransform.position;
            foxAnimator.SetFloat("AISpeed", 1f);
        }
    }

    public void RandomMovement()
    {
        //yield return new WaitForSeconds(1f);
        foxAnimator.SetTrigger("Jump");
    
        foxAnimator.SetBool("Sit", true);
        
    }
}