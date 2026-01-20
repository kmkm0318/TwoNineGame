using System;
using UnityEngine;

/// <summary>
/// 설정 UI 프레젠터
/// </summary>
public class SettingsPresenter : MonoBehaviour, IShowHide
{
    [Header("UI References")]
    [SerializeField] private SettingsUI _settingsUI;

    #region 레퍼런스
    private SettingsManager _settingsManager;
    #endregion

    private void OnDestroy()
    {
        // 이벤트 해제
        UnregisterEvents();
    }

    #region 초기화
    public void Init(SettingsManager settingsManager)
    {
        // 레퍼런스 설정
        _settingsManager = settingsManager;

        // 이벤트 구독
        RegisterEvents();

        // UI 초기화
        InitUI();
    }

    private void InitUI()
    {
        // 설정 데이터 가져오기
        var data = _settingsManager.SettingsData;

        // 현재 설정값으로 UI 업데이트
        _settingsUI.SetBGMVolume(data.BGMVolume);
        _settingsUI.SetSFXVolume(data.SFXVolume);
        _settingsUI.SetLanguage(data.Language);
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        // 설정 변경 이벤트 구독
        _settingsManager.OnBGMVolumeChanged += _settingsUI.SetBGMVolume;
        _settingsManager.OnSFXVolumeChanged += _settingsUI.SetSFXVolume;
        _settingsManager.OnLanguageChanged += _settingsUI.SetLanguage;

        // 설정 UI 변경 이벤트 구독
        _settingsUI.OnCloseButtonClicked += HandleOnCloseButtonClicked;
        _settingsUI.OnBGMButtonClicked += _settingsManager.ChangeBGMVolume;
        _settingsUI.OnSFXButtonClicked += _settingsManager.ChangeSFXVolume;
        _settingsUI.OnLanguageButtonClicked += _settingsManager.ChangeLanguage;
    }

    private void UnregisterEvents()
    {
        // 설정 변경 이벤트 구독 해제
        _settingsManager.OnBGMVolumeChanged -= _settingsUI.SetBGMVolume;
        _settingsManager.OnSFXVolumeChanged -= _settingsUI.SetSFXVolume;
        _settingsManager.OnLanguageChanged -= _settingsUI.SetLanguage;

        // 설정 UI 변경 이벤트 구독 해제
        _settingsUI.OnCloseButtonClicked -= HandleOnCloseButtonClicked;
        _settingsUI.OnBGMButtonClicked -= _settingsManager.ChangeBGMVolume;
        _settingsUI.OnSFXButtonClicked -= _settingsManager.ChangeSFXVolume;
        _settingsUI.OnLanguageButtonClicked -= _settingsManager.ChangeLanguage;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnCloseButtonClicked() => Hide(0f);
    #endregion

    #region Show, Hide
    public void Show(float duration = 0.5f, Action onComplete = null) => _settingsUI.Show(duration, onComplete);
    public void Hide(float duration = 0.5f, Action onComplete = null) => _settingsUI.Hide(duration, onComplete);
    #endregion
}