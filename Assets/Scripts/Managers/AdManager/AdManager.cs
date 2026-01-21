// 모바일 플랫폼인지 확인
#if UNITY_ANDROID || UNITY_IOS
#define MOBILE_PLATFORM
#endif

using System;

// 모바일 플랫폼인 경우만 사용
#if MOBILE_PLATFORM
using GoogleMobileAds.Api;
using UnityEngine;
#endif

/// <summary>
/// 안드로이드 광고를 관리하는 싱글톤 클래스
/// </summary>
public class AdManager : Singleton<AdManager>
{
    [Header("Ad Data")]
    [SerializeField] private AdData _adData;

#if MOBILE_PLATFORM
    #region 광고 ID
    private string _topBannerId;
    private string _bottomBannerId;
    private string _rewardId;
    #endregion

    #region 광고 객체
    private BannerView _topBanner;
    private BannerView _bottomBanner;
    private RewardedAd _rewardedAd;
    #endregion
#endif

    // 모바일 플랫폼인 경우에만 함수 구현
#if MOBILE_PLATFORM
    override protected void Awake()
    {
        // 싱글톤 초기화
        base.Awake();

        // 광고 ID 할당
        if (_adData != null)
        {
            _topBannerId = _adData.AndroidTopBannerId;
            _bottomBannerId = _adData.AndroidBottomBannerId;
            _rewardId = _adData.AndroidRewardId;
        }

        // 모바일 광고 초기화
        MobileAds.Initialize(initStatus =>
        {
            // 배너 광고 로드
            LoadBanners();

            // 보상형 광고 로드
            LoadRewardedAd();
        });
    }

    private void LoadBanners()
    {
        // 배너 광고 생성
        _topBanner = new BannerView(_topBannerId, AdSize.Banner, AdPosition.Top);
        _bottomBanner = new BannerView(_bottomBannerId, AdSize.Banner, AdPosition.Bottom);

        // 광고 요청 생성
        AdRequest request = new();

        // 배너 광고 로드
        _topBanner.LoadAd(request);
        _bottomBanner.LoadAd(request);
    }

    private void LoadRewardedAd()
    {
        // 보상형 광고 로드
        RewardedAd.Load(_rewardId, new(), (ad, error) =>
        {
            // 광고 로드 실패 시 패스
            if (error != null) return;

            // 보상형 광고 할당
            _rewardedAd = ad;
        });
    }
#endif

    /// <summary>
    /// 보상형 광고를 보여줄 수 있는지 여부
    /// </summary>
    public bool CanShowRewardedAd()
    {
#if MOBILE_PLATFORM
        // 모바일인 경우에만 체크
        return _rewardedAd != null && _rewardedAd.CanShowAd();
#else
        // 모바일이 아닌 경우 항상 false 반환
        return false;
#endif
    }

    /// <summary>
    /// 보상형 광고 보여주기
    /// </summary>
    public void ShowRewardedAd(Action onRewarded)
    {
#if MOBILE_PLATFORM
        // 모바일인 경우에만 실행

        // 보상형 광고를 보여줄 수 없으면 패스
        if (!CanShowRewardedAd()) return;

        _rewardedAd.Show((reward) =>
        {
            // 광고 시청 완료 후 보상 콜백 호출
            onRewarded?.Invoke();

            // 다음 광고를 위해 새로 로드
            LoadRewardedAd();
        });
#else
        // 모바일이 아닌 경우 패스
        return;
#endif
    }
}