using System;

/// <summary>
/// 설정 데이터
/// </summary>
[Serializable]
public class SettingsData
{
    #region 오디오 설정
    public float BGMVolume = 0.5f;
    public float SFXVolume = 0.5f;
    #endregion

    #region 언어 설정
    public LanguageType Language = LanguageType.English;
    #endregion
}