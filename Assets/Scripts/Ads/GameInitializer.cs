using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject advertisingServicePrefab;

    private void Start()
    {
        if (FindObjectOfType<AdvertisingService>() == null && advertisingServicePrefab != null)
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

    private void ShowRewardedAdExample()
    {
        AdvertisingService.Instance.ShowRewardedAd((success) =>
        {
            if (success)
            {
                Debug.Log("Reward granted!");
            }
            else
            {
                Debug.Log("Reward not granted");
            }
        });
    }
}