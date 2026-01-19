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

    [Header("Audio Settings")]
    [SerializeField] private Slider _bgmVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    #region 이벤트
    public event Action OnCloseButtonClicked;
    public event Action<float> OnBGMVolumeChanged;
    public event Action<float> OnSFXVolumeChanged;
    #endregion

    private void Awake()
    {
        // UI 이벤트 등록
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
        _bgmVolumeSlider.onValueChanged.AddListener((value) => OnBGMVolumeChanged?.Invoke(value));
        _sfxVolumeSlider.onValueChanged.AddListener((value) => OnSFXVolumeChanged?.Invoke(value));
    }

    #region UI 업데이트
    public void SetBGMVolume(float volume) => _bgmVolumeSlider.SetValueWithoutNotify(volume);
    public void SetSFXVolume(float volume) => _sfxVolumeSlider.SetValueWithoutNotify(volume);
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _showHideUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _showHideUI.Hide(duration, onComplete);
    #endregion
}