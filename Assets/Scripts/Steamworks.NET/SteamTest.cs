using UnityEngine;

/// <summary>
/// Steamworks.NET을 테스트하기 위한 클래스
/// </summary>
public class SteamTest : MonoBehaviour
{
    private void Start()
    {
        if (SteamManager.Initialized)
        {
            var name = Steamworks.SteamFriends.GetPersonaName();
            $"스팀 초기화 성공! 사용자 이름: {name}".Log(this);
        }
        else
        {
            $"스팀 초기화 실패!".LogError(this);
        }
    }
}