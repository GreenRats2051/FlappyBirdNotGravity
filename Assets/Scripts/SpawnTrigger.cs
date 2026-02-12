using UnityEngine;
using VContainer;
using static UnityEngine.Rendering.VolumeComponent;

public class SpawnTrigger : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private string objectPoolTag;
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private string playerTag = "Player";

    private ObjectPoolManager poolManager;

    [Inject]
    public void Construct(ObjectPoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && poolManager != null)
        {
            Vector3 spawnPosition = transform.position + spawnOffset;
            poolManager.SpawnFromPool(objectPoolTag, spawnPosition, Quaternion.identity);
        }
    }
}