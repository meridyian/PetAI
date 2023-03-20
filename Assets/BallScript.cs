using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private Transform parentBone;
    public Rigidbody rigid;
    [SerializeField] private Vector3 lastPos;
    [SerializeField] private Vector3 curVel;
    public float throwSpeed;
    public GameObject player;
    public bool ballisGrounded;
    public static BallScript ballInstance;
    

    public void Awake()
    {
        ballInstance = this;
    }
    public void Start()
    {
        transform.parent = parentBone;
        
    }
    
    
    public void ReleaseBall()
    {
        transform.parent = null;
        rigid.useGravity = true;
        rigid.isKinematic = false;
        transform.rotation = parentBone.transform.rotation;
       
        if (rigid.transform.position.y > 0f)
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
        
    }
    
    
}
