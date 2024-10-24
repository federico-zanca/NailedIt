using UnityEngine;

public class NailingThing : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private GameObject corpsePlatformPrefab;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private int maxCorpses = 3;

    private Camera mainCamera;
    private int corpseCount = 0;

    private void Start()
    {
        // Find main camera automatically
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No camera with MainCamera tag found!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Shoot(mouseWorldPos);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    private void Shoot(Vector3 mousePosition)
    {
        float distanceToPlayer = Vector2.Distance(mousePosition, transform.position);
        
        if (distanceToPlayer <= spawnRadius && corpseCount < maxCorpses)
        {
            Nail(transform.position, transform.rotation);
        }
    }

    private void Nail(Vector3 position, Quaternion rotation)
    {
        GameObject corpse = Instantiate(corpsePlatformPrefab, position, rotation);
        corpseCount++;
    }

    // Visualize scope radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}