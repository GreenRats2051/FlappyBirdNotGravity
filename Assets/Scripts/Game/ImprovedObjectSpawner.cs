using UnityEngine;
using System.Collections.Generic;

public class ImprovedObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private string objectPoolTag;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float minDistanceBetweenObjects = 1.5f;
    [SerializeField] private int maxAttempts = 50;
    [SerializeField] private int maxAmount = 1;

    private List<Vector2> spawnedPositions = new List<Vector2>();

    public void SpawnObjects()
    {
        int numberOfObjects = Random.Range(0, maxAmount + 1);

        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnSingleObject();
        }

        spawnedPositions.Clear();
    }

    private void SpawnSingleObject()
    {
        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            Vector2 spawnPosition = CalculateSpawnPosition();

            if (IsPositionValid(spawnPosition))
            {
                GameObject spawnedObject = GameManager.Instance.PoolManager.SpawnFromPool(objectPoolTag, spawnPosition, Quaternion.identity);

                spawnedPositions.Add(spawnPosition);
                break;
            }
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minDistance, maxDistance);
        return (Vector2)transform.position + randomDirection * randomDistance;
    }

    private bool IsPositionValid(Vector2 position)
    {
        foreach (Vector2 existingPosition in spawnedPositions)
        {
            if (Vector2.Distance(position, existingPosition) < minDistanceBetweenObjects)
            {
                return false;
            }
        }
        return true;
    }
}