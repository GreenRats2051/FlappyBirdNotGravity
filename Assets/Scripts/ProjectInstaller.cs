using VContainer;
using VContainer.Unity;
using UnityEngine;

public class ProjectInstaller : LifetimeScope
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private ObjectPoolManager poolManager;
    [SerializeField] private GameManager gameManager;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IInputService, UnityInputService>(Lifetime.Singleton);
        builder.RegisterComponent(gameManager);
        builder.RegisterComponent(scoreSystem);
        builder.RegisterComponent(poolManager);

        builder.RegisterFactory<string, Vector3, Quaternion, GameObject>(
            container => (tag, position, rotation) =>
            {
                var poolManager = container.Resolve<ObjectPoolManager>();
                return poolManager.SpawnFromPool(tag, position, rotation);
            }, Lifetime.Singleton);

        builder.RegisterEntryPoint<GameInitializer>(Lifetime.Singleton);
    }
}