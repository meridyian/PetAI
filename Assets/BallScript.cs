using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TreeEditor;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Transform parentBone;
    public Rigidbody rigid;
    [SerializeField] private Vector3 lastPos;
    [SerializeField] private Vector3 curVel;
    public float throwSpeed;
    public GameObject player;
    public bool ballisGrounded;
    public static BallScript ballInstance;
    public float ballDistance;
    public bool outofBounds;
    

    public void Awake()
    {
        ballInstance = this;
    }

    
    public void Update()
    {
        ballDistance = (transform.position - player.transform.position).magnitude;
        if (ballDistance < 1f && transform.parent == null && AIFollow.AInstance.hasBall)
        {
            transform.position = parentBone.transform.position;
            transform.parent = parentBone.transform;
        }
    }
    
    
    public void ReleaseBall()
    {
        transform.parent = null;
        rigid.useGravity = true;
        rigid.isKinematic = false;
        transform.rotation = parentBone.transform.rotation;
        
        //
        if (rigid.transform.position.y > 0.3f)
        {
            rigid.AddForce((player.transform.forward + player.transform.up) * throwSpeed);
        }
        else
        {
            rigid.isKinematic = false;
        }
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            ballisGrounded = true;
        }
        if(other.gameObject.CompareTag("PetAI"))
        {
            transform.parent = other.transform;
        }
 
    }

    

}
