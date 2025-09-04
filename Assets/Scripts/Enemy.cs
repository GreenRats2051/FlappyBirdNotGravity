using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 5f;
    public bool isMovingUp = true;

    private Vector3 startPosition;
    private float currentDistance = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float direction = isMovingUp ? 1f : -1f;

        Vector3 movement = Vector3.up * direction * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        currentDistance += Mathf.Abs(movement.y);

        if (currentDistance >= moveDistance)
        {
            isMovingUp = !isMovingUp;
            currentDistance = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isMovingUp = !isMovingUp;
        currentDistance = 0f;
    }
}