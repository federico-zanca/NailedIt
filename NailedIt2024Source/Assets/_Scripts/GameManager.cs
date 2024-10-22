using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float _elapsed;

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

    public float GetElapsed(){
        return _elapsed;
    }
}
