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
    
    [SerializeField] private float speedSmoothVelocity = 0f;
    [SerializeField] private float speedSmoothTime = 0.1f;
    
    public float maxTime = 1.0f;
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
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - transform.position).sqrMagnitude;
            if (sqDistance > minDistance * minDistance)
            {
                agent.destination = playerTransform.position;
            }

            timer = maxTime;
        }
        
        foxAnimator.SetFloat("AISpeed",agent.velocity.magnitude, speedSmoothTime,speedSmoothVelocity);
    }
}
