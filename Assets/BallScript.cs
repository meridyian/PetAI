using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private Transform parentBone;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Vector3 lastPos;
    [SerializeField] private Vector3 curVel;
    public float throwSpeed;
    public GameObject player;
    public bool ballisGrounded;

    public void Start()
    {
        transform.parent = parentBone;
        rigid.useGravity = false;
        rigid.isKinematic = true;
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
            rigid.isKinematic = true;
        }
        
       
    }
    
    
}
