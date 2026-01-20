using TMPro;
using UnityEngine;

/// <summary>
/// 텍스트를 로컬라이즈하는 컴포넌트
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    [Header("Localization Key")]
    [SerializeField] private string _localizationKey;

    #region 레퍼런스
    private TMP_Text _text;
    #endregion

    #region 변수
    private bool _isRegistered = false;
    #endregion

    private void Awake()
    {
        // 레퍼런스 초기화
        if (_text == null) _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        // 싱글톤 초기화 오류를 방지하기 위해 Start에서 OnEnable 호출
        OnEnable();
    }

    private void OnEnable()
    {
        // 이벤트 구독
        RegisterEvents();

        // 싱글톤이 없으면 패스
        if (LocalizationManager.Instance == null) return;

        // 텍스트 초기화
        UpdateLocalizedText(LocalizationManager.Instance.CurrentLanguage);
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        UnregisterEvents();
    }

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        // 중복 등록 방지
        if (_isRegistered) return;

        // 싱글톤이 없으면 패스
        if (LocalizationManager.Instance == null) return;

        // 이벤트 구독
        LocalizationManager.Instance.OnLanguageChanged += UpdateLocalizedText;

        // 플래그 설정
        _isRegistered = true;
    }

    private void UnregisterEvents()
    {
        // 등록되지 않았으면 패스
        if (!_isRegistered) return;

        // 싱글톤이 없으면 패스
        if (LocalizationManager.Instance == null) return;

        // 이벤트 구독 해제
        LocalizationManager.Instance.OnLanguageChanged -= UpdateLocalizedText;

        // 플래그 설정
        _isRegistered = false;
    }
    #endregion

    private void UpdateLocalizedText(LanguageType languageType)
    {
        // 싱글톤이 없으면 패스
        if (LocalizationManager.Instance == null) return;

        // 로컬라이즈된 텍스트 가져오기
        string localizedText = LocalizationManager.Instance.GetLocalizedText(_localizationKey);

        // 텍스트 설정
        _text.text = localizedText;
    }
}