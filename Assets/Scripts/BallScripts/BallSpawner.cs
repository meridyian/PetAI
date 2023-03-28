using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
 
    public void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        AIFollow.AInstance.collidedBall = ball;
        AIFollow.AInstance.ballScript = ball.GetComponent<BallScript>();
        ball.transform.parent = transform;
        PlayerMovement.playerInstance.playerHasBall = true;
    }
    

    
}
