using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CorpseController : MonoBehaviour
{

    private void Start()
    {
        SetupPlatform();
    }

    private void SetupPlatform()
    {
        // Ensure we have a box collider
        BoxCollider2D platformCollider = GetComponent<BoxCollider2D>();
        
    }
}
