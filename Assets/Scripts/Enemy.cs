using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private bool startMovingUp = true;

    private Vector3 startPosition;
    private float currentDistance;
    private bool isMovingUp;
    private MovementDirection currentDirection;

    private enum MovementDirection { Up, Down }

    private void Start()
    {
        startPosition = transform.position;
        isMovingUp = startMovingUp;
        currentDistance = 0f;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float directionMultiplier = isMovingUp ? 1f : -1f;
        Vector3 movement = Vector3.up * directionMultiplier * moveSpeed * Time.deltaTime;

        transform.Translate(movement);
        currentDistance += Mathf.Abs(movement.y);

        if (currentDistance >= moveDistance)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        isMovingUp = !isMovingUp;
        currentDistance = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }
}