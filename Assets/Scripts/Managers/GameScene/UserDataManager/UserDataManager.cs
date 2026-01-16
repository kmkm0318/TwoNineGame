using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 사용자 데이터 관리를 담당하는 매니저 클래스
/// </summary>
public class UserDataManager : MonoBehaviour
{
    #region 상수
    private const string SAVE_FILE_NAME = "UserData.json";
    #endregion

    #region 데이터
    public UserData UserData { get; private set; }
    #endregion

    #region 이벤트
    public event Action<string> OnUserNameChanged;
    public event Action<int> OnHighScoreChanged;
    #endregion

    private string _saveFilePath;

    #region 초기화
    public void Init()
    {
        // 저장 파일 경로 설정
        _saveFilePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

        // 사용자 데이터 로드
        LoadUserData();
    }
    #endregion

    #region 세이브, 로드
    private void SaveUserdata()
    {
        // 데이터가 없으면 저장하지 않음
        if (UserData == null) return;

        // Json 형식으로 변환
        string json = JsonUtility.ToJson(UserData);

        // 파일에 저장
        File.WriteAllText(_saveFilePath, json);
    }

    private void LoadUserData()
    {
        // 파일이 있는 경우
        if (File.Exists(_saveFilePath))
        {
            // 파일을 json 형식으로 읽음
            string json = File.ReadAllText(_saveFilePath);

            // Json을 객체로 변환
            UserData = JsonUtility.FromJson<UserData>(json);
        }
        // 파일이 없는 경우
        else
        {
            // 새로운 데이터 생성
            UserData = new();

            // 기본 데이터를 저장
            SaveUserdata();
        }
    }
    #endregion

    public void ChangePlayerName(string newName)
    {
        // 사용자 이름 변경
        UserData.UserName = newName;

        // 이벤트 호출
        OnUserNameChanged?.Invoke(newName);

        // 변경된 데이터 저장
        SaveUserdata();
    }

    public void UpdateHighScore(int newScore)
    {
        // 새로운 점수가 기존 최고 점수보다 낮거나 같으면 변경하지 않음
        if (newScore <= UserData.HighScore) return;

        // 최고 점수 변경
        UserData.HighScore = newScore;

        // 이벤트 호출
        OnHighScoreChanged?.Invoke(newScore);

        // 변경된 데이터 저장
        SaveUserdata();
    }
}