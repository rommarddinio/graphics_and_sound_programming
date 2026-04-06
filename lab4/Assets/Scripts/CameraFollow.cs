using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       
    public Vector3 offset;         
    public float smoothSpeed = 5f; 

    private Quaternion initialRotation; 

    void Start()
    {
        target  = PlayerManager.Instance.GetPlayerTransform();
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        Transform target = PlayerManager.Instance.GetPlayerTransform();
        if (target == null) return;

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