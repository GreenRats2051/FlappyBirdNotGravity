using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisingService : MonoBehaviour
{
    public static AdvertisingService Instance;

    [Header("Game IDs")]
    [SerializeField] private string androidGameId = "5000000";
    [SerializeField] private string iosGameId = "5000001";

    [Header("Placement IDs")]
    [SerializeField] private string bannerPlacementId = "Banner_Android";
    [SerializeField] private string interstitialPlacementId = "Interstitial_Android";
    [SerializeField] private string rewardedPlacementId = "Rewarded_Android";

    private string _currentGameId;
    private bool _isInitialized = false;

    public System.Action<bool> OnRewardedAdCompleted;

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

        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize(_currentGameId);
            StartCoroutine(WaitForInitialization());
        }
    }

    private System.Collections.IEnumerator WaitForInitialization()
    {
        while (!Advertisement.isInitialized)
            yield return new WaitForSeconds(0.5f);

        _isInitialized = true;
        Debug.Log("Ads initialized - loading banner");

        LoadBanner();
    }

    private void LoadBanner()
    {
        if (!_isInitialized) return;

        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(bannerPlacementId, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBanner();
    }

    private void OnBannerError(string message)
    {
        Debug.LogError($"Banner Error: {message}");
    }

    public void ShowBanner()
    {
        if (!_isInitialized) return;

        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerPlacementId, options);
    }

    private void OnBannerClicked() { }
    private void OnBannerHidden() { }
    private void OnBannerShown() { }

    public void HideBanner()
    {
        if (_isInitialized)
            Advertisement.Banner.Hide();
    }

    public void ShowInterstitial()
    {
        if (!_isInitialized)
        {
            Debug.Log("Ads not initialized yet");
            return;
        }

        Advertisement.Load(interstitialPlacementId);
        Advertisement.Show(interstitialPlacementId);
    }

    public void ShowRewardedAd(System.Action<bool> onCompleted = null)
    {
        if (!_isInitialized)
        {
            Debug.Log("Ads not initialized yet");
            onCompleted?.Invoke(false);
            return;
        }

        if (onCompleted != null)
            OnRewardedAdCompleted = onCompleted;

        Advertisement.Load(rewardedPlacementId);
        Advertisement.Show(rewardedPlacementId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log($"Ad ready: {placementId}");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Ad error: {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"Ad started: {placementId}");
    }

    public void OnUnityAdsDidFinish(string placementId, UnityEngine.Advertisements.ShowResult showResult)
    {
        Debug.Log($"Ad finished: {placementId} with result: {showResult}");

        if (placementId == rewardedPlacementId)
        {
            bool success = (showResult == ShowResult.Finished);
            OnRewardedAdCompleted?.Invoke(success);
            OnRewardedAdCompleted = null;
        }
    }
}