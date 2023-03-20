using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject ballPref;


 
    
    public void Throw()
    {
        BallScript ballScript = ballPref.GetComponent<BallScript>();
        ballScript.ReleaseBall();
    }
}
