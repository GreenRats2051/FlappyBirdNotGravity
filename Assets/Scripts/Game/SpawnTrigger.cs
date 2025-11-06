using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private string objectPoolTag;
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Vector3 spawnPosition = transform.position + spawnOffset;
            GameManager.Instance.PoolManager.SpawnFromPool(objectPoolTag, spawnPosition, Quaternion.identity);
        }
    }
}