using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Vector3 _velocity = Vector3.zero;
    public float smoothTime = 0.2f;
    public Transform target;
    // LateUpdate is called once per frame
    void LateUpdate()
    {
        // Vector3 camPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        // transform.position = Vector3.SmoothDamp(transform.position, camPosition, ref _velocity, smoothTime);

    }


}
