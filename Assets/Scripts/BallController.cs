using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("��������� ����������")]
    public float fallGravity = 9.8f;     // ���� ���������� ��� �������
    public float riseGravity = -9.8f;    // ���� "��������������" ��� �������
    public float maxFallSpeed = 10f;     // ������������ �������� �������
    public float maxRiseSpeed = 10f;     // ������������ �������� �������

    [Header("���� (Target)")]
    public Transform target;             // ������ �� ������ Target
    public float targetRightwardSpeed = 5f; // �������� �������� Target ������

    [Header("��������� ��������")]
    public float smoothTime = 0.1f;      // ����� ��� �������� ��������� ��������

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Vector3 initialLocalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.gravityScale = 0;

        if (target == null && transform.parent != null)
            target = transform.parent;
    }

    void Update()
    {
        MoveTargetRight();
        ControlPlayerVerticalMovement();
    }

    void MoveTargetRight()
    {
        if (target != null)
            target.Translate(Vector3.right * targetRightwardSpeed * Time.deltaTime);
    }

    void ControlPlayerVerticalMovement()
    {
        if (rb == null) return;

        bool isHoldingInput = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);

        float targetVelocityY = isHoldingInput ? maxRiseSpeed : -maxFallSpeed;
        float newVelocityY = Mathf.SmoothDamp(rb.linearVelocity.y, targetVelocityY, ref currentVelocity.y, smoothTime);

        rb.linearVelocity = new Vector2(0, newVelocityY);

        Vector3 localPos = transform.localPosition;
        transform.localPosition = new Vector3(initialLocalPosition.x, localPos.y, localPos.z);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 20), $"Player Local Y: {transform.localPosition.y:F1}");
        if (target != null)
        {
            GUI.Label(new Rect(10, 30, 300, 20), $"Target Position X: {target.position.x:F1}");
            GUI.Label(new Rect(10, 50, 300, 20), $"Player World Y: {transform.position.y:F1}");
        }
    }
}