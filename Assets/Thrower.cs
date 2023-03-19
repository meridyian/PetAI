using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    public void ThrowBall()
    {
        BallScript ballScript = ball.GetComponent<BallScript>();
        ballScript.ReleaseBall();
    }
}
