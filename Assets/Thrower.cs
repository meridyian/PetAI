using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject ballpref;
    
    public void Throw()
    {
        BallScript ballScript = ballpref.GetComponent<BallScript>();
        ballScript.ReleaseBall();
    }
}
