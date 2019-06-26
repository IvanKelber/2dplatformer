using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    private Vector3 endPos = Vector3.zero;

    private int _direction = 1;
    private Vector3 _startPos;
    private float _trackPercent = 0;
    private float MAX_DISTANCE;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = this.transform.position;
        endPos = getEndPos();
        MAX_DISTANCE = Vector3.Distance(_startPos, endPos);
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * _direction * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, endPos, Mathf.Sin(step));
        flip_direction();

        // Speed up controls
        if(Input.GetKeyDown(KeyCode.J)) {
            speed -= .1f;
        }
        if(Input.GetKeyDown(KeyCode.K)) {
            speed += .1f;
        }
    }

    private void flip_direction() {
        float dist = Vector3.Distance(transform.position, endPos);
        if(dist < .1f || dist > MAX_DISTANCE) {
            print("Flipping direction");
            _direction *= -1;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, getEndPos());
    }

    private Vector3 getEndPos() {
        Vector3 pos = this.transform.Find("EndPoint").gameObject.transform.position;
        return pos;
    }
}
