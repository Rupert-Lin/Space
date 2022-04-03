using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // The target we are following
    public Transform target;
    public Vector3 offset;
    void start() 
    {

    }
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        }
    }

}
