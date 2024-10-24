using UnityEngine;

public class ScopeCursor : MonoBehaviour
{
    [Header("Scope Settings")]
    [SerializeField] private GameObject scopePrefab; // Assign your scope sprite in inspector
    [SerializeField] private float scopeSize = 1f;
    [SerializeField] private Color scopeColor = Color.red;
    [SerializeField] private bool hideCursor = true; // Set to true to hide default cursor

    private Camera mainCamera;
    private GameObject currentScope;
    private SpriteRenderer scopeRenderer;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera with MainCamera tag found!");
            return;
        }
        // Hide the default cursor if specified
        if (hideCursor)
        {
            Cursor.visible = false;
        }

        CreateScope();
    }

    private void CreateScope()
    {
        // Create scope object if prefab is assigned
        if (scopePrefab != null)
        {
            currentScope = Instantiate(scopePrefab);
            scopeRenderer = currentScope.GetComponent<SpriteRenderer>();
            if (scopeRenderer != null)
            {
                scopeRenderer.color = scopeColor;
                currentScope.transform.localScale = Vector3.one * scopeSize;
            }
        }
        else
        {
            Debug.LogError("No scope prefab assigned!");
        }
    }

    private void Update()
    {
        if (currentScope != null)
        {
            // Update scope position to follow mouse
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 
            currentScope.transform.position = mousePosition;
        }
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }
}