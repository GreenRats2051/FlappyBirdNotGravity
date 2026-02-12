using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private ObjectPoolManager poolManager;

    private LifetimeScope scope;

    public ScoreSystem ScoreSystem => scoreSystem;
    public ObjectPoolManager PoolManager => poolManager;

    private void Awake()
    {
        scope = GetComponent<LifetimeScope>();

        if (scope == null)
        {
            scope = gameObject.AddComponent<LifetimeScope>();
        }
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}