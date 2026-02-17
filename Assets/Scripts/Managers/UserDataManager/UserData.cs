using System;

/// <summary>
/// 사용자 데이터를 나타내는 클래스
/// </summary>
[Serializable]
public class UserData
{
    public string UserName = "Unknown";
    public int BestScore = 0;
    public bool IsTutorialCompleted = false;
}