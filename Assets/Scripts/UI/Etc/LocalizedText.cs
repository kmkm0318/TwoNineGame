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

    private void Start()
    {
        // 레퍼런스 초기화
        if (_text == null) _text = GetComponent<TMP_Text>();

        // null 체크
        if (_text == null) return;

        // 언어 변경 이벤트 구독
        LocalizationManager.Instance.OnLanguageChanged += UpdateLocalizedText;

        // 초기 텍스트 설정
        UpdateLocalizedText(LocalizationManager.Instance.CurrentLanguage);
    }

    private void OnDestroy()
    {
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