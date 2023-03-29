using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMouse : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public Vector3 directedVector = new Vector3();

    public void OnMouseDown()
    {
        //mZCoord = Camera.main.WorldToScreenPoint(PlayerMovement.playerInstance.transform.position).z;
        // store offset = player worldposition- mouse worldposition
        mOffset = PlayerMovement.playerInstance.transform.position - GetMouseWorldPos();
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        
        //z coordinate of gameobject on screen
        mousePoint.z = mZCoord;

        return mousePoint;
    }

    public void OnMouseDrag()
    {
        directedVector = GetMouseWorldPos() + mOffset;
    }
}
