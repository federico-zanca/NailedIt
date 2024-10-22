using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    private Vector2 _vel;
    private int _dir; // 1 if right, -1 if left

    // ground variables
    [SerializeField] private Transform frontPosition;
    [SerializeField] private float downRayLength;
    [SerializeField] private float frontRayLength;

    // Renderer variables
    private Vector3 _scale; // for flipping the image



    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        _dir = 1;
        _vel = Vector2.right * speed;
        _rb.velocity = _vel;
        _scale = gameObject.transform.localScale;

    }



    // Update is called once per frame
    void Update()
    {
        OnPlatform();
        IsWall();
    }

    /*

    */
    void OnPlatform(){
        if(!Physics2D.Raycast(frontPosition.position, Vector2.down, downRayLength)){
            ChangeDirection();
        }  
    }

    void IsWall(){
        if(Physics2D.Raycast(frontPosition.position, Vector2.right * _dir, frontRayLength)){
            ChangeDirection();
        }
    }

    void ChangeDirection(){
        _dir *= -1;
        _vel = _dir * speed * Vector2.right;
        _scale.x = _dir;
        gameObject.transform.localScale = _scale;

        _rb.velocity = _vel;
    }
}
