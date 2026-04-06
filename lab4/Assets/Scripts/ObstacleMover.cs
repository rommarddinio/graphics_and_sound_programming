using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float startDistance = 10f;
    public float stopAfterDistance = 5f;
    public string stopTag = "Obstacle";

    private Rigidbody rb;
    private bool isMoving = false;

    private Vector3 localStartPos;
    private Transform parentPlatform;
    private Transform lastPlatform;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;

        if (transform.parent != null)
        {
            parentPlatform = transform.parent;
            localStartPos = transform.localPosition;
        }
    }

    void Start()
    {
        if (PlayerManager.Instance.HasPlayer())
            player = PlayerManager.Instance.CurrentPlayer.transform;
        else
            Debug.LogWarning("Игрок ещё не спавнен!");
    }

    void Update()
    {
        player = PlayerManager.Instance.CurrentPlayer.transform;
    }

    void FixedUpdate()
    {

        if (parentPlatform != null && parentPlatform != lastPlatform)
        {
            isMoving = false;
            speed = 5f; 
            rb.isKinematic = false;
            lastPlatform = parentPlatform;

        }

        if (!isMoving && player != null)
        {
            float distance = Mathf.Abs(player.position.z - transform.position.z);
            if (distance <= startDistance)
                isMoving = true;
        }

        if (isMoving)
        {
            rb.MovePosition(rb.position + Vector3.back * speed * Time.fixedDeltaTime);

            if (player.position.z > transform.position.z + stopAfterDistance)
            {
                StopMovement();
            }
        }
        else
        {
            if (parentPlatform != null)
            {
                rb.MovePosition(parentPlatform.position + localStartPos);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(stopTag) || collision.gameObject.CompareTag("Player"))
        {
            StopMovement();
        }
    }

    private void StopMovement()
    {
        speed = 0f;
        isMoving = false;
        rb.isKinematic = true;
    }
}