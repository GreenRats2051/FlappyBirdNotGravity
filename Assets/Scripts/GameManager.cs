using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private ObjectPoolManager poolManager;

    public ScoreSystem ScoreSystem => scoreSystem;
    public ObjectPoolManager PoolManager => poolManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSystems();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSystems()
    {
        scoreSystem.Initialize();
        poolManager.Initialize();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}