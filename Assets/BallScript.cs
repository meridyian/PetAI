using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private Transform parentBone;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private Vector3 currentVel;

    
    void Start()
    {
        transform.parent = parentBone;
        rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReleaseBall()
    {
        transform.parent = null;
        transform.rotation = parentBone.transform.rotation;
        rigidbody.AddForce(transform.forward * 20000);
    }
}
