using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 설정 UI 클래스
/// </summary>
public class SettingsUI : MonoBehaviour, IShowHide
{
    [Header("UI Elements")]
    [SerializeField] private ShowHideUI _showHideUI;
    [SerializeField] private Button _closeButton;

    [Header("Settings Item")]
    [SerializeField] private SettingsItem _bgmSettingsItem;
    [SerializeField] private SettingsItem _sfxSettingsItem;
    [SerializeField] private SettingsItem _languageSettingsItem;
    [SerializeField] private SerializableDictionary<LanguageType, string> _languageTypeToText;

    #region 이벤트
    public event Action OnCloseButtonClicked;
    public event Action<bool> OnBGMButtonClicked;
    public event Action<bool> OnSFXButtonClicked;
    public event Action<bool> OnLanguageButtonClicked;
    #endregion

    private void Awake()
    {
        // UI 이벤트 등록
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());

        _bgmSettingsItem.OnLeftButtonClicked += () => OnBGMButtonClicked?.Invoke(false);
        _bgmSettingsItem.OnRightButtonClicked += () => OnBGMButtonClicked?.Invoke(true);
        _sfxSettingsItem.OnLeftButtonClicked += () => OnSFXButtonClicked?.Invoke(false);
        _sfxSettingsItem.OnRightButtonClicked += () => OnSFXButtonClicked?.Invoke(true);
        _languageSettingsItem.OnLeftButtonClicked += () => OnLanguageButtonClicked?.Invoke(false);
        _languageSettingsItem.OnRightButtonClicked += () => OnLanguageButtonClicked?.Invoke(true);
    }

    #region UI 업데이트
    public void SetBGMVolume(float volume)
    {
        // 백분율로 변경
        string percentage = (volume * 100f).ToString("0") + "%";

        // 텍스트 업데이트
        _bgmSettingsItem.SetValueText(percentage);
    }

    public void SetSFXVolume(float volume)
    {
        // 백분율로 변경
        string percentage = (volume * 100f).ToString("0") + "%";

        // 텍스트 업데이트
        _sfxSettingsItem.SetValueText(percentage);
    }

    public void SetLanguage(LanguageType languageType)
    {
        // 언어에 해당하는 텍스트 가져오기
        if (_languageTypeToText.TryGetValue(languageType, out string languageText))
        {
            // 텍스트 업데이트
            _languageSettingsItem.SetValueText(languageText);
        }
        else
        {
            // 텍스트가 없으면 경고 로그 출력
            $"[SettingsUI] Language text not found for type: {languageType}".LogWarning(this);
        }
    }
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _showHideUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _showHideUI.Hide(duration, onComplete);
    #endregion
}