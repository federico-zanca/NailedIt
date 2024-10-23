using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float mass;
    private float _inversMass;
    [SerializeField] private float accel;
    [SerializeField] private float decel;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxFallSpeed;
    private Rigidbody2D _rb;
    private float _walkDirection;
    private Vector2 _vel;
    // Jump section -- To be modified by Fede Zanca Uomo Incredibilmente bello
    [SerializeField] private float jumpForce;
    private bool _isJumping;


    // Collision variables
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask groundLayer;
    private float _feetRayLen = 0.1f;
    private bool _onGround;

    // Rendering variables
    private Renderer _renderer;
    private float _halfSizey;

    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
    }
    
    void Start()
    {
        _walkDirection = 0.0f;
        _halfSizey = _renderer.bounds.size.y / 2;
        _vel = Vector2.zero;
        _onGround = false;
        _inversMass = 1 / mass;
    }

    void Update()
    {
        _walkDirection = Input.GetAxisRaw("Horizontal");
        // For the jump mechanism, do not set _isJumping = Input.GetButtonDown cause it won't work, source: trust me bro
        if(Input.GetButtonDown("Jump")){
            _isJumping = true;
        }
    }

    void FixedUpdate(){
        HandleVerticalMovement();
        HandleHorizontalMovement();
        _rb.velocity = _vel;
    }

    void HandleVerticalMovement(){
        _vel.y -= PhysicsManager.instance.GetGravity() * GameManager.instance.GetFixedElpased() * _inversMass;
        if(_vel.y <= 0){
            RaycastHit2D groundHit = GroundCollision();
            if(groundHit){
                _vel.y = 0;
                _onGround = true;
                AlignWithGround(groundHit);
            }
            else{
                _onGround = false;
            }
        }

        HandleJump();

        _vel.y = Mathf.Max(_vel.y, -maxFallSpeed);
    }

    void HandleJump(){
        if(_onGround && _isJumping){
            _vel.y = jumpForce * _inversMass;
            _onGround = false;
        }
        _isJumping = false;
    }

    void AlignWithGround(RaycastHit2D groundHit){
        Vector3 pos = transform.position;
        GameObject ground = groundHit.collider.gameObject;
        pos.y = ground.transform.position.y + ground.GetComponent<Renderer>().bounds.size.y / 2 + _halfSizey;
        transform.position = pos;
    }

    void HandleHorizontalMovement(){
        if(_walkDirection > 0 && _vel.x < maxSpeed){
            if(_vel.x < 0){
                _vel.x = 0;
            }
            _vel.x = Mathf.Min(_vel.x + accel * GameManager.instance.GetElapsed() * _inversMass, maxSpeed * _inversMass);
        }
        else if(_walkDirection < 0 && _vel.x > -maxSpeed){
            if(_vel.x > 0){
                _vel.x = 0;
            }
            _vel.x = Mathf.Max(_vel.x - accel * GameManager.instance.GetElapsed() * _inversMass, -maxSpeed * _inversMass);
        }
        else if(_walkDirection == 0){
            if(_vel.x < 0){
                _vel.x = Mathf.Min(_vel.x + decel * GameManager.instance.GetElapsed() * _inversMass, 0.0f);
            }
            else if(_vel.x > 0){
                _vel.x = Mathf.Max(_vel.x - decel * GameManager.instance.GetElapsed() * _inversMass, 0.0f);
            }
        }
    }

    RaycastHit2D GroundCollision(){
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, _feetRayLen, groundLayer);
        return hit;
    }
}
