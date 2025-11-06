using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisingService : MonoBehaviour
{
    public static AdvertisingService Instance;

    [Header("Game IDs")]
    [SerializeField] private string androidGameId = "5000000";
    [SerializeField] private string iosGameId = "5000001";

    [Header("Banner Placement ID")]
    [SerializeField] private string bannerPlacementId = "banner";

    private string _currentGameId;
    private bool _isInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAds()
    {
#if UNITY_ANDROID
        _currentGameId = androidGameId;
#elif UNITY_IOS
        _currentGameId = iosGameId;
#else
        _currentGameId = androidGameId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_currentGameId, true);

            StartCoroutine(WaitForInitialization());
        }
    }

    private System.Collections.IEnumerator WaitForInitialization()
    {
        while (!Advertisement.isInitialized)
            yield return new WaitForSeconds(0.5f);

        _isInitialized = true;
        Debug.Log("Ads initialized - showing banner");
        ShowBanner();
    }

    public void ShowBanner()
    {
        if (!_isInitialized)
            return;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerPlacementId);
    }

    public void HideBanner()
    {
        if (Advertisement.Banner.isLoaded)
            Advertisement.Banner.Hide();
    }

    public void ShowInterstitial()
    {
        if (_isInitialized)
            Advertisement.Show("interstitial");
    }

    public void ShowRewardedAd()
    {
        if (_isInitialized)
            Advertisement.Show("rewarded");
    }
}