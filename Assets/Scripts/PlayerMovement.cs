using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{

    private float _speed;
    public float slowSpeed = 250.0f;
    public float fastSpeed = 500.0f;
    private float jumpForce = 12.0f;

    private Rigidbody2D _body;
    private BoxCollider2D _box;
    private Animator _anim;
    private bool isGrounded;
    private float _deltaX;
    private bool _jump = false;
    private Collider2D _hit;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();   
        _box = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _speed = slowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {
            _speed += slowSpeed * Time.deltaTime;
            if(_speed > fastSpeed) {
                _speed = fastSpeed;
            }
        } else {
            _speed -= fastSpeed * Time.deltaTime;
            if(_speed < slowSpeed) {
                _speed = slowSpeed;
            }
        }

        isGrounded = checkGrounded();
        _deltaX = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
       
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            _jump = true;
            // _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // _body.AddForce(Vector2.right * _body.velocity, ForceMode2D.Impulse);
        } 

        MovePlatform platform = null;
        if (_hit != null) {
            platform = _hit.GetComponent<MovePlatform>();
        }
        if (platform != null) {
            transform.parent = platform.transform;
        } else {
            transform.parent = null;
        }
        // update animator state machine
        _anim.SetFloat("speed", Mathf.Abs(_deltaX));
        if(!Mathf.Approximately(_deltaX, 0)) {
            transform.localScale = new Vector3(Mathf.Sign(_deltaX), 1, 1);
        }

    }

    void FixedUpdate() {
        Vector2 movement = new Vector2(_deltaX, _body.velocity.y);
        _body.velocity = movement;

        if(_jump) {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _jump = false;
        }

    }

    bool checkGrounded() {
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);

        _hit = Physics2D.OverlapArea(corner1, corner2);
        return _hit != null;
    }
}
