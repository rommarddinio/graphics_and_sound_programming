using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       
    public Vector3 offset;         
    public float smoothSpeed = 5f; 

    private Quaternion initialRotation; 

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.rotation = initialRotation;
    }

    public void SetSpeed(float speed)
    {
        smoothSpeed += speed;
    }
}