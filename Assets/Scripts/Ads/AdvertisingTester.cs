using UnityEngine;

public class AdvertisingTester : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 400));

        GUILayout.Label("Test Panel");

        if (GUILayout.Button("Show Banner"))
            AdvertisingService.Instance.ShowBanner();

        if (GUILayout.Button("Hide Banner"))
            AdvertisingService.Instance.HideBanner();

        if (GUILayout.Button("Show Rewarded Ad"))
            AdvertisingService.Instance.ShowRewardedAd("test_reward");

        if (GUILayout.Button("Show Interstitial"))
            AdvertisingService.Instance.ShowInterstitialAd();

        if (GUILayout.Button("Reload Banner"))
            AdvertisingService.Instance.LoadBanner();

        GUILayout.EndArea();
    }
}