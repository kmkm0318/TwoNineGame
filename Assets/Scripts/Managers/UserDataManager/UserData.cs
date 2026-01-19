using System;

/// <summary>
/// 사용자 데이터를 나타내는 클래스
/// </summary>
[Serializable]
public class UserData
{
    public string UserName;
    public int BestScore;

    public UserData()
    {
        UserName = "Unknown";
        BestScore = 0;
    }
}