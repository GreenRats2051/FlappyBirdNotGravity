using VContainer;
using UnityEngine;

public class ObjectSpawnerFactory
{
    private readonly IObjectResolver container;
    private readonly GameManager gameManager;

    public ObjectSpawnerFactory(IObjectResolver container, GameManager gameManager)
    {
        this.container = container;
        this.gameManager = gameManager;
    }

    public ImprovedObjectSpawner CreateSpawner(Vector3 position, string tag)
    {
        var spawnerObject = new GameObject($"ObjectSpawner_{tag}");
        spawnerObject.transform.position = position;

        var spawner = spawnerObject.AddComponent<ImprovedObjectSpawner>();
        container.Inject(spawner);

        return spawner;
    }
}