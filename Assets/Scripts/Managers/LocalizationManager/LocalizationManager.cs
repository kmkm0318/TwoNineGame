using System;
using UnityEngine;

/// <summary>
/// 로컬라이제이션을 관리하는 싱글톤 클래스
/// </summary>
public class LocalizationManager : Singleton<LocalizationManager>
{
    [Header("Localization Table Data")]
    [SerializeField] private LocalizationTableData _localizationTableData;

    #region 변수
    public LanguageType CurrentLanguage { get; private set; } = LanguageType.Korean;
    #endregion

    #region 레퍼런스
    private SettingsManager _settingsManager;
    #endregion

    #region 이벤트
    public event Action<LanguageType> OnLanguageChanged;
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

        // 이벤트 등록
        RegisterEvents();

        // 초기 언어 설정
        SetLanguage(_settingsManager.SettingsData.Language);
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        // 설정 변경 이벤트 구독
        _settingsManager.OnLanguageChanged += SetLanguage;
    }

    private void UnregisterEvents()
    {
        // 설정 변경 이벤트 해제
        _settingsManager.OnLanguageChanged -= SetLanguage;
    }
    #endregion

    public void SetLanguage(LanguageType languageType)
    {
        // 현재 언어라면 패스
        if (CurrentLanguage == languageType) return;

        // 언어 변경
        CurrentLanguage = languageType;

        // 이벤트 호출
        OnLanguageChanged?.Invoke(CurrentLanguage);
    }

    public string GetLocalizedText(string key)
    {
        // 데이터가 없으면 키 반환
        if (_localizationTableData == null) return key;

        // 테이블 가져오기
        var localizationTable = _localizationTableData.LocalizationTable;

        // 테이블이 없으면 키 반환
        if (localizationTable == null) return key;

        // 현재 언어에 대한 텍스트 테이블 데이터가 없으면 키 반환
        if (!localizationTable.TryGetValue(CurrentLanguage, out var localizedTextTableData)) return key;

        // 텍스트 테이블 가져오기
        var localizedTextTable = localizedTextTableData.LocalizedTextTable;

        // 텍스트 테이블이 없으면 키 반환
        if (localizedTextTable == null) return key;

        // 현재 언어에 대한 텍스트 테이블이 없으면 키 반환
        if (!localizedTextTable.TryGetValue(key, out var text)) return key;

        // 텍스트 반환
        return text;
    }
}
