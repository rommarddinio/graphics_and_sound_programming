using UnityEngine;

[System.Serializable]
public enum Side
{
    Left,
    Mid,
    Right
}

public class Character : MonoBehaviour
{
    public Side side = Side.Mid;

    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;

    private bool isGameOver = false;
    private bool isFallingToGround = false;
    private bool isGameStarted = false;

    private float newXPos = 0f;
    public float xValue = 2f;

    public float forwardSpeed = 10f; 
    public float jumpPower = 10f;
    public float gravity = -20f;

    private float x;
    private float y;

    private CharacterController controller;
    private Animator animator;
    public CameraFollow cameraFollow;
    public UIManager manager;

    private float speedTimer = 0f;       
    public float speedIncreaseInterval = 10f; 
    public float speedStep = 5f;        

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        transform.position = new Vector3(0, 0, 0);

        forwardSpeed = 0f;
    }

    void Update()
    {
        if (!isGameStarted || isGameOver) return;

        swipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        swipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        swipeUp = Input.GetKeyDown(KeyCode.Space);

        if (swipeLeft)
        {
            if (side == Side.Mid)
            {
                newXPos = -xValue;
                side = Side.Left;
            }
            else if (side == Side.Right)
            {
                newXPos = 0;
                side = Side.Mid;
            }
        }

        if (swipeRight)
        {
            if (side == Side.Mid)
            {
                newXPos = xValue;
                side = Side.Right;
            }
            else if (side == Side.Left)
            {
                newXPos = 0;
                side = Side.Mid;
            }
        }

        x = (newXPos - transform.position.x) * 10f;

        Vector3 move = new Vector3(x, y, forwardSpeed) * Time.deltaTime;
        controller.Move(move);

        HandleJump();

        speedTimer += Time.deltaTime;
        if (speedTimer >= speedIncreaseInterval)
        {
            forwardSpeed += speedStep;
            speedTimer = 0f;
            cameraFollow.SetSpeed(speedStep);
            Debug.Log("Скорость была увеличена!");
        }
    }

    void HandleJump()
    {
        if (controller.isGrounded)
        {
            if (y < 0) y = -1f;

            if (swipeUp)
            {
                y = jumpPower;
            }

            animator.SetBool("isGrounded", controller.isGrounded);
        }
        else
        {
            y += gravity * Time.deltaTime;
            animator.SetBool("isGrounded", false);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Vector3 forward = transform.forward;
            float dot = Vector3.Dot(hit.normal, -forward); 
            if (dot > 0.5f)
            {
                GameOver(); 
            }
            else
            {
                HandleSideCollision(hit.normal);
            }

        }
    }

    void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        forwardSpeed = 0f;
        isFallingToGround = true;
        animator.SetTrigger("over");
        manager.ShowDeathPanel();
    }

    void FixedUpdate()
    {
        if (isFallingToGround && !controller.isGrounded)
        {
            y += gravity * 0.3f * Time.fixedDeltaTime; 
            Vector3 move = new Vector3(0, y, 0);
            controller.Move(move * Time.fixedDeltaTime);
        }
        else if (isFallingToGround && controller.isGrounded)
        {
            y = 0f;
            isFallingToGround = false;
            animator.SetBool("isGrounded", true);
        }
    }

    void HandleSideCollision(Vector3 normal)
    {
        if (normal.x > 0.5f)
        {
            if (side == Side.Left)
            {
                newXPos = 0;
                side = Side.Mid;
            }
            else if (side == Side.Mid)
            {
                newXPos = xValue; 
                side = Side.Right;
            }
        }
        else if (normal.x < -0.5f) 
        {
            if (side == Side.Right)
            {
                newXPos = 0; 
                side = Side.Mid;
            }
            else if (side == Side.Mid)
            {
                newXPos = -xValue; 
                side = Side.Left;
            }
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        forwardSpeed = 10f; 
        animator.SetTrigger("started");
    }
}