using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    //public GameObject ballpref;
    public Transform ballParent;

    
    public void Throw()
    {
        AIFollow.AInstance.ballScript.GetComponent<SphereCollider>().isTrigger = false;
        ballParent.GetComponentInChildren<BallScript>().ReleaseBall();
        
    }
}
