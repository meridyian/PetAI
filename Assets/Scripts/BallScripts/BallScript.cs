using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using TreeEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BallScript : MonoBehaviour
{
    public Transform parentBone;
    public Rigidbody rigid;
    public GameObject player;

    [SerializeField] private Vector3 lastPos;
    [SerializeField] private Vector3 curVel;
    public float throwSpeed;
    
    // check if the ball is grounded
    // distance between ball and player
    //check the boundaries of the ground 
    public bool ballisGrounded;
    public float ballDistance;
    public bool outofBounds;
    public BallSpawner ballSpawner;
    public bool onWall;


    
    public static BallScript ballInstance;

    public void Awake()
    {
        if (ballInstance != null) return;
        ballInstance = this;
    }

    public void Start()
    {
        rigid = GetComponent<Rigidbody>();
        player = PlayerMovement.playerInstance.gameObject;
        parentBone = PlayerMovement.playerInstance.parentBone;
        ballSpawner = GetComponentInParent<BallSpawner>();
        
    }

    
    public void Update()
    {
        ballDistance = (transform.position - player.transform.position).magnitude;
        
        // AIFollow.AInstance.hasBall should be there so that it wont stick to parentbone
        if (ballDistance < 2f && transform.parent == null && AIFollow.AInstance.hasBall)
        {
            transform.position = parentBone.transform.position;
            transform.parent = parentBone.transform;
        }

        if (transform.position.x is > 45f or < -45f || transform.position.z is > 45f or < -45f || onWall)
        {
            outofBounds = true;
            ballSpawner.SpawnBall();
            Destroy(gameObject);
        }
    }
    
    
    public void ReleaseBall()
    {
        transform.parent = null;
        rigid.useGravity = true;
        rigid.isKinematic = false;
        transform.rotation = parentBone.transform.rotation;


        if (PlayerMovement.playerInstance.throwBall)
        {
            
            //forceZ = (parentBone.forward * PlayerStats.playerStatsInstance.forceToAdd * throwSpeed).magnitude;
            //rigid.AddForce((player.transform.forward + player.transform.up ) * throwSpeed + player.transform.right * PlayerStats.playerStatsInstance.forceToAdd * 3f);
            rigid.AddForce((player.transform.forward * throwSpeed/18f * PlayerStats.playerStatsInstance.forceToAdd + player.transform.up * throwSpeed + player.transform.right * PlayerMovement.playerInstance.slideValue ));
            PlayerStats.playerStatsInstance.ForceSlider.value = 0f;
            PlayerStats.playerStatsInstance.isIncreasing = false;
            PlayerMovement.playerInstance.startDirect = false;
            PlayerStats.playerStatsInstance.forceToAdd = 0f;
            //rigid.AddForce(PlayerMovement.playerInstance.GetComponent<DragMouse>().directedVector);
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
            transform.parent = other.transform.GetComponent<AIFollow>().foxMouth;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            onWall = true;
        }
 
    }

    public void BallToPlayer()
    {

        rigid.isKinematic = true;
        transform.parent = parentBone;
        transform.position = parentBone.position;
        PlayerMovement.playerInstance.playerHasBall = true;
        AIFollow.AInstance.petDestination = false;
        PlayerStats.playerStatsInstance.ForceSlider.value = 0f;

    }


}
