using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform toBeFollowed;
    [SerializeField] private Vector3 distance;
    [SerializeField] private float smoothSpeed = 0.2f; 

    private void FixedUpdate(){
        Vector3 targetPosition = toBeFollowed.position + distance;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed); // linear interpolation
        transform.position = new Vector3(smoothedPosition.x, transform.position.y, transform.position.z);
    }
}
