using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            SpawnObject();
    }

    private void SpawnObject()
    {
        if (objectToSpawn != null)
            Instantiate(objectToSpawn, gameObject.transform.position + spawnPosition, Quaternion.identity);
    }
}