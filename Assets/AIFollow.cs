using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator foxAnimator;
    
    public float maxTime = .05f;
    public float minDistance = 1.0f;
    private float timer = 0.0f;


    // Start is called before the first frame update
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
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(playerTransform.forward),
                Time.deltaTime * 5);
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                foxAnimator.SetTrigger("Sit");
                timer = maxTime;
            }
            
        }

        //
        //
        //{
        if (sqDistance > minDistance * minDistance)
        {
            agent.destination = playerTransform.position;
        }


        //    
        //}       

        foxAnimator.SetFloat("AISpeed", agent.velocity.magnitude / agent.speed);
    }
}