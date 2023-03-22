using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
 
    public void SpawnBall()
    {
        GameObject  ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ball.transform.parent = transform;
    }

    
}
