using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float speed = 2f;
    public float moveRange = 1.5f;

    private Vector3 startPos;
    private int direction = 1;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;

        startPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        Vector3 localPos = transform.localPosition;
        localPos.x += speed * direction * Time.fixedDeltaTime;

        if (Mathf.Abs(localPos.x - startPos.x) >= moveRange)
        {
            localPos.x = startPos.x + Mathf.Clamp(localPos.x - startPos.x, -moveRange, moveRange);
            direction *= -1;
        }

        transform.localPosition = localPos;
    }
}