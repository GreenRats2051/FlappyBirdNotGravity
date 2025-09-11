using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public float fallGravity = 9.8f;
    public float riseGravity = -9.8f;
    public float maxFallSpeed = 10f;
    public float maxRiseSpeed = 10f;
    public Transform target;
    public float targetRightwardSpeed = 5f;
    public float smoothTime = 0.1f;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Vector3 initialLocalPosition;

    void Start()
    {
        rb.gravityScale = 0;
        target = transform.parent;
    }

    void Update()
    {
        target.Translate(Vector3.right * targetRightwardSpeed * Time.deltaTime);

        bool isHoldingInput = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);

        float targetVelocityY = isHoldingInput ? maxRiseSpeed : -maxFallSpeed;
        float newVelocityY = Mathf.SmoothDamp(rb.linearVelocity.y, targetVelocityY, ref currentVelocity.y, smoothTime);

        rb.linearVelocity = new Vector2(0, newVelocityY);

        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(initialLocalPosition.x, localPos.y, localPos.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Dead")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}