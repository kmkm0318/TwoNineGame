using UnityEngine;

/// <summary>
/// 광고 관련 데이터를 담는 SO
/// 기본값은 테스트 광고 ID
/// </summary>
[CreateAssetMenu(fileName = "AdData", menuName = "SO/Ad/AdData", order = 0)]
public class AdData : ScriptableObject
{
    [Header("Android Ad IDs")]
    [SerializeField] private string _androidTopBannerId = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] private string _androidBottomBannerId = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] private string _androidRewardId = "ca-app-pub-3940256099942544/5224354917";

    public string AndroidTopBannerId => _androidTopBannerId;
    public string AndroidBottomBannerId => _androidBottomBannerId;
    public string AndroidRewardId => _androidRewardId;
}