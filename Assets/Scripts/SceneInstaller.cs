using VContainer;
using VContainer.Unity;
using UnityEngine;

public class SceneInstaller : LifetimeScope
{
    [Header("Spawners")]
    [SerializeField] private ImprovedObjectSpawner[] objectSpawners;
    [SerializeField] private SpawnTrigger[] spawnTriggers;

    [Header("Enemies")]
    [SerializeField] private Enemy[] enemies;

    [Header("Collectibles")]
    [SerializeField] private ScoreCollectible[] scoreCollectibles;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GameManager>(Lifetime.Singleton).FromComponentInHierarchy();
        builder.Register<ObjectSpawnerFactory>(Lifetime.Singleton);

        foreach (var spawner in objectSpawners)
        {
            if (spawner != null)
                builder.RegisterBuildCallback(resolver => resolver.Inject(spawner));
        }

        foreach (var trigger in spawnTriggers)
        {
            if (trigger != null)
                builder.RegisterBuildCallback(resolver => resolver.Inject(trigger));
        }

        builder.RegisterComponentOnNewGameObject<EnemySpawnerController>(Lifetime.Singleton).WithParameter("spawners", objectSpawners);
    }
}