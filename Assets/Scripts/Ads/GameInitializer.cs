using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject advertisingServicePrefab;

    private void Start()
    {
        if (AdvertisingService.Instance == null && advertisingServicePrefab != null)
            Instantiate(advertisingServicePrefab);

        StartGame();
    }

    private void StartGame()
    {
        Debug.Log("Game started with ads!");

        Invoke(nameof(ShowStartAd), 3f);
    }

    private void ShowStartAd()
    {
        if (AdvertisingService.Instance != null)
            AdvertisingService.Instance.ShowInterstitial();
    }
}