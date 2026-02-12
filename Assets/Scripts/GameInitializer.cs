using Unity.VisualScripting;
using VContainer;
using VContainer.Unity;
using static UnityEngine.Rendering.VolumeComponent;

public class GameInitializer : IInitializable
{
    private readonly ScoreSystem scoreSystem;
    private readonly ObjectPoolManager poolManager;

    [Inject]
    public GameInitializer(ScoreSystem scoreSystem, ObjectPoolManager poolManager)
    {
        this.scoreSystem = scoreSystem;
        this.poolManager = poolManager;
    }

    public void Initialize()
    {
        scoreSystem.Initialize();
        poolManager.Initialize();
    }
}