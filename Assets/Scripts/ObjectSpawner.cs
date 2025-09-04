using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float minDistance = 2f;
    public float maxDistance = 5f;
    public float minDistanceBetweenObjects = 1.5f;
    public int maxAttempts = 50;

    private List<Vector2> spawnedPositions = new List<Vector2>();

    void Start()
    {
        SpawnRandomObjects();
    }

    void SpawnRandomObjects()
    {
        int numberOfObjects = Random.Range(0, 4);

        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnSingleObject();
        }

        spawnedPositions.Clear();
    }

    void SpawnSingleObject()
    {
        Vector2 spawnPosition;
        bool validPositionFound = false;
        int attempts = 0;

        while (!validPositionFound && attempts < maxAttempts)
        {
            attempts++;

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minDistance, maxDistance);
            spawnPosition = (Vector2)transform.position + randomDirection * randomDistance;

            if (IsPositionValid(spawnPosition))
            {
                validPositionFound = true;

                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                spawnedPositions.Add(spawnPosition);
            }
        }
    }

    bool IsPositionValid(Vector2 position)
    {
        foreach (Vector2 existingPosition in spawnedPositions)
        {
            float distance = Vector2.Distance(position, existingPosition);
            if (distance < minDistanceBetweenObjects)
            {
                return false;
            }
        }
        return true;
    }
}