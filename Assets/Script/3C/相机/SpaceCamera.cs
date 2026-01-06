using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCamera : MonoBehaviour
{
    [SerializeField] Transform target;      
    [SerializeField] float smoothTime = 0.3f;  
    [SerializeField] Vector3 offset;       

    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z) + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}
