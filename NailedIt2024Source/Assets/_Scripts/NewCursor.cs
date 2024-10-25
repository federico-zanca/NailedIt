/*
using UnityEngine;

public class ScopeCursor : MonoBehaviour
{
    [Header("Scope Settings")]
    [SerializeField] private bool hideCursor = true;

    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera with MainCamera tag found!");
            return;
        }

        Cursor.visible = hideCursor;

    }


    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
    
        transform.position = worldPosition;
        
    }

}
*/