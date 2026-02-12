using UnityEngine;
using VContainer;
using VContainer.Unity;

public static class VContainerBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        if (ProjectInstaller.Instance == null)
        {
            var projectInstallerPrefab = Resources.Load<ProjectInstaller>("VContainerSetup");
            if (projectInstallerPrefab != null)
            {
                Object.Instantiate(projectInstallerPrefab);
            }
            else
            {
                Debug.LogError("VContainerSetup префаб не найден в Resources папке!");
            }
        }
    }
}