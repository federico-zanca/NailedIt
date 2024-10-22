using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float mass;
    [SerializeField] private float accel;
    [SerializeField] private float decel;
    [SerializeField] private float maxSpeed;
    private Rigidbody2D _rb;
    private float _walkDirection;
    private Vector2 _vel;
    private bool _pastDir; // Indicates if the player was going right or not. True if he was moving right


    // Collision variables
    [SerializeField] private Transform feetPosition;
    private float _feetRayLen = 0.1f;
    private bool _onGround;

    // Rendering variables
    private Renderer _renderer;
    private float _halfSize;

    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
    }
    
    void Start()
    {
        _walkDirection = 0.0f;
        _halfSize = _renderer.bounds.size.y / 2;
        _vel = Vector2.zero;
        _onGround = false;
    }

    void Update()
    {

        _walkDirection = Input.GetAxisRaw("Horizontal");

        // Checking collisions
        _vel.y -= PhysicsManager.instance.GetGravity() * GameManager.instance.GetElapsed() / mass;
        RaycastHit2D groundHit = GroundCollision();
        if(groundHit){
            _vel.y = 0;
            _onGround = true;
            Vector3 pos = transform.position;
            GameObject ground = groundHit.collider.gameObject;
            pos.y = ground.transform.position.y + ground.GetComponent<Renderer>().bounds.size.y / 2 + _halfSize;
            transform.position = pos;
        }

        if(_walkDirection > 0 && _vel.x < maxSpeed){
            if(_vel.x < 0){
                _vel.x = 0;
            }
            _vel.x = Mathf.Min(_vel.x + accel * GameManager.instance.GetElapsed() / mass, maxSpeed / mass);
        }
        else if(_walkDirection < 0 && _vel.x > -maxSpeed){
            if(_vel.x > 0){
                _vel.x = 0;
            }
            _vel.x = Mathf.Max(_vel.x - accel * GameManager.instance.GetElapsed() / mass, -maxSpeed / mass);
        }
        else if(_walkDirection == 0){
            if(_vel.x < 0){
                _vel.x = Mathf.Min(_vel.x + decel * GameManager.instance.GetElapsed() / mass, 0.0f);
            }
            else if(_vel.x > 0){
                _vel.x = Mathf.Max(_vel.x - decel * GameManager.instance.GetElapsed() / mass, 0.0f);
            }
        }
    }

    void FixedUpdate(){
        _rb.velocity = _vel;
    }

    RaycastHit2D GroundCollision(){
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, _feetRayLen);
        return hit;
    }
}
