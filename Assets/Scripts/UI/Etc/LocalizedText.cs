using TMPro;
using UnityEngine;

/// <summary>
/// 로컬라이즈된 텍스트 컴포넌트
/// </summary>
public class LocalizedText : TMP_Text
{
    [Header("UI Components")]
    [SerializeField] private TMP_Text _text;

    [Header("Localization Key")]
    [SerializeField] private string _localizationKey;

    protected override void OnEnable()
    {
        // 기본 OnEnable 호출
        base.OnEnable();

        // 언어 변경 이벤트 구독
        LocalizationManager.Instance.OnLanguageChanged += UpdateLocalizedText;

        // 초기 텍스트 설정
        UpdateLocalizedText(LocalizationManager.Instance.CurrentLanguage);
    }

    protected override void OnDisable()
    {
        // 기본 OnDisable 호출
        base.OnDisable();

        // 언어 변경 이벤트 구독 해제
        LocalizationManager.Instance.OnLanguageChanged -= UpdateLocalizedText;
    }

    private void UpdateLocalizedText(LanguageType languageType)
    {
        // 로컬라이즈된 텍스트 가져오기
        string localizedText = LocalizationManager.Instance.GetLocalizedText(_localizationKey);

        // 텍스트 설정
        _text.text = localizedText;
    }
}