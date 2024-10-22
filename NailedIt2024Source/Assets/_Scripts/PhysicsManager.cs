using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public static PhysicsManager instance; 

    [SerializeField] private float gravity;

    void Awake(){
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject); // Avoid duplicates
        }
    }

    public float GetGravity(){
        return this.gravity;
    }


}
