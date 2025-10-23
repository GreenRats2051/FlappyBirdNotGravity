using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float fallGravity = 9.8f;
    [SerializeField] private float riseGravity = -9.8f;
    [SerializeField] private float maxFallSpeed = 10f;
    [SerializeField] private float maxRiseSpeed = 10f;
    [SerializeField] private float smoothTime = 0.1f;

    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private float targetRightwardSpeed = 5f;

    private Rigidbody2D rigidBody;
    private Vector2 currentVelocity;
    private Vector3 initialLocalPosition;
    private IInputService inputService;
    private float targetVelocityY;

    public void Initialize(IInputService inputService)
    {
        this.inputService = inputService;
        rigidBody.gravityScale = 0;
        initialLocalPosition = transform.localPosition;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (inputService == null)
        {
            inputService = new UnityInputService();
            Initialize(inputService);
        }
    }

    private void Update()
    {
        HandleTargetMovement();
        HandleBallMovement();
        ConstrainHorizontalPosition();
    }

    private void HandleTargetMovement()
    {
        if (target != null)
        {
            target.Translate(Vector3.right * targetRightwardSpeed * Time.deltaTime);
        }
    }

    private void HandleBallMovement()
    {
        if (inputService == null)
            return;

        bool isHoldingInput = inputService.IsHoldingInput();

        if (isHoldingInput)
            targetVelocityY = maxRiseSpeed;
        else
            targetVelocityY = -maxFallSpeed;

        float newVelocityY = Mathf.SmoothDamp(rigidBody.linearVelocity.y, targetVelocityY, ref currentVelocity.y, smoothTime);

        rigidBody.linearVelocity = new Vector2(0, newVelocityY);
    }

    private void ConstrainHorizontalPosition()
    {
        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(initialLocalPosition.x, localPos.y, localPos.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Dead"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RestartLevel();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}