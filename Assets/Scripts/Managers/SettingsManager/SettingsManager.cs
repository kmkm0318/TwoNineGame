using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 설정 관련 매니저
/// </summary>
public class SettingsManager : MonoBehaviour
{
    #region 상수
    private const string SAVE_FILE_NAME = "Settings.json";
    #endregion

    #region 에디터 변수
    [Header("Volume Settings")]
    [SerializeField] private int _volumeStep = 1;
    [SerializeField] private int _maxVolume = 10;
    #endregion

    #region 데이터
    public SettingsData SettingsData { get; private set; }
    #endregion

    #region 변수
    private string _savePath;
    #endregion

    #region 이벤트
    public event Action<float> OnBGMVolumeChanged;
    public event Action<float> OnSFXVolumeChanged;
    #endregion

    #region 초기화
    public void Init()
    {
        // 저장 경로 설정
        _savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

        // 설정 데이터 로드
        LoadSettings();
    }
    #endregion

    #region 세이브, 로드
    private void SaveSettings()
    {
        // 데이터가 없으면 저장하지 않음
        if (SettingsData == null) return;

        // 데이터를 Json으로 변환
        string json = JsonUtility.ToJson(SettingsData);

        // 파일로 저장
        File.WriteAllText(_savePath, json);
    }

    private void LoadSettings()
    {
        // 파일이 존재하는지 확인
        if (File.Exists(_savePath))
        {
            // 파일에서 Json 읽기
            string json = File.ReadAllText(_savePath);

            // Json을 데이터로 변환
            SettingsData = JsonUtility.FromJson<SettingsData>(json);
        }
        else
        {
            // 파일이 없으면 기본 설정 생성
            SettingsData = new();

            // 기본 설정 저장
            SaveSettings();
        }
    }
    #endregion

    #region 데이터 변경
    public void ChangeBGMVolume(bool increase)
    {
        // 볼륨 계산
        float newVolume = GetNewVolume(SettingsData.BGMVolume, increase);

        // 볼륨 설정
        SetBGMVolume(newVolume);
    }

    public void SetBGMVolume(float volume)
    {
        // 데이터 변경
        SettingsData.BGMVolume = volume;

        // 이벤트 호출
        OnBGMVolumeChanged?.Invoke(volume);

        // 설정 저장
        SaveSettings();
    }

    public void ChangeSFXVolume(bool increase)
    {
        // 볼륨 계산
        float newVolume = GetNewVolume(SettingsData.SFXVolume, increase);

        // 볼륨 설정
        SetSFXVolume(newVolume);
    }

    public void SetSFXVolume(float volume)
    {
        // 데이터 변경
        SettingsData.SFXVolume = volume;

        // 이벤트 호출
        OnSFXVolumeChanged?.Invoke(volume);

        // 설정 저장
        SaveSettings();
    }
    #endregion

    #region 계산
    private float GetNewVolume(float oldVolume, bool increase)
    {
        // 현재 볼륨을 0~10 범위의 정수로 변환
        int currentVolume = Mathf.RoundToInt(oldVolume * _maxVolume);

        // 볼륨 변경
        currentVolume += increase ? _volumeStep : -_volumeStep;

        // 범위 제한
        currentVolume = Mathf.Clamp(currentVolume, 0, _maxVolume);

        // 0~1 범위로 변환
        float newVolume = currentVolume / (float)_maxVolume;

        // 반환
        return newVolume;
    }
    #endregion
}
