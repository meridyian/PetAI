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
    public bool aiArrived;

    
    public float maxTime = .05f;
    public float minDistance = 1.0f;
    private float timer = 0.0f;

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
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(playerTransform.forward),
                Time.deltaTime * 5);
            aiArrived = true;
            Debug.Log(agent.velocity.magnitude / agent.speed);
            if (agent.velocity.magnitude / agent.speed < 0.1f)
            {
                RandomMovement();
            }
            
        }
        

        if (sqDistance > minDistance * minDistance)
        {
            agent.destination = playerTransform.position;
        }

        foxAnimator.SetFloat("AISpeed", agent.velocity.magnitude / agent.speed);
    }

    public void RandomMovement()
    {
        //yield return new WaitForSeconds(1f);
        foxAnimator.SetTrigger("Jump");
    
        foxAnimator.SetBool("Sit", true);
        
    }
}