using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{

    private float _rotateSpeed = 10.0f;
    [SerializeField] private GameObject platform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        platform.transform.Rotate(new Vector3(0,0,_rotateSpeed * Time.deltaTime));
    }
}
