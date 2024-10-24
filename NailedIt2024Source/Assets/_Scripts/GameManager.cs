using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _elapsed;
    private float _fixedElapsed;

    void Awake(){
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject); // Avoid duplicates
        }
    }

    void Update(){
        _elapsed = Time.deltaTime;
    }

    void FixedUpdate(){
        _fixedElapsed = Time.fixedDeltaTime;
    }

    public float GetElapsed(){
        return _elapsed;
    }

    public float GetFixedElapsed(){
        return _fixedElapsed;
    }
}
